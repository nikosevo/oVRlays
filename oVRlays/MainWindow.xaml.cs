
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace oVRlays
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// lets act here like i know what i am doing :) 
    public partial class MainWindow : Window
    {

        DataProvider dataProvider;

        private oVRlays.Views.Telemetry _telemetry;


        public MainWindow()
        {
            InitializeComponent();
            dataProvider = new DataProvider();
            //StartDataUpdateLoop();

        }
        //private void StartDataUpdateLoop()
        //{
        //    var timer = new System.Windows.Threading.DispatcherTimer
        //    {
        //        Interval = TimeSpan.FromMilliseconds(100) // Update interval
        //    };
        //    timer.Tick += (sender, e) =>
        //    {
        //        UpdateUI();
        //    };
        //    timer.Start();
        //}

        //private void UpdateUI()
        //{
        //    // Access the telemetry data and update the TextBox
        //    telemetryDataStruct data = this.dataProvider.getTelemetry();
        //    OutputTextBox.AppendText($"Speed: {data.speed}, RPM: {data.rpm}, throtle: {data.throttle_application}\n");
        //    //OutputTextBox.AppendText("so something");


        //    OutputTextBox.ScrollToEnd(); // Auto-scroll to the latest entry
        //}
        private void telemetry_Checked(object sender, RoutedEventArgs e)
        {
            // Open the new window when the toggle is checked
            if (_telemetry == null)
            {
                //dataProvider.StartReading();
                Task.Run(() => dataProvider.StartReading()); // Start the telemetry reading in a background thread

                _telemetry = new oVRlays.Views.Telemetry(dataProvider);
                _telemetry.Show();
                _telemetry.Closed += (s, args) =>
                {
                    // Handle the window being closed manually
                    _telemetry = null;
                    telemetry.IsChecked = false; // Reset toggle state
                    dataProvider.StopReading();

                };
            }
        }

        private void telemetry_Unchecked(object sender, RoutedEventArgs e)
        {
            // Close the window when the toggle is unchecked
            if (_telemetry != null)
            {
                _telemetry.Close();
                _telemetry = null;
                dataProvider.StopReading();
            }
        }
    }
}
