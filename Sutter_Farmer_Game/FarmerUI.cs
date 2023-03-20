using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
namespace Sutter_Farmer_Game
{
    class FarmerUI
    {
        //There was no ProcessChoice method in the UML so it was not included in this App

        Farmer farmer = new Farmer();

        //The game display. Showing both banks and the river
        public void DisplayGameState()
        {
            DisplayNorthBank();
            DisplayRiver();
            DisplaySouthBank();
            //Telling user which side of river the farmer is on. Getting which bank from farmer object
            WriteLine("\nThe farmer is on the {0} bank of the river.", farmer.theFarmer);
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
            for (int i = 0; i < farmer.NorthBank.Count; i++)
            {
                Write(farmer.NorthBank[i]);
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
            BackgroundColor = ConsoleColor.Green;
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine("\nVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            WriteLine("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            WriteLine("VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV");
            WriteLine("******************************* South Bank *************************************");
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < farmer.SouthBank.Count; i++)
            {
                Write(farmer.SouthBank[i]);
                Write("  ");
            }
        }

        public void DisplayWelcome()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine("\n\n\n\n");
            WriteLine("\tThis is the Farmer Game! The object of the game");
            WriteLine("\tis to get everything across the river! This means the farmer,");
            WriteLine("\tfox, chicken, and grain need to move to the other side of the river.");
            WriteLine("\tBut hold on, not so fast! If the farmer leaves the chicken");
            WriteLine("\tand grain on either side of the river alone,");
            WriteLine("\tthe chicken will eat the grain. That's not good. If the farmer");
            WriteLine("\tleaves the fox and chicken on any side of the river alone,");
            WriteLine("\tthe fox will eat the chicken. That also is not good. Either way");
            WriteLine("\tyou lose the game. If you can get the farmer, the chicken,");
            WriteLine("\tthe fox, and the grain safely across the river and intact, you win the game!");
            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\n\n\n");
            WriteLine("Please press any key when you are ready to start this game!");
            ReadKey();
            Clear();
        }

        public bool Play(int intro)
        {
            //Only displaying introduction to game once
            if (intro < 1) { DisplayWelcome(); }
            //Displaying banks, river and placement of actors
            DisplayGameState();
            //Returning boolean from movement to play again- if count is over 0 intro will not display again
            return PromptForMove();
        }

        //Starting game
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
            string input = "";
            string userChoice = "";
            bool choiceError = true;
            //Variable to hold number to find outcome of game
            //0 means user can keep going, 1 = win, 4= Fox ate Chicken, 8: Chicken ate Grain
            int outcome = 0;

            Write("\nChoose next item for the farmer.  If you choose nothing, just hit the enter key ");
            userChoice = ReadLine();
            if (userChoice == "")
            {
                outcome = farmer.Move(userChoice);
                choiceError = false;
            }
            else if (farmer.theFarmer == Direction.North)
            {
                for (int i = 0; i < farmer.NorthBank.Count; i++)
                {
                    if (userChoice.ToUpper() == farmer.NorthBank[i])
                    {
                        outcome = farmer.Move(userChoice.ToUpper());
                        choiceError = false;
                        userChoice = "";
                    }
                }
            }
            else if (farmer.theFarmer == Direction.South)
            {
                for (int i = 0; i < farmer.SouthBank.Count; i++)
                {
                    if (userChoice.ToUpper() == farmer.SouthBank[i])
                    {
                        outcome = farmer.Move(userChoice.ToUpper());
                        choiceError = false;
                        userChoice = "";
                    }
                }
            }

            Clear();
            if (choiceError)
            {
                WriteLine("\nThat item is not on that side of the river.");
                WriteLine("Please try again");
                return true;
            }
            //Display for if the user wins the game
            else if (outcome == 1)
            {
                WriteLine("\n\n\n");
                WriteLine("You have successfully completed the game!!");
                WriteLine("CONGRATULATIONS!");
                WriteLine("\n\n\n");
                Write("Would you like to play again? ");
                input = ReadLine();
                if (input != "" && input.ToUpper()[0] == 'Y')
                {
                    Clear();
                    return true;
                }
                else { return false; }
            }
            else if (outcome == 4)
            {
                WriteLine("\n\n\n\n\n");
                WriteLine("OH NO! The Fox Ate the Chicken!!");
                WriteLine("YOU LOSE!");
                Write("\n\n\n");
                Write("Would you like to the play again? ");
                input = ReadLine();
                if (input != "" && input.ToUpper()[0] == 'Y')
                {
                    Clear();
                    return true;
                }
                else { return false; }
            }
            else if (outcome == 8)
            {
                WriteLine("\n\n\n\n");
                WriteLine("Oh No! The Chicken Ate the Grain!!");
                WriteLine("YOU LOSE!");
                Write("\n\n\nWould you like to play again? ");
                input = ReadLine();
                if (input != "" && input.ToUpper()[0] == 'Y')
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
