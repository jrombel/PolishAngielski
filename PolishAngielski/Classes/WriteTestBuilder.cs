using System;
using System.Linq;

namespace PolishAngielski.Models
{
    class WriteTestBuilder : TestBuilder
    {
        private int numberOfQuestions;
        private bool useAdjectives;
        private bool nativeLanguage;
        private WriteTest test;
        public WriteTestBuilder()
        {
            test = new WriteTest();
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
                Word tmpWord = Program.words.wordList.ElementAt(index);
                Boolean absent = true;
                foreach (Word word in test.questions)
                {
                    if (tmpWord.Equals(word))
                        absent = false;
                }
                if (absent)
                {
                    test.questions.Add(tmpWord);
                }
                else
                {
                    i--;
                }
            }
        }
        public WriteTest GetTest()
        {
            return test;
        }
    }
}