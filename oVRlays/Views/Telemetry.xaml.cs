using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private DataProvider dataProvider;

        private LiveGraph liveGraph;



        public Telemetry(DataProvider dataProvider)
        {
            InitializeComponent();
            this.dataProvider = dataProvider;

            liveGraph = new LiveGraph();
            dataProvider.TelemetryUpdated += UpdateGraphWithTelemetryData;

            
        }

        private void UpdateGraphWithTelemetryData(telemetryDataStruct data)
        {
            liveGraph.updateSpeed(data.speed);
        }
        

        private float NormalizeValue(float value,float minValue,float maxValue)
        {
            return (value - 0) / (maxValue - minValue);
        }
           
    }
}
