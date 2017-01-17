using System.Collections.ObjectModel;

namespace PolishAngielski.Models
{
    public class AdjectiveBase
    {
        private static AdjectiveBase instance = new AdjectiveBase();
        public ObservableCollection<Word> adjectiveList { get; set; }

        private AdjectiveBase()
        {
            adjectiveList = new ObservableCollection<Word>();
        }
        public static AdjectiveBase getInstance()
        {
            if (instance == null)
            {
                instance = new AdjectiveBase();
            }
            return instance;
        }
    }
}
