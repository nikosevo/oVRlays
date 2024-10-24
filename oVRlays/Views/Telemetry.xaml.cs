using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using LiveCharts;

namespace oVRlays.Views
{
    /// <summary>
    /// Interaction logic for Telemetry.xaml
    /// </summary>
    public partial class Telemetry : Window
    {

        public ChartValues<float> SpeedValues { get; set; }
        public ChartValues<float> RpmValues { get; set; }
        public ChartValues<float> ThrottleValues { get; set; }
        public ChartValues<float> BrakeValues { get; set; }

        private DataProvider dataProvider;



        public Telemetry(DataProvider dataProvider)
        {
            InitializeComponent();
            this.dataProvider = dataProvider;

            SpeedValues = new ChartValues<float>();
            RpmValues = new ChartValues<float>();
            ThrottleValues = new ChartValues<float>();
            BrakeValues = new ChartValues<float>();

            //// Bind the data
            DataContext = this;
            // Subscribe to the telemetry updated event
            dataProvider.TelemetryUpdated += OnTelemetryUpdated;
            // Start updating the graph
            //StartGraphing();
        }
        private void OnTelemetryUpdated(telemetryDataStruct data)
        {
            // Ensure the UI updates occur on the UI thread
            Dispatcher.Invoke(() =>
            {
                SpeedValues.Add(NormalizeValue(data.speed,0,300));
                RpmValues.Add(NormalizeValue(data.rpm,0,6000));
                ThrottleValues.Add(data.throttle_application);
                BrakeValues.Add(data.brake_application);

                // Optional: Limit the number of points
                if (SpeedValues.Count > 100)
                {
                    SpeedValues.RemoveAt(0);
                    RpmValues.RemoveAt(0);
                    ThrottleValues.RemoveAt(0);
                    BrakeValues.RemoveAt(0);
                }

            });
        }

        private float NormalizeValue(float value,float minValue,float maxValue)
        {
            return (value - 0) / (maxValue - minValue);
        }
        //private void StartGraphing()
        //{
        //    DispatcherTimer timer = new DispatcherTimer();
        //    timer.Interval = TimeSpan.FromMilliseconds(10); // Update every 100ms
        //    timer.Tick += UpdateGraph;
        //    timer.Start();
        //}
        //private void UpdateGraph(object sender, EventArgs e)
        //{
        //    telemetryDataStruct telemetry = dataProvider.getTelemetry(); // Get latest telemetry data

        //    SpeedValues.Add(telemetry.speed);
        //    RpmValues.Add(telemetry.rpm);
        //    ThrottleValues.Add(telemetry.throttle_application);
        //    BrakeValues.Add(telemetry.brake_application);

        //    //// Keep chart manageable by limiting the number of points
        //    if (SpeedValues.Count > 100)
        //    {
        //        SpeedValues.RemoveAt(0);
        //        RpmValues.RemoveAt(0);
        //        ThrottleValues.RemoveAt(0);
        //        BrakeValues.RemoveAt(0);
        //    }
        //}
    }
}
