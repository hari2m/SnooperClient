﻿using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace SnooperClient
{
    public class Controller
    {
        public void controller(SerialPort _com, ControlParamViewModel param)
        {

            string motion;
            if (param.direction)
            {
                switch (param.button)
                {
                    case "38":
                        motion = "1";
                        break;
                    case "40":
                        motion = "3";
                        break;
                    case "37":
                        motion = "5";
                        break;
                    case "39":
                        motion = "7";
                        break;
                    default:
                        Console.WriteLine("Invalid command recieved");
                        motion = "8";
                        break;
                }
            }
            else if (!param.direction)
            {
                switch (param.button)
                {
                    case "38":
                        motion = "0";
                        break;
                    case "40":
                        motion = "2";
                        break;
                    case "37":
                        motion = "4";
                        break;
                    case "39":
                        motion = "6";
                        break;
                    default:
                        Console.WriteLine("Invalid command recieved");
                        motion = "8";
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid command recieved");
                motion = "1111";
            }
            Console.WriteLine(motion);
            _com.Open();
            byte[] buffer = new byte[] { Convert.ToByte(motion) };
            _com.Write(buffer, 0, 1);
            //_com.Write(motion);
            _com.Close();
        }
    }
}
