
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

        DataProvider_old dataProvider;

        private oVRlays.Views.Telemetry _telemetry;
        private handlers.Handler handler;


        public MainWindow()
        {
            InitializeComponent();
            handler = new handlers.Handler();

            //dataProvider = new DataProvider_old();
            //StartDataUpdateLoop();

        }
     
        private void telemetry_Checked(object sender, RoutedEventArgs e)
        {

            handler.activateTelemetry();
            // Open the new window when the toggle is checked
            //if (_telemetry == null)
            //{
            //    //dataProvider.StartReading();
            //    Task.Run(() => dataProvider.StartReading()); // Start the telemetry reading in a background thread

            //    _telemetry = new oVRlays.Views.Telemetry(dataProvider);
            //    _telemetry.Show();
            //    _telemetry.Closed += (s, args) =>
            //    {
            //        // Handle the window being closed manually
            //        _telemetry = null;
            //        telemetry.IsChecked = false; // Reset toggle state
            //        dataProvider.StopReading();

            //    };
            //}
        }

        private void telemetry_Unchecked(object sender, RoutedEventArgs e)
        {
            handler.deactivateTelemetry();
        
        }
        private void windows_lock(object sender,RoutedEventArgs e)
        {
            handler.lockWindows();
        }
        private void windows_unlock(object sender, RoutedEventArgs e)
        {
            handler.unlockWindows();
        }
    }
}
