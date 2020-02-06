using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class ProteinS:ProteinGene
    {
        public const int GENELENGTH = 4;

        public ProteinS(string geneticString)
        {
            this.geneticString = geneticString;
            liaison = geneticString.Substring(2);
        }

        public override string ToString()
        {
            return "PROTEINSYNTHESIS " + liaison;
        }

        public override void Express(Creature expressedOn)
        {
            throw new NotImplementedException();
        }

    }
}
