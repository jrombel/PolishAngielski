using System.Collections.Generic;
using System.Linq;

namespace PolishAngielski.Models
{
    public class Answer
    {
        public List<Word> answers;
        public Answer()
        {
            answers = new List<Word>();
        }
        public Answer(List<Word> providedAnswers)
        {
            answers = providedAnswers;
        }

        public Word getConcreteAnswer(int index)
        {
            return answers.ElementAt(index);
        }
    }
}