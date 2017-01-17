﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PolishAngielski.Models
{
    class ManyTest : Test
    {
        public List<Answer> answers;
        public List<Word> questions;
        public bool nativeLanguage;
        private int correctAnswers;
        private int wrongAnswers;

        public ManyTest()
        {
            answers = new List<Answer>();
            questions = new List<Word>();
            nativeLanguage = false;
        }
        public override bool CheckAnswer(int questionNumber, string answer)
        {
            List<Word> goodAnswers = new List<Word>();
            foreach (Word i in answers[questionNumber].answers)
            {
                if (i.category == questions[questionNumber].category)
                    goodAnswers.Add(i);
            }

            var answerIndexes = answer.Select(digit => int.Parse(digit.ToString()));
            if (goodAnswers.Count == answer.Length)
            {
                bool everyAnswersGood = true;
                for (int i = 0; i < answer.Length; i++)
                {
                    if (questions[questionNumber].category != answers[questionNumber].answers.ElementAt(answerIndexes.ElementAt(i)).category)
                    {
                        everyAnswersGood = false;
                    }
                }
                if (everyAnswersGood)
                    correctAnswers++;
                return everyAnswersGood;
            }
            else
            {
                wrongAnswers++;
                return false;
            }
        }
        public override string ShowScore()
        {
            String result;
            try
            {
                result = "Poprawne odpowiedzi: " + correctAnswers + "\nBłędne odpowiedzi: " + wrongAnswers + "\nNieudzielone odpowiedzi: "
                    + (questions.Count - correctAnswers - wrongAnswers) + "\nWszyskie pytania: " + questions.Count +
                    "\n\nWynik procentowy: " + (correctAnswers * 100) / (questions.Count) + "%";
            }
            catch (DivideByZeroException)
            {
                result = "Chciałesz oszukać test";
            }
            return (result);
        }
    }
}