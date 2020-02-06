using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class Vision : Input
    {

        public Vision(string geneticString)
        {
            this.geneticString = geneticString;

        }

        public override string ToString()
        {
            return "INPUT VISION " + geneticString.Substring(3);

        }

        public override void Express(Creature expressedOn)
        {
            throw new NotImplementedException();
        }
    }
}
