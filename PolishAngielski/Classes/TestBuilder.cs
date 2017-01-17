namespace PolishAngielski.Models
{
    public abstract class TestBuilder
    {
        public abstract void CollectTestInfo(int numberOfQuestions, bool useAdjectives, bool nativeLanguage);
        public abstract void PrepareList();
    }
}