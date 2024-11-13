using oVRlays.handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oVRlays.providers
{
    internal class DummyProvider : DataProvider
    {
        private bool providing = true;
        private SimData simData;

        public DummyProvider(SimData simData)
        {
            ProvideTelemetry();
            this.simData = simData;
        }

        void ProvideTelemetry()
        {

            //throttle and brake
            Thread providingThread = new Thread(new ThreadStart(readData));
            providingThread.IsBackground = true;
            providingThread.Start();
            

        }


        private void readData()
        {
            float speed, brake;
            try
            {
                while (providing)
                {
                    //we could also make it 
                    speed = (float) new Random().NextDouble();
                    brake = (float) new Random().NextDouble();
                    simData.updateTelemetry(speed, brake);
                    Thread.Sleep(50); // Delays for 100 ms

                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                //idk close connection maybe when we start implementing the actual thing
            }
        }
    }
}
