using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Logger
    {
        private string filePath = @"C:\Users\sasha\source\repos\dpassignment no copy on git\dpBackEnd\test.txt";


        public void Log(string Message)
        {
            StreamWriter sr = new StreamWriter(filePath, true);
            sr.WriteLine(Message + " @ " + DateTime.Now.ToString() + "\n");
            sr.Close();
        }
    }
}
