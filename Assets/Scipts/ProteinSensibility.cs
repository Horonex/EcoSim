using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class ProteinSensibility : Gene 
    {
        public ProteinSensibility(string geneticString)
        {
            this.geneticString = geneticString;

        }

        public override string ToString()
        {
            return "INPUT PROTEIN " + geneticString.Substring(3);

        }

        public override void Express(Creature expressedOn)
        {
            throw new NotImplementedException();
        }
    }
}
