using Microsoft.VisualStudio.ApplicationInsights.Channel;
using oVRlays.providers;
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
            //winLayoutHandler = new WindowLayoutHandler();
            //loadLayout();

            //todo: create new provider based on the sim,
            //currently only use Dummy
            //new DummyProvider(simData);
            new IracingProvider(simData);



        }

        public void activateWindow(WindowType wintype)
        {
            foreach(var window in activeWindows)
            {
                if (window.getWindowsType() == wintype) return;
            }
            //create a new windows that will display the telemetry
            OverlayWindow temp = new OverlayWindow(simData, wintype);
            //applySettings(temp,winLayoutHandler.isWindowSet(wintype));
            temp.Show();
            temp.Closed += (s, e) =>
            {
                temp = null;
            };
            activeWindows.Add(temp);
        }
        public void deactivateWindow(WindowType wintype)
        {
            OverlayWindow temp = new OverlayWindow() ;
            foreach (var window in activeWindows)
            {
                if(window.getWindowsType() == wintype)
                {
                    window.Close();
                    temp = window;
                }
            }
            if(temp != null)
            {
                activeWindows.Remove(temp);

            }
        }

        internal void lockWindows()
        {
            foreach (var win in activeWindows)
            {
                win.toggleWindowLock(true);
            }
        }

        internal void unlockWindows()
        {
            foreach (var win in activeWindows)
            {
                win.toggleWindowLock(false);
            }
        }
    }
}
