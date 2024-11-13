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
using System.Windows.Shapes;

namespace oVRlays.Views
{
    /// <summary>
    /// Interaction logic for OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {
        handlers.WindowType winType;            //this will let the class know what type of window we are running
        private handlers.SimData simData;
        private IOverlay overlay;
        public OverlayWindow(handlers.SimData simData, handlers.WindowType winType)
        {
            InitializeComponent();
            this.simData = simData;
            this.winType = winType;

            initContent();
        }
        private void initContent()
        {

            //use a switch case when its time to actually work with it
            if(winType == handlers.WindowType.Telemetry)
            {
                overlay = new TelemetryGraph(simData);
                this.Content = overlay;
            }
        }
        //add all the resize, drag lock z-index etc functions

    }
}
