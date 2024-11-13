using IRSDKSharper;
using oVRlays.handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace oVRlays.providers
{
    class IracingProvider : DataProvider
    {
        private bool connected = true;
        private SimData simData;
        private IRacingSdk irsdk;

        public IracingProvider(SimData simData)
        {
            this.simData = simData;
            irsdk = new IRacingSdk();
            irsdk.OnTelemetryData += updateTelemetry;

            //irsdk.UpdateInterval = 30;
            irsdk.Start();


        }
        private void updateTelemetry()
        {
       
            //var braking = irsdk.Data.TelemetryDataProperties["BrakeRaw"];
            var brake = irsdk.Data.GetFloat("BrakeRaw");
            var throttle = irsdk.Data.GetFloat("ThrottleRaw");

            simData.updateTelemetry(throttle, brake);           
        }
        
    }
}
