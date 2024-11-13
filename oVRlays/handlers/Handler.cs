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
        private WindowLayoutHandler winLayoutHandler;
        
        
        public Handler()
        {
            Console.WriteLine("welcome");

            simData = new SimData();
            winLayoutHandler = new WindowLayoutHandler();
            loadLayout();

            //todo: create new provider based on the sim,
            //currently only use Dummy
            new DummyProvider(simData);



        }

        public void activateTelemetry()
        {
            //create a new windows that will display the telemetry
            OverlayWindow temp = new OverlayWindow(simData, WindowType.Telemetry);
            applySettings(temp,winLayoutHandler.isWindowSet(WindowType.Telemetry));
            temp.Show();
            temp.Closed += (s, e) =>
            {
                temp = null;
            };
            activeWindows.Add(temp);
        }
        public void deactivateTelemetry()
        {
            OverlayWindow temp = new OverlayWindow() ;
            foreach (var window in activeWindows)
            {
                if(window.getWindowsType() == WindowType.Telemetry)
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

        internal void saveLayout()
        {
            foreach(var win in activeWindows)
            {
                winLayoutHandler.saveWindowPosition(win);
            }
        }
        private void loadLayout()
        {
            var dictionary = winLayoutHandler.getLayout();
            if (dictionary == null) return;
            foreach (var entry in dictionary)
            {
                WindowType windowType = entry.Key;
                WindowSettings settings = entry.Value;

                OverlayWindow temp = new OverlayWindow(simData, entry.Key);
                temp.Left = settings.Left;
                temp.Top = settings.Top;
                temp.Width = settings.Width;
                temp.Height = settings.Height;
                temp.WindowState = settings.WindowState;
                temp.Show();
                temp.Closed += (s, e) =>
                {
                    temp = null;
                };
                activeWindows.Add(temp);
            }
        }

        private void applySettings(OverlayWindow win,WindowSettings settings)
        {
            win.Left = settings.Left;
            win.Top = settings.Top;
            win.Width = settings.Width;
            win.Height = settings.Height;  
            win.WindowState = settings.WindowState;
        }
    }
}
