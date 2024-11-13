
using oVRlays.handlers;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace oVRlays
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// lets act here like i know what i am doing :) 
    public partial class MainWindow : Window
    {

        DataProvider_old dataProvider;

        private oVRlays.Views.Telemetry _telemetry;
        private handlers.Handler handler;


        public MainWindow()
        {
            InitializeComponent();
            handler = new handlers.Handler();

            //dataProvider = new DataProvider_old();
            //StartDataUpdateLoop();

        }
     

        private void windows_lock(object sender,RoutedEventArgs e)
        {
            handler.lockWindows();
        }
        private void windows_unlock(object sender, RoutedEventArgs e)
        {
            handler.unlockWindows();
        }

        private void TabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabList.SelectedItem is ListBoxItem selectedItem)
            {
                // Load the content based on the selected tab's Tag
                switch (selectedItem.Tag)
                {
                    case "Telemetry":
                        TabContent.Content = new ViewsMain.TelemetryControls(handler);
                        break;
                    case "Relative":
                        //TabContent.Content = new RelativeControl();
                        break;
                }
            }
        }
    }
}
