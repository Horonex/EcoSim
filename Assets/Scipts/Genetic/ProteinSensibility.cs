using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class ProteinSensibility : Input 
    {

        new static int geneLength = 7;


        public ProteinSensibility(string gString)
        {
            geneticString = Pad(gString, geneLength - gString.Length).Substring(0, geneLength);
            Prefix = gString.Substring(0, 2);
        }

        public override string ToString()
        {
            return "INPUT PROTEIN " + geneticString.Substring(3);

        }
        public override int GetLength()
        {
            return geneLength;
        }
    }
}
