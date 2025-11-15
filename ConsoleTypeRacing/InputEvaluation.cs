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

        private int _currentPosition = 0;
        private int _totalKeyPresses = 0;
        private int _totalCorrectKeyPresses = 0;


        public string UserInput { get => _userInput; set => _userInput = value; }
        public string GameAnswer { get => _gameAnswer; private set => _gameAnswer = value; }
        public int CurrentPosition { get => _currentPosition; private set => _currentPosition = value; }
        public int TotalKeyPresses { get => _totalKeyPresses; private set => _totalKeyPresses = value; }
        public int TotalCorrectKeyPresses { get => _totalCorrectKeyPresses; private set => _totalCorrectKeyPresses = value; }


        public InputEvaluation(string answer)
        {
            GameAnswer = answer;
        }

        public void HandleUserInput(char newInput)
        {
            UpdateUserInput(newInput);
            IncrementCurrentPosition();

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


        public int CalculateAccuracy()
        {
            //flawed, always 100% by the time you finish lol
            //need to evaluate the total characters used to finish / characters in the answer (same as # of correct possible keystrokes)
            int charactersCorrect = 0;
            int minLength = Math.Min(UserInput.Length, GameAnswer.Length);
            for (int i = 0; i < UserInput.Length; i++)
            {
                if (UserInput[i] == GameAnswer[i])
                {
                    charactersCorrect++;
                }
            }

            if (minLength == 0 || charactersCorrect == 0)
            {
                return 0;
            }
            else
            {
                float accuracy = (float)charactersCorrect / minLength * 100;
                return (int)Math.Round(accuracy);
            }

        }
        public void CalculateWPM()
        {
            throw new NotImplementedException();
        }
        public int GetFirstErrorPosition()
        {
            int posFirstError = -1;
            for (int i = 0; i < UserInput.Length; i++)
            {
                if (UserInput[i] != GameAnswer[i])
                {
                    posFirstError = i;
                    break;
                }
            }
            return posFirstError;
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



