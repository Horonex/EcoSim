using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class InvalidGene : Gene
    {
        public const int GENELENGTH = 2;

        public InvalidGene(string geneticString)
        {
            this.geneticString = geneticString;
        }

        public override string ToString()
        {
            return "INVALID GENE " + geneticString;
        }

        public override void Express(Creature expressedOn)
        {
            throw new NotImplementedException();
        }
    }
}
