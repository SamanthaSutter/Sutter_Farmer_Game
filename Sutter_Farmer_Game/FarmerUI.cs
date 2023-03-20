using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
namespace Sutter_Farmer_Game
{
    class FarmerUI
    {
        //There was no ProcessChoice method in the UML so it was not included in this App

        Farmer FarmerGuy = new Farmer();

        public FarmerUI() { }

        public void DisplayGameState()
        {
            DisplayNorthBank();
            DisplayRiver();
            DisplaySouthBank();
            WriteLine("\nThe farmer is on the {0} bank of the river.", FarmerGuy.TheFarmer);
        }

        public void DisplayNorthBank()
        {
            BackgroundColor = ConsoleColor.Green;
            ForegroundColor = ConsoleColor.DarkGreen;
            SetCursorPosition(0, 7);
            WriteLine("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            WriteLine("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            WriteLine("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            WriteLine("******************************* North Bank *************************************");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < FarmerGuy.NorthBank.Count; i++)
            {
                Write(FarmerGuy.NorthBank[i]);
                Write("  ");
            }
        }

        public void DisplayRiver()
        {
            BackgroundColor = ConsoleColor.Blue;
            ForegroundColor = ConsoleColor.DarkBlue;
            WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
        }

        public void DisplaySouthBank()
        {
            for (int i = 0; i < FarmerGuy.SouthBank.Count; i++)
            {
                Write(FarmerGuy.SouthBank[i]);
                Write("  ");
            }
            BackgroundColor = ConsoleColor.Green;
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("\nVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            WriteLine("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            WriteLine("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            WriteLine("******************************* South Bank *************************************");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
        }

        public void DisplayWelcome()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine("\n\n\n\tThis is the Farmer Game.  The object of the game");
            WriteLine("\tis to get the farmer, fox, chicken, and grain to the other");
            WriteLine("\tside of the river.  But hold on, not so fast.  If the farmer");
            WriteLine("\tleaves the chicken and grain on either side of the river alone,");
            WriteLine("\tthe chicken will eat the grain and that is not good.  If the");
            WriteLine("\tfarmer leaves the fox and chicken on any side of the river alone,");
            WriteLine("\tthe fox will eat the chicken.  That is also not good.  In either case");
            WriteLine("\tyou lose the game.  If you get the farmer, the chicken,");
            WriteLine("\tthe fox, and the grain safely across the river and intact, you win");
            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\n\nPress any key when you are ready to start this thought provoking game");
            ReadKey();
            Clear();
        }

        public bool Play(int intro)
        {
            if (intro < 1) { DisplayWelcome(); }
            DisplayGameState();
            return PromptForMove();
        }

        public void PlayGame()
        {
            bool RunGame = true;
            int count = 0;

            while (RunGame)
            {
                RunGame = Play(count);
                count++;
            }
        }

        public bool PromptForMove()
        {
            string tempString = "";
            string stringChoice = "";
            bool choiceError = true;
            //0: keep going, 1: win, 4: Fox ate Chicken, 8: Chicken ate Grain
            int outcome = 0;

            Write("\nChoose next item for the farmer.  If you choose nothing, just hit the enter key ");
            stringChoice = ReadLine();
            if (stringChoice == "")
            {
                outcome = FarmerGuy.Move(stringChoice);
                choiceError = false;
            }
            else if (FarmerGuy.TheFarmer == Direction.North)
            {
                for (int i = 0; i < FarmerGuy.NorthBank.Count; i++)
                {
                    if (stringChoice.ToUpper() == FarmerGuy.NorthBank[i])
                    {
                        outcome = FarmerGuy.Move(stringChoice.ToUpper());
                        choiceError = false;
                        stringChoice = "";
                    }
                }
            }
            else if (FarmerGuy.TheFarmer == Direction.South)
            {
                for (int i = 0; i < FarmerGuy.SouthBank.Count; i++)
                {
                    if (stringChoice.ToUpper() == FarmerGuy.SouthBank[i])
                    {
                        outcome = FarmerGuy.Move(stringChoice.ToUpper());
                        choiceError = false;
                        stringChoice = "";
                    }
                }
            }

            Clear();
            if (choiceError)
            {
                WriteLine("\nThat item is not on this side of the river");
                WriteLine("Please try again");
                return true;
            }
            else if (outcome == 1)
            {
                WriteLine("\n\n\nYou have successfully completed the game!!");
                WriteLine("CONGRATULATIONS");
                Write("\n\n\nWould you like to play again? ");
                tempString = ReadLine();
                if (tempString != "" && tempString.ToUpper()[0] == 'Y')
                {
                    Clear();
                    return true;
                }
                else { return false; }
            }
            else if (outcome == 4)
            {
                WriteLine("\n\n\n\n\n\nOh No! The Fox Ate the Chicken!!");
                WriteLine("YOU LOSE");
                Write("\n\n\nWould you like to play again? ");
                tempString = ReadLine();
                if (tempString != "" && tempString.ToUpper()[0] == 'Y')
                {
                    Clear();
                    return true;
                }
                else { return false; }
            }
            else if (outcome == 8)
            {
                WriteLine("\n\n\n\n\n\nOh No! The Chicken Ate the Grain!!");
                WriteLine("YOU LOSE");
                Write("\n\n\nWould you like to play again? ");
                tempString = ReadLine();
                if (tempString != "" && tempString.ToUpper()[0] == 'Y')
                {
                    Clear();
                    return true;
                }
                else { return false; }
            }
            else { return true; }
        }
    }
}
