using System;
using System.Collections.Generic;

namespace PolishAngielski.Models
{
    class WriteTest : Test
    {
        public List<IWord> questions;
        public bool nativeLanguage;
        private int correctAnswers;
        private int wrongAnswers;
        public WriteTest()
        {
            questions = new List<IWord>();
            nativeLanguage = false;
        }
        public override bool CheckAnswer(int questionNumber, string answer)
        {
            if (questions[questionNumber].GetEnglish() == answer || questions[questionNumber].GetPolish() == answer)
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