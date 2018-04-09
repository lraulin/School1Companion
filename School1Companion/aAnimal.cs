using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School1Companion
{
    public abstract class Animal
    {
        public virtual string Voice()
        {
            return "";
        }

        public virtual int Legs()
        {
            if (HasLegs())
            {
                return (4);
            }
            else
            {
                return (0);
            }
        }

        public virtual bool HasLegs()
        {
            return (true);
        }
    }

    public class Dog : Animal
    {
        public override string Voice()
        {
            return ("Woof");
        }
    }

    public class Sail : Animal
    {
        public override bool HasLegs()
        {
            return false;
        }
    }
}
