using System;
using System.Collections.Generic;

namespace PolishAngielski.Models
{
    public class LearnTest : Test
    {
        public List<Answer> answers;
        public List<IWord> questions;
        public bool nativeLanguage;
        public int correctAnswers;
        public int wrongAnswers;
        public int allAnswers;
        private bool countCorrect;

        public LearnTest()
        {
            answers = new List<Answer>();
            questions = new List<IWord>();
            nativeLanguage = false;

            countCorrect = true;
        }
        public override bool CheckAnswer(int questionNumber, string answer)
        {
            int answerIndex = int.Parse(answer);
            if(answerIndex == -1)
            {
                wrongAnswers++;
                return false;
            }
            if (answerIndex == -2)
            {
                countCorrect = false;
                return false;
            }
            if (questions[questionNumber].Equals(answers[questionNumber].getConcreteAnswer(answerIndex)))
            {
                answers.RemoveAt(questionNumber);
                questions.RemoveAt(questionNumber);
                if(countCorrect)
                    correctAnswers++;
                allAnswers++;
                return true;
            }
            else
            {
                wrongAnswers++;
                return false;
            }
        }
        public override string ShowScore()
        {
            String result = "";
            try
            {
                result = "Poprawne odpowiedzi: " + correctAnswers + "\nBłędne odpowiedzi: " + wrongAnswers + 
                    "\nWszyskie pytania: " + allAnswers +
                    "\n\nWynik procentowy: " + (correctAnswers * 100) / (allAnswers) + "%";
            }
            catch (DivideByZeroException)
            {
            }
            return (result);
        }
    }
}