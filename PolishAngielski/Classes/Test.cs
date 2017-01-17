using System;

namespace PolishAngielski.Models
{
    public abstract class Test
    {
        public abstract bool CheckAnswer(int questionNumber, String answer);
        public abstract string ShowScore();

        public Test()
        {

        }
    }
}