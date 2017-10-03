using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf_36._1._2
{
    public static class Progress
    {
        public static int total { get; set; }

        private static int _progress;
        public static int progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                //UpdateProgressbar();
            }
        }
        //from https://stackoverflow.com/questions/24918768/progress-bar-in-console-application
        private static void UpdateProgressbar()
        {
            Console.CursorVisible = false;
            Console.CursorLeft = 0;
            Console.Write("[");
            Console.CursorLeft = 32;
            Console.Write("]");
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.CursorLeft = position++;
                Console.Write("=");
            }
            for (int i = position; i <= 31; i++)
            {
                Console.CursorLeft = position++;
                Console.Write(" ");
            }
            Console.CursorLeft = 33;
            double percent = (Convert.ToDouble(progress) / Convert.ToDouble(total)) * 100;
            Console.Write(" {0}%", percent.ToString("N0").PadLeft(2));
        }
    }
}
