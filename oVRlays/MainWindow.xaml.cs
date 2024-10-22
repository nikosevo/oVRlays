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
        public MainWindow()
        {
            InitializeComponent();
            LoadDynamicTabs(SubTabControlAC, "Assetto Corsa");
            LoadDynamicTabs(SubTabControlACC, "Assetto Corsa Competizione");
            LoadDynamicTabs(SubTabControlIRacing, "iRacing");
        }
        private void LoadDynamicTabs(TabControl subTabControl, string gameName)
        {
            // Set the path to your Views directory
            string viewsDirectory = "D:\\desktop\\oVRlays\\oVRlays\\Views";

            if (Directory.Exists(viewsDirectory))
            {
                // Get all files in the Views directory
                string[] files = Directory.GetFiles(viewsDirectory);

                foreach (string file in files)
                {
                    if (file.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                        continue;
                    // Use the file name as the tab header
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    TabItem tabItem = new TabItem
                    {
                        Header = fileName,
                        Content = new TextBlock
                        {
                            Text = $"Content for {fileName} in {gameName}",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    };
                    subTabControl.Items.Add(tabItem);
                }
            }
            else
            {
                MessageBox.Show("Views directory not found.");
            }
        }
    }
}