using System;

namespace PolishAngielski.Models
{
    public class Adjective : WordDecorator
    {
        public String polish { get; set; }
        public String english { get; set; }
        public String category { get; set; }
        public int difficulty { get; set; }


        //private int index;

        public Adjective(IWord word) : base(word)
        {
            //Random rnd = new Random();
            //index = rnd.Next(Program.adjectives.adjectiveList.Count);

        }
        public override String GetPolish()
        {
            Random rnd = new Random();
            int index = rnd.Next(Program.adjectives.adjectiveList.Count);
            return (Program.adjectives.adjectiveList[index].GetPolish() + " " + base.GetPolish());
        }
        public override String GetEnglish()
        {
            Random rnd = new Random();
            int index = rnd.Next(Program.adjectives.adjectiveList.Count);
            return (Program.adjectives.adjectiveList[index].GetEnglish() + " " + base.GetEnglish());
        }
        public override int GetDifficulty()
        {
            return this.difficulty;
        }
        public override String GetCategory()
        {
            return base.GetCategory();
        }
    }
}