using oVRlays.handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace oVRlays.Views
{
    /// <summary>
    /// Interaction logic for OverlayWindow.xaml
    /// </summary>
    /// 
    public partial class OverlayWindow : Window
    {
        private bool locked = false;
        
        handlers.WindowType winType;            //this will let the class know what type of window we are running
        private handlers.SimData simData;
        private IOverlay overlay;

        public OverlayWindow() { }
        public OverlayWindow(handlers.SimData simData, handlers.WindowType winType)
        {
            InitializeComponent();
            this.simData = simData;
            this.winType = winType;
            this.Topmost = true;
            initContent();


        }

        public void toggleWindowLock(bool locked)
        {
            this.locked = locked;
            lockSize(locked);
        }
        public handlers.WindowType getWindowsType() {  return this.winType; }
        private void initContent()
        {

            //use a switch case when its time to actually work with it
            if(winType == handlers.WindowType.Telemetry)
            {
                overlay = new TelemetryGraph(simData);

            }
        }
        //add all the resize, drag lock z-index etc functions


        //make it dragable
        private void mouseDown(object sender, MouseButtonEventArgs e)
        {
            if (locked)
            {
                return;
            }
            // Check if the left mouse button is pressed
            if (e.ChangedButton == MouseButton.Left)
            {
                //make it so its not resizable when its dragable
                lockSize(true);

                // Begin dragging the window
                this.DragMove();


            }
            lockSize(false);
        }
        private void lockSize(bool lockit)
        {
            if (lockit)
            {
                this.ResizeMode = ResizeMode.NoResize;          
            }
            else
            {
                this.ResizeMode = ResizeMode.CanResize;       
            }
            this.UpdateLayout();
        }

        public handlers.WindowSettings getPosition()
        {

            var settings = new handlers.WindowSettings
            {
                Left = this.Left,
                Top = this.Top,
                Width = this.Width,
                Height = this.Height,
                WindowState = this.WindowState,
                WindowType = this.winType
            };

            return settings;

        }

        public void setPosition(handlers.WindowSettings settings)
        {
            if (settings != null)
            {
                // Apply the loaded settings
                this.Left = settings.Left;
                this.Top = settings.Top;
                this.Width = settings.Width;
                this.Height = settings.Height;
                this.WindowState = settings.WindowState;
            }         
        }
    }
}
