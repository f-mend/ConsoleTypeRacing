using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTypeRacing
{
    internal class InputEvaluation
    {
        // This class will be responsible for tracking the accuracy of the user's typing.
        // It may include methods to calculate accuracy percentage, count errors, and provide feedback to the user.
        private string _userInput = "";
        private string _gameAnswer = "";

        //This may not be needed, as cursor postiion is a Console accessible command/method already, so manipulating it over manually accounting for it may be the better implementation
        private int _currentPosition = 0;
        private int _totalKeyPresses = 0;
        private int _totalCorrectKeyPresses = 0;


        public string UserInput { get => _userInput; set => _userInput = value; }
        public string GameAnswer { get => _gameAnswer; private set => _gameAnswer = value; }
        public int CurrentPosition { get => _currentPosition; private set => _currentPosition = value; }
        public int TotalKeyPresses { get => _totalKeyPresses; private set => _totalKeyPresses = value; }
        public int TotalCorrectKeyPressesPossible { get => _totalCorrectKeyPresses; private set => _totalCorrectKeyPresses = value; }


        public InputEvaluation(string answer)
        {
            GameAnswer = answer;
            TotalCorrectKeyPressesPossible = GameAnswer.Length;
        }
        private void EmptyUserInput()
        {
            if (UserInput != "")
            {
                UserInput = "";
            }
        }
        private void UpdateUserInput(char newInput)
        {
            UserInput = UserInput + newInput;
        }
        private void IncrementCurrentPosition()
        {
            CurrentPosition++;
        }
        private void DecrementCurrentPosition()
        {
            CurrentPosition--;
        }

        // All above are private and only accessible from inside the class instance
        // All below are public for use around the main 
        
        
        public int CalculateAccuracy()
        {
            float accuracy = (float)TotalCorrectKeyPressesPossible / TotalKeyPresses * 100;
            return (int)Math.Round(accuracy);
        }
        public int CalculateWPM()
        {
            // Total Number of Words = Total Keys Pressed / 5
            // WPM = Total Number of Words / Time Elapsed in Minutes(rounded down)
            throw new NotImplementedException();
        }
        public int CalculateAWPM(int wpm, int accuracy)
        {
            // WPM = 26
            // Accuracy = 85 %
            // AWPM = 26 x .85 = 22
            throw new NotImplementedException();
        }
        public void InrecmentTotalKeyPresses()
        {
            TotalKeyPresses++;
        }
        public bool isKeyCorrect(char keyPress)
        {
            if (CurrentPosition>  GameAnswer.Length)
            {
                return false;
            }
            else if (keyPress == GameAnswer[CurrentPosition])
            {
                return true;
            }
            else
                return false;
        }
        public void HandleUserInput(char newInput)
        {
            UpdateUserInput(newInput);
            IncrementCurrentPosition();
        }
        public void RemoveLastInputFromUserInput()
        {
            if (CurrentPosition - 1 <= 0)
            {
                EmptyUserInput();
                CurrentPosition = 0;
                Console.Write("\b \b");
            }
            else
            {
                UserInput = UserInput.Substring(0, UserInput.Length - 1);
                DecrementCurrentPosition();
                Console.Write("\b \b"); //this handles removing and moving back a space in the writeline so that we can have text editor like qualities
            }
        }
    }
}



