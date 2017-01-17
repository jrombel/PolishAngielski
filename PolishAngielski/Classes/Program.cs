using System;
using System.IO;

namespace PolishAngielski.Models
{
    public static class Program
    {
        public static WordBase words;
        public static AdjectiveBase adjectives;

        public static void SaveToFile()
        {
            StreamWriter file = new StreamWriter(@"wordBase.txt", false);
            foreach (Word word in words.wordList)
            {
                file.WriteLine(word.polish + ";" + word.english + ";" + word.category + ";" + word.difficulty);
            }
            file.Close();

            file = new StreamWriter(@"adjectiveBase.txt", false);
            foreach (Word adjective in adjectives.adjectiveList)
            {
                file.WriteLine(adjective.polish + ";" + adjective.english + ";" + adjective.category + ";" + adjective.difficulty);
            }
            file.Close();
        }

        public static void LoadFromFile()
        {
            if (File.Exists(@"wordBase.txt"))
            {
                string line;
                StreamReader file = new StreamReader(@"wordBase.txt");
                while ((line = file.ReadLine()) != null)
                {
                    string[] data = line.Split(';');
                    words.wordList.Add(new Word(data[0], data[1], data[2], Int32.Parse(data[3])));
                }
                file.Close();
            }
            if (File.Exists(@"adjectiveBase.txt"))
            {
                string line;
                StreamReader file = new StreamReader(@"adjectiveBase.txt");
                while ((line = file.ReadLine()) != null)
                {
                    string[] data = line.Split(';');
                    adjectives.adjectiveList.Add(new Word(data[0], data[1], data[2], Int32.Parse(data[3])));
                }
                file.Close();
            }
        }
    }
}