using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    public class BrainGene : Gene
    {
        static new int geneLength = 7;


        public BrainGene(string gString)
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
