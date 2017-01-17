using System;

namespace PolishAngielski.Models
{
    public abstract class IWord
    {
        public abstract String GetPolish();
        public abstract String GetEnglish();
        public abstract int GetDifficulty();
        public abstract String GetCategory();
    }
}
