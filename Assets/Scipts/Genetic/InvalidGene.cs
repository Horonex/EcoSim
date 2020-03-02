using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class InvalidGene : Gene
    {
        new static int geneLength = 2;

        public InvalidGene(string gString)
        {
            geneticString = Pad(gString, geneLength - gString.Length).Substring(0, geneLength);
            Prefix = gString.Substring(0, 2);

        }

        public override string ToString()
        {
            return "INVALID GENE " + geneticString;
        }
        public override int GetLength()
        {
            return geneLength;
        }
    }
}
