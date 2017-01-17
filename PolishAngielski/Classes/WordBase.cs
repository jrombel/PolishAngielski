using System.Collections.ObjectModel;

namespace PolishAngielski.Models
{
    public class WordBase
    {
        private static WordBase instance = new WordBase();
        public ObservableCollection<Word> wordList { get; set; }

        private WordBase()
        {
            wordList = new ObservableCollection<Word>();
        }
        public static WordBase getInstance()
        {
            if (instance == null)
            {
                instance = new WordBase();
            }
            return instance;
        }
    }
}