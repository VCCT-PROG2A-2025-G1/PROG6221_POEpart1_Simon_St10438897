using POEpart1.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

//References:
//ChatGPT www.ChatGPT.com
//How to Play Music In C# (Simple) - https://www.youtube.com/watch?v=uZlz1SSisYY

namespace POEpart1
{
    public static class CyberChat
    {
        //Global variable
        static string name; //Global variable for the name of the user

        private static readonly Random _rng = new Random();

        //Method: PlaySound
        //Plays the gerrting sound when the program starts
        //Method inspired by How to Play Music In C# (Simple)
        public static void PlaySound(string soundFilePath)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(soundFilePath))
                {
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                //Error handling for the sound file
                Console.WriteLine($"Error playing sound: {ex.Message}");
            }

        }

        //-------------------------------------------------------------------
        //-------------------------------------------------------------------

        //Method: DisplayLogo
        //Displays the ASCII art logo of the program
        //Method created by ChatGPT to display ASCII art
        public static void DisplayLogo()
        {
            Console.WriteLine(@"  ____            _                  _                 
     / ___|___  _ __ | |_ _ __ _   _   / \   ___ ___  ___ 
    | |   / _ \| '_ \| __| '__| | | | / _ \ / __/ __|/ _ \
    | |__| (_) | | | | |_| |  | |_| |/ ___ \\__ \__ \  __/
     \____\___/|_| |_|\__|_|   \__, /_/   \_\___/___/\___|
                               |___/                     
       Cybersecurity Awareness Bot
            ");
        }

        //-------------------------------------------------------------------
        //-------------------------------------------------------------------

        //Method: Greeting
        //Greets the user and asks for their name
        public static void Greeting()
        {
            //displays the logo
            DisplayLogo();

            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                             Welcome to the Cyber Chat!");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

            //Loops until a valid name is entered
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green; //idea to change colour was from ChatGPT
                Console.Write("Please enter your name: ");
                Console.ResetColor();
                name = Console.ReadLine();
                //Checks if the name is empty or contains numbers
                if (string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error]: Please enter a valid name and don't include numbers.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine($"Hello, {name}! I'm your virtual assistant for online safety.");
                    Console.WriteLine("I'm here to help you with your online safety questions.");
                    Console.ResetColor();
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    break;
                }
            }
        }
        //-------------------------------------------------------------------
        //-------------------------------------------------------------------

        public static void Cyperchat()
        {


            //Main loop where questions are asked and answered
            //Only works with one question at a time, so no two questions at the same time
            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Please ask your question {name}: ");
                Console.ResetColor();

                //Takes the user input and converts it to lowercase
                string question = Console.ReadLine().ToLower();

                //Checks if the question is empty or contains numbers
                if (string.IsNullOrWhiteSpace(question) || question.Any(char.IsDigit))
                {
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{name}, Please enter a valid question and don't include numbers.");
                    Console.ResetColor();
                    continue;
                }

                //Checks if the question contains any of the keys in the sentiment detection dictionary
                var sentimentKey = MainResponces.sentimateDetection.Keys.FirstOrDefault(k => question.Contains(k));

                if (sentimentKey != null)
                {
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.WriteLine(MainResponces.sentimateDetection[sentimentKey]);
                    Console.ResetColor();
                }

                //Generated by ChatGPT to remove punctuation and special characters
                question = Regex.Replace(question, @"[^\w\s]", "");

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

                //Generated by ChatGPT to check if the question contains any of the keys in the dictionary
                string matchKey = MainResponces.responses.Keys.FirstOrDefault(k => question.Contains(k));

                if (matchKey != null)
                {
                    //If a match is found, get the corresponding answers from the dictionary
                    var options = MainResponces.responses[matchKey];

                    //Generated by ChatGPT to randomly select an answer from the options
                    var reply = options[_rng.Next(options.Length)];

                    //If a match is found, display the answer
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Answer: {reply}");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

                    while (true)
                    {
                        //Asks the user if they want to hear a more in detailed answer
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Would you like to hear A more in detailed answer {name}? (yes/no)");
                        Console.ResetColor();
                        Console.Write("Enter: ");
                        string moreAnswer = Console.ReadLine().ToLower();
                        if (moreAnswer == "yes")
                        {
                            //If the user wants to hear a more in detailed answer
                            if (MainResponces.followUp.TryGetValue(matchKey, out var detailedOptions))
                            {
                                //Selects a random detailed answer from the options
                                var reply2 = detailedOptions[_rng.Next(detailedOptions.Length)];

                                Console.WriteLine();
                                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"More details on {matchKey}: {reply2}");
                                Console.ResetColor();
                                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                                Console.WriteLine();
                            }
                            
                            break;
                        }
                        else if (moreAnswer == "no")
                        {
                            //If the user does not want to hear a more indetailed answer
                            Console.WriteLine();
                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine();
                            break;
                        }
                        else if (string.IsNullOrWhiteSpace(moreAnswer) || moreAnswer.Any(char.IsDigit))
                        {
                            //If the user enters an invalid response, display an error message
                            //Prompts the user to enter a valid response
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"[Error]: {name}, Please enter a valid response (yes/no) and don't include numbers.");
                            Console.ResetColor();
                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                        }
                    }


                    //Asks if the user wants to ask another question
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Would you like to ask another question {name}? (yes/no)");

                        Console.ResetColor();
                        Console.Write("Enter: ");
                        string anotherQuestion = Console.ReadLine().ToLower();

                        if (anotherQuestion == "yes")
                        {
                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                            break;
                        }
                        else if (anotherQuestion == "no")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Thank you for using the Cyber Chat {name}! Stay safe online!");
                            Console.ResetColor();
                            Environment.Exit(0);

                        }
                        else if (string.IsNullOrWhiteSpace(anotherQuestion) || anotherQuestion.Any(char.IsDigit))
                        {
                            //If the user enters an invalid response, display an error message
                            //Prompts the user to enter a valid response
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"[Error]: {name}, Please enter a valid question and don't include numbers.");
                            Console.ResetColor();
                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                        }
                    }

                }
                else
                {
                    //If no match is found, display a message indicating that the question is not recognized
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"I'm sorry {name}, I don't have an answer for that question.");
                    Console.ResetColor();
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("If you want to ask another question, please type 'yes'.");
                        Console.WriteLine("If you want to exit, please type 'no'.");
                        Console.WriteLine();
                        Console.Write("Enter: ");
                        Console.ResetColor();
                        string yn = Console.ReadLine().ToLower();

                        if (yn == "yes")
                        {
                            //If the user wants to ask another question, break the loop and continue
                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                            break;
                        }
                        else if (yn == "no")
                        {
                            //If the user wants to exit, display a thank you message and exit the program
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Thank you {name} for using the Cyber Chat! Stay safe online!");
                            Console.ResetColor();
                            Environment.Exit(0);
                        }
                        else if (string.IsNullOrWhiteSpace(yn) || yn.Any(char.IsDigit))
                        {
                            //If the user enters an invalid response, display an error message
                            //Prompts the user to enter a valid response
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine($"[Error]: {name}, Please enter a valid respone (yes/no) and don't include numbers.");
                            Console.ResetColor();
                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                        }
                    }

                }
            }
        }
        //-------------------------------------------------------------------
        //-------------------End Of File----------------------------
    }
}
