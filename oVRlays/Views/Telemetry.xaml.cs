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

        private DataProvider_old dataProvider;

        private LiveGraph liveGraph;



        public Telemetry(DataProvider_old dataProvider)
        {
            InitializeComponent();
            this.dataProvider = dataProvider;

            liveGraph = new LiveGraph();
            dataProvider.TelemetryUpdated += UpdateGraphWithTelemetryData;

            
        }

        private void UpdateGraphWithTelemetryData(telemetryDataStruct data)
        {
            //todo readmax value from the data and use this as max value for speed....or dont even use speed in telemetry
            liveGraph.updateSpeed(NormalizeValue(data.speed,0,300));
            liveGraph.updateThrottle(data.throttle_application);
            liveGraph.updateBrake(data.brake_application);
        }
        

        private float NormalizeValue(float value,float minValue,float maxValue)
        {
            return (value - 0) / (maxValue - minValue);
        }
           
    }
}
