using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class OutputGene:Gene
    {
        new static int geneLength = 7;


        public OutputGene(string gString)
        {
            geneticString = Pad(gString, geneLength - gString.Length).Substring(0, geneLength);
        }

        public override void Express(Creature owner)
        {
            throw new NotImplementedException();
        }

        public override int GetLength()
        {
            return geneLength;
        }
    }
}
