using System;

namespace PolishAngielski.Models
{
    public class Word : IWord
    {
        public String polish { get; set; }
        public String english { get; set; }
        public String category { get; set; }
        public int difficulty { get; set; }

        public Word()
        {
            polish = "";
            english = "";
            category = "";
            difficulty = 0;
        }
        public Word(String polish, String english, String category, int difficulty)
        {
            this.polish = polish;
            this.english = english;
            this.category = category;
            this.difficulty = difficulty;
        }
        public override String GetPolish()
        {
            return this.polish;
        }
        public override String GetEnglish()
        {
            return this.english;
        }
        public override int GetDifficulty()
        {
            return this.difficulty;
        }
        public override String GetCategory()
        {
            return this.category;
        }
    }
}