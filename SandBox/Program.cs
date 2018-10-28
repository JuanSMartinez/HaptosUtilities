using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaptosUtilities;
using System.Threading;

namespace SandBox
{
    class Program
    {

        //Test the stop watches
        private void TestTImeUtilities()
        {
            TimeUtilities.Instance.StartSession(TimeUtilities.sessions.trainig);
            TimeUtilities.Instance.StartSession(TimeUtilities.sessions.trainig);
            //Thread sleep
            Thread.Sleep(1000);
            Console.WriteLine("Session lasted: " + TimeUtilities.Instance.StopSession(TimeUtilities.sessions.trainig));

            TimeUtilities.Instance.StartSession(TimeUtilities.sessions.trainig);
            //Thread sleep
            Thread.Sleep(1500);
            Console.WriteLine("Session lasted: " + TimeUtilities.Instance.StopSession(TimeUtilities.sessions.trainig));

            Console.Read();
          
        }


        static void Main(string[] args)
        {
            Program program = new Program();
            program.TestTImeUtilities();
        }
    }
}
