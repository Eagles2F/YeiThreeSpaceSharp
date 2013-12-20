﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEISensorLib.RawApi;
using YEISensorLib.Sharped;

namespace YEISensor.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var device = SensorDevices.GetFirstAvailable())
            {
                device.Tare();

                Console.WriteLine("Port:            {0}", device.PortName);
                Console.WriteLine("Friendly Name:   {0}", device.FriendlyPortName);
                Console.WriteLine("Sensor Type:     {0}", device.SensorType);
                Console.WriteLine("Connected:       {0}", device.IsConnected);
                Console.WriteLine("Serial:          {0}", device.SerialNumber);

                var line = string.Empty;
                while (line == string.Empty)
                {
                    var quatSuccess = device.GetQuaternion();
                    Console.WriteLine("Quaternion:  {0:0.000},{1:0.000},{2:0.000},{3:0.000}", device.Quaternion.W, device.Quaternion.X, device.Quaternion.Y, device.Quaternion.Z);

                    var eulerSuccess = device.GetEulerAngles();
                    Console.WriteLine("Euler:       {0:0.000},{1:0.000},{2:0.000}", device.Euler.X, device.Euler.Y, device.Euler.Z);

                    var sensorSuccess = device.GetNormalizedSensorData();
                    Console.WriteLine("Sensor data... {0} / {1}", sensorSuccess, device.TimeStamp);
                    Console.WriteLine("Gyro:        {0:0.000},{1:0.000},{2:0.000}", device.Gyro.X, device.Gyro.Y, device.Gyro.Z);
                    Console.WriteLine("Accel:       {0:0.000},{1:0.000},{2:0.000}", device.Accelerometer.X, device.Accelerometer.Y, device.Accelerometer.Z);
                    Console.WriteLine("Compass:     {0:0.000},{1:0.000},{2:0.000}", device.Compass.X, device.Compass.Y, device.Compass.Z);


                    var color = new Color
                                    {
                                        R = (device.Accelerometer.X + 1) / 2,
                                        G = (device.Accelerometer.Y + 1) / 2,
                                        B = (device.Accelerometer.Z + 1) / 2,
                                    };
                    device.SetLedColour(color);
                    color = device.GetLedColour();
                    Console.WriteLine("LED:     {0:0.000},{1:0.000},{2:0.000}", color.R, color.G, color.B);


                    line = Console.ReadLine();
                }

            }


            Console.ReadLine();

        }
    }
}
