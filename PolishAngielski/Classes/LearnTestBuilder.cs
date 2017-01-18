using System;
using System.Linq;

namespace PolishAngielski.Models
{
    class LearnTestBuilder : TestBuilder
    {
        private int numberOfQuestions;
        private bool useAdjectives;
        private bool nativeLanguage;
        private LearnTest test;
        public LearnTestBuilder()
        {
            test = new LearnTest();
        }
        public override void CollectTestInfo(int numberOfQuestions, bool useAdjectives, bool nativeLanguage)
        {
            this.numberOfQuestions = numberOfQuestions;
            this.useAdjectives = useAdjectives;
            this.nativeLanguage = nativeLanguage;

            test.nativeLanguage = nativeLanguage;
        }
        public override void PrepareList()
        {
            Random rnd = new Random();
            int index;
            for (int i = 0; i < numberOfQuestions; i++)
            {
                index = rnd.Next(Program.words.wordList.Count);
                IWord tmpWord = Program.words.wordList.ElementAt(index);
                Boolean absent = true;
                foreach (Word word in test.questions)
                {
                    if (tmpWord.Equals(word))
                        absent = false;
                }
                if (absent)
                {
                    test.questions.Add(tmpWord);

                    int where = rnd.Next(4);
                    Answer tmpAnswer = new Answer();
                    for (int j = 0; j < 4; j++)
                    {
                        bool oneMoreTime = false;
                        if (j == where)
                            tmpAnswer.answers.Add(tmpWord);
                        else
                        {
                            index = rnd.Next(Program.words.wordList.Count);
                            IWord tmpWord2 = Program.words.wordList.ElementAt(index);
                            if (tmpWord2.Equals(tmpWord))
                                oneMoreTime = true;
                            for (int k = 0; k < j; k++)
                            {
                                if (tmpAnswer.answers.ElementAt(k).Equals(tmpWord2))
                                    oneMoreTime = true;
                            }
                            if (oneMoreTime)
                            {
                                j--;
                            }
                            else
                                tmpAnswer.answers.Add(Program.words.wordList.ElementAt(index));
                        }
                    }
                    test.answers.Add(tmpAnswer);
                }
                else
                {
                    i--;
                }
            }
        }
        public LearnTest GetTest()
        {
            return test;
        }
    }
}