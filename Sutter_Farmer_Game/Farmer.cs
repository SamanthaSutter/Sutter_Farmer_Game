using System;
using System.Collections.Generic;
using System.Text;

namespace Sutter_Farmer_Game
{
    enum Direction { North, South };
    class Farmer
    {
        //Nested Type Enum
        private Direction farmer;
        private List<string> northBank = new List<string>();
        private List<string> southBank = new List<string>();

        //Properties
        public Direction theFarmer { get { return farmer; } set { farmer = value; } }
        public List<string> NorthBank { get { return northBank; } set { northBank = value; } }
        public List<string> SouthBank { get { return southBank; } set { southBank = value; } }


        public Farmer()
        {
            northBank.Add("FOX");
            northBank.Add("CHICKEN");
            northBank.Add("GRAIN");
            farmer = Direction.North;
        }

        //Checking to see if an animal has eaten anything.
        public int AnimalAteFood()
        {
            int num = 0;

            if (farmer == Direction.North && southBank.Count > 1)
            {
                for (int i = 0; i < southBank.Count; i++)
                {
                    if (southBank[i] == "FOX") { num = num + 1; }
                    if (southBank[i] == "CHICKEN") { num = num + 3; }
                    if (southBank[i] == "GRAIN") { num = num + 5; }
                }
            }
            else if (farmer == Direction.South && northBank.Count > 1)
            {
                for (int i = 0; i < northBank.Count; i++)
                {
                    if (northBank[i] == "FOX") { num = num + 1; }
                    if (northBank[i] == "CHICKEN") { num = num + 3; }
                    if (northBank[i] == "GRAIN") { num = num + 5; }
                }
            }

            if (DetermineWin()) { return 1; }
            else if (num == 4 || num == 8) { return num; }
            else if (farmer == Direction.South && northBank.Count == 3 && num == 9) { return 4; }
            else { return 0; }
        }

        //Above would have been a lot cleaner, but I wanted to stay true to the UML
        //and use the below method.
        public bool DetermineWin()
        {
            int num = 0;

            for (int i = 0; i < southBank.Count; i++)
            {
                if (southBank[i] == "FOX") { num = num + 1; }
                if (southBank[i] == "CHICKEN") { num = num + 3; }
                if (southBank[i] == "GRAIN") { num = num + 5; }
            }

            if (num == 9) { return true; }
            else { return false; }
        }

        //User input for what is moving with farmer
        public int Move(string move)
        {
            int num = 0;

            //0: keep going, 1: win, 4: Fox ate Chicken, 8: Chicken ate Grain
            //Moving farmer, if on north bank moving farmer to south, if south bank farmer moves north
            if (move == "")
            {
                if (farmer == Direction.North)
                {
                    farmer = Direction.South;
                }

                else if (farmer == Direction.South) 
                {    
                    farmer = Direction.North; 
                }
            }
            else if (farmer == Direction.North)
            {
                northBank.Remove(move.ToUpper());
                southBank.Add(move.ToUpper());
                farmer = Direction.South;
            }
            else if (farmer == Direction.South)
            {
                southBank.Remove(move.ToUpper());
                northBank.Add(move.ToUpper());
                farmer = Direction.North;
            }

            num = AnimalAteFood();
            if (num > 0)
            {
                northBank.Clear();
                southBank.Clear();
                farmer = Direction.North;
                northBank.Add("FOX");
                northBank.Add("CHICKEN");
                northBank.Add("GRAIN");
            }
            return num;
        }
    }
}
