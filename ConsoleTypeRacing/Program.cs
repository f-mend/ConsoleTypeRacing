using System.IO.Pipes;
using System.Net;
using System.Reflection.Emit;
using System.Diagnostics;


namespace ConsoleTypeRacing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // create console application to generate words, phrases, sentences or paragraphs for typing practice (different difficulty levels).
            // it will handle the text comparison in real time and prompt the user to fix issues with a dynanamic count since first error.
            // additionally , it will track WPM (words per minute) and accuracy percentage, and provide a summary at the end of each session.
            // finally we will write results and keep track of progress over time with a local database or file storage (TBD).
            // we may also try and implement a visual race on the console window where the user can see their progress as they type.
            // still need to ask for difficulty, get different lengths of strings based n difficulty
            var data = new GameData();
            var stopwatch = new Stopwatch();
            var textKing = new GameEvaluation(data.GetGameText());

            bool running = true;

            Console.WriteLine($"Your Prompt:\n{textKing.GameAnswer}\n");
            do
            {
                var keyPress = Console.ReadKey(intercept: true);
                // Start the stopwatch on first keystroke
                if (!stopwatch.IsRunning)
                {
                    stopwatch.Start();
                }

                if (keyPress.Key == ConsoleKey.Enter) //skip, we dont want new lines
                {
                    continue;

                }
                else if (keyPress.Key == ConsoleKey.Backspace)
                {
                    textKing.RemoveLastInputFromUserInput();
                    Console.Write("\b \b");

                }
                else if (keyPress.Key == ConsoleKey.Escape)
                {
                    running = false;
                    break;
                }
                else
                {
                    bool isStringCorrect = textKing.isKeyCorrect(keyPress.KeyChar);
                    textKing.HandleUserInput(keyPress.KeyChar);
                    UserInputWithColoration(keyPress.KeyChar, isStringCorrect);
                }


            } // Two ways out. Either quit by hitting escape, or complete the text puzzle.
            while (textKing.UserInput != textKing.GameAnswer && running);
            stopwatch.Stop();
            Console.Clear();
            if (running)
            {   //calculate gamedata if we didnt quit
                double seconds = stopwatch.Elapsed.TotalSeconds;
                double wpm = textKing.GetWPM(seconds);
                int accuracy = textKing.GetAccuracy();

                Console.WriteLine($"Your prompt was:\n{textKing.GameAnswer}");
                Console.WriteLine($"You typed:\n{textKing.UserInput}\n");

                Console.WriteLine($"\tPost Game Stats:");
                Console.WriteLine($"\t  Your WPM was: {wpm}");
                Console.WriteLine($"\t  Your Overall Accuracy: {accuracy}%");
                Console.WriteLine($"\t  You made {textKing.TotalKeyPresses - textKing.TotalCorrectKeyPressesPossible} mistakes");
                Console.WriteLine($"\t  Your AWPM was: {textKing.GetAWPM(wpm, accuracy)}");

            }
            else
            {
                Console.WriteLine($"You Quit...");
            }
            Thread.Sleep(2000);
            Console.ReadKey();
        }

        public static void UserInputWithColoration(char input, bool isCorrect)
        {
            if (isCorrect)
            {
                Console.Write($"{input}");
            }
            else 
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write($"{input}");
            }
            Console.ResetColor();
        }
    }
}
