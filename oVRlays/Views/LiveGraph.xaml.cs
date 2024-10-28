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

        private static double[] graphData_speed;
        private static double[] graphData_throttle;
        private static double[] graphData_brake;


        public LiveGraph()
        {
            InitializeComponent();
            // Set the timer interval to 60 ms
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += (sender,args) => RenderGraph(); // Subscribe to the Tick event


            //initiate the graphData arrays
            graphData_speed =  new double[bufferSize];
            graphData_throttle = new double[bufferSize];
            graphData_brake = new double[bufferSize];

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


            Polyline throtle = new Polyline
            {
                Stroke = Brushes.LimeGreen,
                StrokeThickness = 2
            };
            Polyline brake = new Polyline
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };

            for (int i = 0; i < bufferSize-1; i++)
            {

                //polyline.Points.Add(new Point(i * 10, new Random().NextDouble() * 300));
                throtle.Points.Add(new Point(i*stepX, y - y* graphData_throttle[i]));
                brake.Points.Add(new Point(i*stepX,y-y* graphData_brake[i]));
            }
            // Add the polyline to the canvas
            graphCanvas.Children.Add(throtle);
            graphCanvas.Children.Add(brake);


        }

        public void updateSpeed(double speed)
        {
            for (int i = 0; i < bufferSize-1; i++)
            {
                graphData_speed[i] = graphData_speed[i + 1];
            }
            graphData_speed[bufferSize-1] = speed;
        }

        internal void updateThrottle(float throttle_application)
        {
            for (int i = 0; i < bufferSize - 1; i++)
            {
                graphData_throttle[i] = graphData_throttle[i + 1];
            }
            graphData_throttle[bufferSize - 1] = throttle_application;
        }

        internal void updateBrake(float brake_application)
        {
            for (int i = 0; i < bufferSize - 1; i++)
            {
                graphData_brake[i] = graphData_brake[i + 1];
            }
            graphData_brake[bufferSize - 1] = brake_application;
        }
    }
}
