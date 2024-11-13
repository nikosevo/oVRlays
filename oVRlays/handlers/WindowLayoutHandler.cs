using oVRlays.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace oVRlays.handlers
{


    internal class WindowLayoutHandler
    {
        private Dictionary<WindowType, WindowSettings> layout { get; set; }

        public WindowLayoutHandler()
        {
            layout = new Dictionary<WindowType, WindowSettings>();
            loadLayoutFromFile();
        }
        public void saveWindowPosition(OverlayWindow win)
        {
            var settings = new WindowSettings
            {
                WindowType = win.getWindowsType(),
                Left = win.Left,
                Top = win.Top,
                Width = win.Width,
                Height = win.Height,
                WindowState = win.WindowState
            };

            layout[win.getWindowsType()] = settings;

            string json = JsonSerializer.Serialize(layout);
            File.WriteAllText("appSettings.json", json);
        }

        private void loadLayoutFromFile()
        {
            // Check if the file exists
            if (File.Exists("appSettings.json"))
            {
                // Read JSON data
                string json = File.ReadAllText("appSettings.json");

                // Deserialize into a temporary dictionary
                var deserializedSettings = JsonSerializer.Deserialize<Dictionary<WindowType, WindowSettings>>(json);

                if (deserializedSettings != null)
                {
                    // Add each entry to overlaySettingsDictionary
                    foreach (var entry in deserializedSettings)
                    {
                        layout[entry.Key] = entry.Value;
                    }
                }
            }
        }

        public Dictionary<WindowType,WindowSettings> getLayout()
        {
            return layout;
        }

        public WindowSettings isWindowSet(WindowType type)
        {
            if (layout.ContainsKey(type))
            {
                return layout[type];
            }
            return null;
        }
    }

}
