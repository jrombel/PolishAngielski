using System;

namespace PolishAngielski.Models
{
    public class Word
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
        public String GetPolish()
        {
            return this.polish;
        }
        public String GetEnglish()
        {
            return this.english;
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