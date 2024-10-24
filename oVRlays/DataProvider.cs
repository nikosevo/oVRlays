using oVRlays.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LiveCharts.Wpf;

namespace oVRlays
{
    public struct telemetryDataStruct
    {
        public float speed;
        public float rpm;
        public float throttle_application;
        public float brake_application;
    }

    public class DataProvider
    {

        public delegate void TelemetryUpdatedEventHandler(telemetryDataStruct data);
        public event TelemetryUpdatedEventHandler TelemetryUpdated;


        private bool isRunning = true;
        public telemetryDataStruct _telemetry;
        public telemetryDataStruct Telemetry => _telemetry;
        private UdpClient udpClient;
        private IPEndPoint endPoint;

        public DataProvider()
        {
            initConnection();
        }
        public telemetryDataStruct getTelemetry()
        {
            return _telemetry;
        }
        private void initConnection()
        {
            //ReadUdpData();
            udpClient = new UdpClient(12345);
            endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

        }
        // Function to stop reading data
        public void StopReading()
        {
            isRunning = false;
        }
        public void StartReading()
        {
            isRunning = true;
            Thread udpThread = new Thread(new ThreadStart(ReadUdpData));
            udpThread.IsBackground = true;
            udpThread.Start();
        }
        // The thread function that reads data
        private void ReadUdpData()
        {


            try
            {
                while (isRunning)
                {
                    // Receive data from the UDP server
                    byte[] data = udpClient.Receive(ref endPoint);
                    // Deserialize the data
                    float _speed = BitConverter.ToSingle(data, 0);      // 4 bytes for speed
                    float _rpm = BitConverter.ToSingle(data, 4);        // 4 bytes for rpm
                    float _throttle = BitConverter.ToSingle(data, 8);   // 4 bytes for throttle
                    float _brake = BitConverter.ToSingle(data, 12);     // 4 bytes for brake

                    // Parse the data and fill the telemetry struct
                    // Example parsing, you will have to adjust based on your data format
                    _telemetry = new telemetryDataStruct
                    {
                        speed = _speed,
                        rpm = _rpm,
                        throttle_application =_throttle,
                        brake_application = _brake
                    };

                    TelemetryUpdated?.Invoke(_telemetry);


                    // Add any other fields and processing as needed

                    // Print to check if it works
                    //Debug.WriteLine($"Speed: {_telemetry.speed}, RPM: {_telemetry.rpm}, throtle: {_telemetry.throttle_application}, Brake {_telemetry.brake_application}");

                    // Optionally sleep the thread to prevent high CPU usage
                    //Thread.Sleep(10); // 10 ms for example
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                udpClient.Close();
            }
        }
    }

}
