using System;
using System.Linq;
using System.Windows;

namespace PolishAngielski.Models
{
    class OneTestBuilder : TestBuilder
    {
        private int numberOfQuestions;
        private bool useAdjectives;
        private bool nativeLanguage;
        private OneTest test;
        public OneTestBuilder()
        {
            test = new OneTest();
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
                if(useAdjectives)
                    tmpWord = new Adjective(tmpWord);
                Boolean absent = true;
                foreach (IWord word in test.questions)
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
                            if (useAdjectives)
                                tmpWord2 = new Adjective(tmpWord2);
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
                                tmpAnswer.answers.Add(tmpWord2);
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
        public OneTest GetTest()
        {
            return test;
        }
    }
}