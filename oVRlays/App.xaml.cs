using Akavache;
using System.Configuration;
using System.Data;
using System.Windows;

namespace oVRlays
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            BlobCache.ApplicationName = "oVRlays";
        }
    }

}
