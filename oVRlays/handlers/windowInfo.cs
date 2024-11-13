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
        public bool active { get; set; }

        public float Left { get; set; }
        public float Top { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
        public WindowType type { get; set; } 
    }

}
