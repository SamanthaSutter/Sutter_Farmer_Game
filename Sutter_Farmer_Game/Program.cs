using System;

namespace Sutter_Farmer_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating assignment name variable
            string assign = "Assignment 6 - Farmer Game";

            //Creating info object
            Info info = new Info();
            //Creating FarmerUI object
            FarmerUI farmer = new FarmerUI();

            //Displaying class and assignment information to user
            info.DisplayInfo(assign);
            //Running application
            farmer.PlayGame();
        }
    }
}
