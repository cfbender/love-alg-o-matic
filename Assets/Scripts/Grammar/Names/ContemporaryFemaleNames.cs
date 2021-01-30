using System.Collections.Generic;

namespace Lexic
{
    //A dictionary containing syllables that will form semi-historical names of kings, along with their titles. Fits any "alternate-history" RPG or strategy game.
    public class ContemporaryFemaleNames : BaseNames
    {
        private static Dictionary<string, List<string>> syllableSets = new Dictionary<string, List<string>>()
            {
                {
                    "firstnames",    new List<string>(){
                        "Mary", "Jennifer", "Lisa", "Sandra", "Michelle",
                        "Patricia", "Maria", "Nancy", "Donna", "Laura",
                        "Linda", "Susan", "Karen", "Carol", "Sarah",
                        "Barbara", "Margaret", "Betty", "Ruth",
                        "Kimberly", "Elizabeth", "Dorothy", "Helen", "Sharon", "Deborah",
                    }
                },
                {
                    "lastnames",      new List<string>(){
                        "_Smith","_Johnson","_Williams","_Jones","_Brown",
                        "_Davis","_Miller","_Wilson","_Moore","_Taylor",
                        "_Anderson","_Thomas","_Jackson","_White","_Harris",
                        "_Martin","_Thompson","_Garcia","_Martinez","_Robinson",
                        "_Clark","_Rodriguez","_Lewis","_Lee","_Walker","_Hall",
                        "_Allen","_Young","_Hernandez","_King","_Wright","_Lopez",
                        "_Hill","_Scott","_Green","_Adams","_Baker","_Gonzalez",
                        "_Nelson","_Carter","_Mitchell","_Perez","_Roberts",
                        "_Turner","_Phillips","_Campbell","_Parker","_Evans","_Edwards","_Collins",
                        "_McLendon","_Witterschein","_Bender"
                    }
                },
            };

        private static List<string> rules = new List<string>()
            {
               "%100firstnames%20lastnames", "%100firstnames%100lastnames",
            };

        public new static List<string> GetSyllableSet(string key) { return syllableSets[key]; }

        public new static List<string> GetRules() { return rules; }
    }
}
