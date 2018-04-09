using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School1Companion
{
    
    class Dice
    {
        public Dice() {}

        public int ThreeD6()
        {
            Random random = new Random();
            return random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
        }

        public int ThreeD6(int iDice)
        {
            Random random = new Random();
            int iTotal = 0;
            for (int i = 0; i < iDice; i++)
            {
                iTotal += random.Next(1, 7);
            }
            return iTotal;
        }

        public int FudgeDice()
        {
            Random random = new Random();
            int iTotal = 0;
            for (int i = 0; i < 3; i++)
            {
                iTotal += random.Next(-1, 2);
            }
            return iTotal;
        }

        public int Roll4d6k3()
        {
            Random random = new Random();
            List<int> iScoresList = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                iScoresList[i] = random.Next(1, 7);
            }
            iScoresList.Remove(iScoresList.Min());
            return iScoresList.Sum();
        }

        public Dictionary<string, int> AttributesDnDGen()
        {
            Dictionary<string, int> oAttributes = new Dictionary<string, int>();
            oAttributes.Add("Str", Roll4d6k3());
            oAttributes.Add("Dex", Roll4d6k3());
            oAttributes.Add("Con", Roll4d6k3());
            oAttributes.Add("Int", Roll4d6k3());
            oAttributes.Add("Wis", Roll4d6k3());
            oAttributes.Add("Cha", Roll4d6k3());
            return oAttributes;
        }
    }
}
