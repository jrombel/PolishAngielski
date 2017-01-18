using System.Collections.Generic;
using System.Linq;

namespace PolishAngielski.Models
{
    public class Answer
    {
        public List<IWord> answers;
        public Answer()
        {
            answers = new List<IWord>();
        }
        public Answer(List<IWord> providedAnswers)
        {
            answers = providedAnswers;
        }

        public IWord getConcreteAnswer(int index)
        {
            return answers.ElementAt(index);
        }
    }
}