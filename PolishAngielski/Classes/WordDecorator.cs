namespace PolishAngielski.Models
{
    public class WordDecorator : IWord
    {
        protected IWord word;

        public WordDecorator(IWord word)
        {
            this.word = word;
        }

        public override string GetPolish()
        {
            return word.GetPolish();
        }
        public override string GetEnglish()
        {
            return word.GetEnglish();
        }
        public override int GetDifficulty()
        {
            return word.GetDifficulty();
        }
        public override string GetCategory()
        {
            return word.GetCategory();
        }
    }
}