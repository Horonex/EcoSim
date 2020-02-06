using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class Hearing : Input
    {
        public Hearing(string geneticString)
        {
            this.geneticString = geneticString;

        }

        public override string ToString()
        {
            return "INPUT HEARING " + geneticString.Substring(3);

        }

        public override void Express(Creature expressedOn)
        {
            throw new NotImplementedException();
        }

    }
}
