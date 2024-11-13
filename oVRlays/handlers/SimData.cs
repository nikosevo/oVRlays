using Microsoft.VisualStudio.RpcContracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oVRlays.handlers
{
    public struct telemetryDataStruct
    {
        public float throttle_application;
        public float brake_application;
    }
    
    public class SimData
    {

        public delegate void TelemetryUpdatedEventHandler(telemetryDataStruct data);
        public event TelemetryUpdatedEventHandler TelemetryUpdated;

        public telemetryDataStruct _telemetry;
        public telemetryDataStruct Telemetry => _telemetry;
        public SimData()
        {

        }

        public void updateTelemetry(float throttle_application,float brake_application)
        {
            _telemetry = new telemetryDataStruct
            {
                throttle_application = throttle_application,
                brake_application = brake_application
            };
            TelemetryUpdated?.Invoke(_telemetry);
            Thread.Sleep(10);
        }
    }
}
