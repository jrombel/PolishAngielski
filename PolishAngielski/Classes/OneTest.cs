using System;
using System.Collections.Generic;

namespace PolishAngielski.Models
{
    class OneTest : Test
    {
        public List<Answer> answers;
        public List<Word> questions;
        public bool nativeLanguage;
        private int correctAnswers;
        private int wrongAnswers;

        public OneTest()
        {
            answers = new List<Answer>();
            questions = new List<Word>();
            nativeLanguage = false;
        }
        public override bool CheckAnswer(int questionNumber, string answer)
        {
            int answerIndex = Int32.Parse(answer);
            if (questions[questionNumber].Equals(answers[questionNumber].getConcreteAnswer(answerIndex)))
            {
                correctAnswers++;
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
                result = "Poprawne odpowiedzi: " + correctAnswers + "\nBłędne odpowiedzi: " + wrongAnswers + "\nNieudzielone odpowiedzi: "
                    + (questions.Count - correctAnswers - wrongAnswers) + "\nWszyskie pytania: " + questions.Count +
                    "\n\nWynik procentowy: " + (correctAnswers * 100) / (questions.Count) + "%";
            }
            catch (DivideByZeroException)
            {
            }
            return (result);
        }
    }
}