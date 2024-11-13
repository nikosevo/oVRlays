using Microsoft.VisualStudio.Utilities;
using oVRlays.handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xaml;

namespace oVRlays.Views
{
    /// <summary>
    /// This will be the logic behind the graph 
    /// Data are saved to a circular buffer so that we avoid shifting valued and copying everytime there is a change
    /// Data are updated with a subscription method, meaning, when new data is in the SimData it will notify here, no polling needed
    /// the refresh rate of the graph is completely unrelated to the data, however make sure to be fast enough so that we dont loose value but slow enough so that we dont draw same data multiple times
    /// </summary>
    /// 

    public partial class TelemetryGraph: UserControl, IOverlay
    {

        private int bufferSize = 200;                                           //this will determine how many samples are currerntly on the window
        private readonly DispatcherTimer refreshRate = new DispatcherTimer();   //uset this to chcange the refresh rate of the overrlay
     
        
        private readonly object _lock = new object();                           
        Stopwatch stopwatch = new Stopwatch();

        private static CircularBuffer<float> telemetryData_brake;
        private static CircularBuffer<float> telemetryData_throtle;

        private handlers.SimData simData;




        public TelemetryGraph(SimData simData)
        {
            InitializeComponent();

            //update the data of the circulat buffers when telemetryUpdated is invoked
            this.simData = simData;
            simData.TelemetryUpdated += updateBuffer;

            //int buffers
            telemetryData_throtle = new CircularBuffer<float>(bufferSize);
            telemetryData_brake = new CircularBuffer<float>(bufferSize);


            //set refresh rate
            refreshRate.Interval = TimeSpan.FromMilliseconds(10);    //set refresh to 100hz for now ( ms = 1/hz * 1000) 
            refreshRate.Tick += (sender,args)=> RenderGraph();                     //Subscribe the rendering of the graph to the timer
            refreshRate.Start();
        }

        private void RenderGraph()
        {
            //get the canvas size so that we draw the line in it
            double y = telemetryWindow.ActualHeight;
            double x = telemetryWindow.ActualWidth;
            double stepX = x / bufferSize;

            //generate the lines 
            Polyline throttle = new Polyline
            {
                Stroke = Brushes.LimeGreen,
                StrokeThickness = 2
            };
            Polyline brake = new Polyline
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };


            //todo put the whole plotting into a function 
            //copy buffers to a temp variable to avoid data changing while itterating
            var brakePoints = telemetryData_brake.ToArray();
            var throttlePoints = telemetryData_throtle.ToArray();


            //todo use for and foreach loops to determine if there is efficiency difference
            x = 0;
            foreach(double value in brakePoints)
            {
                x += stepX;
                brake.Points.Add(new Point(x, y - y * value));

            }
            x = 0;
            foreach (double value in throttlePoints)
            {
                x += stepX;
                throttle.Points.Add(new Point(x, y - y * value));
            }

            //repaint graph
            telemetryWindow.Children.Clear();
            telemetryWindow.Children.Add(brake);
            telemetryWindow.Children.Add(throttle);



        }

        private void updateBuffer(handlers.telemetryDataStruct data)
        {
            Debug.WriteLine(data.throttle_application);
            telemetryData_brake.Add(data.brake_application);
            telemetryData_throtle.Add(data.throttle_application);
        }
    }
}
