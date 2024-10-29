using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.VisualStudio.Utilities;

namespace oVRlays.Views
{
    /// <summary>
    /// Interaction logic for LiveGraph.xaml
    /// </summary>
    /// 

    public partial class LiveGraph : UserControl
    {
        //todo change that based on windows size + probably need to use now list for graphData
        private int bufferSize = 100;
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private readonly object _lock = new object();


        private static CircularBuffer<double> graphData_brake;
        private static CircularBuffer<double> graphData_throttle;

        TimeSpan elapsed_buff_add;
        TimeSpan elapsed_buff_iter;

        Stopwatch stopwatch = new Stopwatch();




        public LiveGraph()
        {
            InitializeComponent();
            // Set the timer interval to 60 ms
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += (sender,args) => RenderGraph(); // Subscribe to the Tick event


            //initiate the graphData buffers
            graphData_brake = new CircularBuffer<double>(bufferSize);
            graphData_throttle = new CircularBuffer<double>(bufferSize);



            // Start the timer
            timer.Start();


        }

        private void RenderGraph()
        {

            graphCanvas.Children.Clear(); // Clear previous drawings
                                          // Set up the polyline to represent the line graph

            double y = graphCanvas.ActualHeight;
            double x = graphCanvas.ActualWidth;
            double stepX = x / bufferSize;


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



            //--------------------------------------- CIRCULAR BUFFER -----------------------
            stopwatch.Reset();
            stopwatch.Start();
            var brakePoints = graphData_brake.ToArray();
            var throttlePoints = graphData_throttle.ToArray();

            //todo try using foreach and for loop to see if any of em have an advantage
            x = 0;
            int numOfElements = brakePoints.Length;
            for (int i = 0; i< numOfElements; i++)
            {
                brake.Points.Add(new Point(x * stepX, y - y * brakePoints[i]));
                brake.Points.Add(new Point(x * stepX, y - y * throttlePoints[i]));

            }
            graphCanvas.Children.Add(brake);
            graphCanvas.Children.Add(throttle);

            stopwatch.Stop();
            elapsed_buff_iter = stopwatch.Elapsed;

            // Add the polyline to the canvas
            //graphCanvas.Children.Add(throtle);


        }
        internal void updateSpeed(float speed)
        {
            //todo not implemented
        }
        internal void updateThrottle(float throttle_application)
        {
            stopwatch.Reset();
            stopwatch.Start();

            graphData_throttle.Add(throttle_application);

            stopwatch.Stop();
            elapsed_buff_add = stopwatch.Elapsed;
        }

        internal void updateBrake(float brake_application)
        {

            stopwatch.Reset();
            stopwatch.Start();

            graphData_brake.Add(brake_application);

            stopwatch.Stop();
            elapsed_buff_add = stopwatch.Elapsed;   
        }
    }
}
