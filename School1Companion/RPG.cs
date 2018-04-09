using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School1Companion
{
    class Race
    {
        public virtual byte Strength()
        {
            return 10;
        }
        public virtual byte Dexterity()
        {
            return 10;
        }
        public virtual byte Constitution()
        {
            return 10;
        }
        public virtual byte Intelligence()
        {
            return 10;
        }
        public virtual byte Willpower()
        {
            return 10;
        }
        public virtual byte Charisma()
        {
            return 10;
        }
        public int HitPoints()
        {
            return 10;
        }
    }

    class HighElf : Race
    {
        public override byte Strength()
        {
            return 9;
        }
        public override byte Dexterity()
        {
            return 12;
        }
        public override byte Constitution()
        {
            return 9;
        }
        public override byte Intelligence()
        {
            return 11;
        }
        public override byte Willpower()
        {
            return 10;
        }
        public override byte Charisma()
        {
            return 10;
        }
    }
}
