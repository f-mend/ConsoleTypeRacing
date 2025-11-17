using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTypeRacing
{
    internal class GameEvaluation
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
            
        public GameEvaluation(string answer)
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
        private void InrecmentTotalKeyPresses()
        {
            TotalKeyPresses++;
        }
        private void DecrementCurrentPosition()
        {
            CurrentPosition--;
        }
        
        
        public int GetAccuracy()
        {
            float accuracy = (float)TotalCorrectKeyPressesPossible / TotalKeyPresses * 100;
            return (int)Math.Round(accuracy);
        }
        public double GetWPM(double durationElapsed)
        {
            double wpm = (TotalKeyPresses * 60.0) / (5.0 * durationElapsed);
            return Math.Round(wpm, 2); 
        }
        public double GetAWPM(double wpm, int accuracy)
        {
            double AWPM =  wpm * (accuracy / 100.0);
            return Math.Round(AWPM, 2);
        }
        public bool isKeyCorrect(char keyPress)
        {
            if (CurrentPosition >= GameAnswer.Length)
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
            InrecmentTotalKeyPresses();
        }
        public void RemoveLastInputFromUserInput()
        {
            if (CurrentPosition - 1 <= 0)
            {
                EmptyUserInput();
                CurrentPosition = 0;
            }
            else
            {
                UserInput = UserInput.Substring(0, UserInput.Length - 1);
                DecrementCurrentPosition();
            }
        }
    }
}



