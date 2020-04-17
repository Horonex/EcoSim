using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    public class ProteinBD:ProteinGene
    {
        new static int geneLength = 4;

        public ProteinBD(string gString)
        {


            geneticString = Pad(gString, geneLength - gString.Length).Substring(0, geneLength);
            liaison = geneticString.Substring(2);
            Prefix = gString.Substring(0, 2);

        }


        public override string ToString()
        {
            return "PROTEINBREAKDOWN " + liaison;
        }

        public override int GetLength()
        {
            return geneLength;
        }

        public override void Express(Creature owner)
        {
            owner.stomac.chemicalGenes.Add(this);
            owner.stats.pBreakedown.Add(this);
        }
    }
}
