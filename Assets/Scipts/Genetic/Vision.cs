using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    class Vision : Input
    {
        new static int geneLength = 7;
                
        public Vision(string gString)
        {
            geneticString = Pad(gString, geneLength - gString.Length).Substring(0, geneLength);
            Prefix = gString.Substring(0, 2);

        }

        public override string ToString()
        {
            return "INPUT VISION " + geneticString.Substring(3);

        }

        public override int GetLength()
        {
            return geneLength;
        }

    }
}
