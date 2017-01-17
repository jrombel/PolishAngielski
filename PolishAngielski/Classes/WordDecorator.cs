namespace PolishAngielski.Models
{
    public abstract class WordDecorator : Word
    {
        private Word word;

        public WordDecorator(Word word)
        {
            this.word = word;
        }

        public string GetPolish()
        {
            return word.GetPolish();
        }
        public string GetEnglish()
        {
            return word.GetEnglish();
        }
        public int GetDifficulty()
        {
            return word.GetDifficulty();
        }
    }
}