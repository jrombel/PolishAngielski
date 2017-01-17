using System;

namespace PolishAngielski.Models
{
    class Adjective : WordDecorator
    {
        public String polish { get; set; }
        public String english { get; set; }
        public String category { get; set; }
        public int difficulty { get; set; }

        public Adjective(Word word) : base(word)
        {

        }
        public String GetPolish()
        {
            return ("smutny " + base.GetPolish());
        }
        public String GetEnglish()
        {
            return ("sad" + base.GetEnglish());
        }
        public int GetDifficulty()
        {
            return this.difficulty;
        }
        public String GetCategory()
        {
            return this.category;
        }
    }
}