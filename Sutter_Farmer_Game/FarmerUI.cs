using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Sutter_Farmer_Game
{
    class FarmerUI
    {
        //Creating farmer object
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
            string grass = "V";
            WriteLine("\n\n\n\n\n");
            //Setting background color to green and text color to darker green
            BackgroundColor = ConsoleColor.Green;
            ForegroundColor = ConsoleColor.DarkGreen;
            for (int i=0; i < 3; i++)
            {
                for (int x=0; x < 80; x++)
                {
                    Write(grass);
                }
                WriteLine();
            }
            WriteLine("******************************* North Bank *************************************");
            //Changing background color back to black and white for text
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < farmer.NorthBank.Count; i++)
            {
                Write(farmer.NorthBank[i]);
                Write("  ");
            }
        }

        //Displaying visual river to user
        public void DisplayRiver()
        {
            string water = "~";
            //Setting background color to blue and text color to a darker blue
            BackgroundColor = ConsoleColor.Blue;
            ForegroundColor = ConsoleColor.DarkBlue;
            WriteLine("\n\n");
            for (int i=0; i < 4; i++)
            {
                for (int x=0; x < 80; x++)
                {
                    Write(water);
                }
                WriteLine();
            }
            //Changing background color back to black and text color to white
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
        }

        //displaying south bank to user
        public void DisplaySouthBank()
        {
            string grass = "V";
            WriteLine("\n\n");
            //Setting background to green and text color to a darker green
            BackgroundColor = ConsoleColor.Green;
            ForegroundColor = ConsoleColor.DarkGreen;
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < 80; x++)
                {
                    Write(grass);
                }
                WriteLine();
            }
            WriteLine("******************************* South Bank *************************************");
            //Setting background to black and text color to white
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < farmer.SouthBank.Count; i++)
            {
                Write(farmer.SouthBank[i]);
                WriteLine();
            }
        }

        //Displaying user instructions
        public void DisplayWelcome()
        {
            //Changing text to red
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
            //Changing text back to white
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
            //Output for user error
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
                WriteLine("Yay! You did it! You completed the game!!");
                WriteLine("CONGRATULATIONS!");
                WriteLine("\n\n\n");
                Write("Would you like to play again? ");
                input = ReadLine();
                //Getting user input to play again or not
                if (input != "" && input.ToUpper()[0] == 'Y')
                {
                    Clear();
                    return true;
                }
                else { return false; }
            }
            //Display for when fox and chicken are left alone on bank
            else if (outcome == 4)
            {
                WriteLine("\n\n\n\n\n");
                WriteLine("NOOOO! The Fox Ate the Chicken!!");
                WriteLine("YOU LOSE!");
                Write("\n\n\n");
                Write("Would you like to play the game again? ");
                input = ReadLine();
                //Getting user input to play again or not
                if (input != "" && input.ToUpper()[0] == 'Y')
                {
                    Clear();
                    return true;
                }
                else { return false; }
            }
            //Display if chicken and grain are left alone on bank
            else if (outcome == 8)
            {
                WriteLine("\n\n\n");
                WriteLine("NOOO! The Chicken Ate the Grain!!");
                WriteLine("YOU LOSE!");
                Write("\n\n\nWould you like to play again? ");
                input = ReadLine();
                //Getting user input to play again or not can enter YES, yes, Y and y
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
