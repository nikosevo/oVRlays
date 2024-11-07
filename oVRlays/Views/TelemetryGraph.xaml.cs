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

namespace oVRlays.Views
{
    /// <summary>
    /// Interaction logic for TelemetryGraph.xaml
    /// </summary>
    public partial class TelemetryGraph: UserControl, IOverlay
    {
        private handlers.SimData simData;
        public TelemetryGraph(SimData simData)
        {
            InitializeComponent();
            this.simData = simData;
        }
    }
}
