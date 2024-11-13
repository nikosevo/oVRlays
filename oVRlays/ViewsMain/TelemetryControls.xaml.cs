using oVRlays.handlers;
using System;
using System.Collections.Generic;
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

namespace oVRlays.ViewsMain
{
    /// <summary>
    /// Interaction logic for TelemetryControls.xaml
    /// </summary>
    public partial class TelemetryControls : Page
    {

        private Handler handler;
        public TelemetryControls(Handler handler)
        {

            InitializeComponent();
            this.handler = handler;
        }

        private void telemetry_Checked(object sender, RoutedEventArgs e)
        {

            handler.activateWindow(WindowType.Telemetry);
        }

        private void telemetry_Unchecked(object sender, RoutedEventArgs e)
        {
            handler.deactivateWindow(WindowType.Telemetry);

        }
    }
}
