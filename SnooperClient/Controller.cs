using System;
using System.Configuration;
using System.IO.Ports;

namespace SnooperClient
{
    public class Controller
    {
        public void controller(SerialPort com, ControlParamViewModel command)
        {
            var motion = ConfigurationManager.AppSettings[string.Format(command.direction ? "t{0}" : "f{0}", command.button)];
            if (motion == null)
            {
                Console.WriteLine("Invalid command recieved");
                motion = "8";
            }
            
            Console.WriteLine(motion);
            com.Open();
            byte[] buffer = new byte[] { Convert.ToByte(motion) };
            com.Write(buffer, 0, 1);
            //_com.Write(motion);
            com.Close();
        }
    }
}
