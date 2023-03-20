using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Sutter_Farmer_Game
{
    class Info
    {
        public void DisplayInfo(string assignment)
        {
            const string barrier = "***********************************************" +
                "************************************";
            WriteLine(barrier);
            WriteLine("Name:\t\t Samantha Sutter");
            WriteLine("Instructor:\t Janese Christie");
            WriteLine("Assignment:\t " + assignment);
            WriteLine("Date:\t\t " + DateTime.Today.ToShortDateString());
            WriteLine(barrier);
        }
    }
}
