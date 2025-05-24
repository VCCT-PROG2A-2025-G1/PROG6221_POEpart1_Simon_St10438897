using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Channels;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;




namespace POEpart1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            CyberChat.PlaySound("IntroSound.wav");
            CyberChat.Greeting();
            CyberChat.Cyperchat();
        }
    }
}
