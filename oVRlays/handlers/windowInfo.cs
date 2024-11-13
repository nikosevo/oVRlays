using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace oVRlays.handlers
{
    public enum WindowType
    {
        none = 0,
        Telemetry,
        Relative

    }
    public class WindowSettings
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public WindowState WindowState { get; set; }
        public WindowType WindowType { get; set; }
    }

  
    
}
