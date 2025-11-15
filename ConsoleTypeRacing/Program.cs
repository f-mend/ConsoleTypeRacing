using System.IO.Pipes;
using System.Net;
using System.Reflection.Emit;
using System.Threading;

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

            // eventually this will be replaced with the return value of our fetch game data class return result
            var TextKing = new InputEvaluation("The quick brown fox jumps over the lazy dog.");
            
            bool running = true;
            Console.WriteLine($"Your Prompt: \n{TextKing.GameAnswer}\n");
            do
            {
                var keyPress = Console.ReadKey(intercept: true);
                bool isStringCorrect = TextKing.isKeyCorrect(keyPress.KeyChar);
                if (keyPress.Key == ConsoleKey.Backspace)
                {
                    TextKing.RemoveLastInputFromUserInput();

                }
                else if (keyPress.Key == ConsoleKey.Escape)
                {
                    running = false;
                    break;
                }
                else 
                {
                    TextKing.HandleUserInput(keyPress.KeyChar);
                    TextKing.InrecmentTotalKeyPresses();
                    UserInputWithColoration(keyPress.KeyChar, isStringCorrect);
                }                


            } // Two ways out. Either quit by hitting escape, or complete the text puzzle.
            while (TextKing.UserInput != TextKing.GameAnswer && running);

            Console.Clear();

            if (running)
            {
                Console.WriteLine($"Your prompt was: {TextKing.GameAnswer}\n");
                Console.WriteLine($"You typedt: {TextKing.UserInput}");
                Console.WriteLine($"Your Overall Accuracy: {TextKing.CalculateAccuracy()}%");
                Console.WriteLine($"You made {TextKing.TotalKeyPresses - TextKing.TotalCorrectKeyPressesPossible} mistakes");


            }
            else
            {
                Console.WriteLine($"Your prompt was: {TextKing.GameAnswer}\n");
                Console.WriteLine($"Instead you quit...\n");
                Console.WriteLine($"Your attempt before quitting was: {TextKing.UserInput}");
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
