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
        private int _remainingCharacters = 0;
        private string _gameAnswer = "";
        private int _currentPosition = 0;

        public string UserInput { get { return _userInput; } set { _userInput = value; } }
        public int RemainingCharacters { get { return _remainingCharacters; } set { _remainingCharacters = value; } }
        public string GameAnswer { get { return _gameAnswer; } private set { _gameAnswer = value; } }
        public int CurrentPosition { get { return _currentPosition; } private set { _currentPosition = value; } }


        public InputEvaluation(string answer)
        {
            GameAnswer = answer;
            RemainingCharacters = GameAnswer.Length;
        }


        private void UpdateRemainingCharacters()
        {
            RemainingCharacters = GameAnswer.Length - UserInput.Length;
        }
        private void EmptyUserInput()
        {
            if (UserInput != "")
            {
                UserInput = "";
            }
        }
        public void UpdateUserInput(char newInput)
        {
            UserInput = UserInput + newInput;
            IncrementCurrentPosition();
            UpdateRemainingCharacters();
        }
        public void RemoveLastInputFromUserInput()
        {
            if (CurrentPosition - 1 <= 0)
            {
                EmptyUserInput();
                CurrentPosition = 0;
                UpdateRemainingCharacters();
            }
            else
            {
                UserInput = UserInput.Substring(0, UserInput.Length - 1);
                DecrementCurrentPosition();
                UpdateRemainingCharacters();
            }
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

    }
}



