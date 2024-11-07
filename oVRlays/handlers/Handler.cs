﻿using oVRlays.providers;
using oVRlays.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oVRlays.handlers
{

    public class Handler
    {
        private List<OverlayWindow> activeWindows = new List<OverlayWindow>();
        private SimData simData;
        
        
        public Handler()
        {
            Console.WriteLine("welcome");

            simData = new SimData();

            //todo: create new provider based on the sim,
            //currently only use Dummy
            new DummyProvider(simData);

        }

        public void activateTelemetry()
        {
            //create a new windows that will display the telemetry

            //beggin all relative processes eg. ACCProvider.startTelemetry();

            //add windows to active windows to handle locking,resizing etc...
            activeWindows.Add(new OverlayWindow(simData, WindowType.Telemetry));
        }
    }
}
