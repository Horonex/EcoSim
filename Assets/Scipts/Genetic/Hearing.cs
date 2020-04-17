using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class Hearing : Input
    {
        new static int geneLength = 7;

        const int earNumberModTreshold = 15;


        public Hearing(string gString)
        {
            geneticString = Pad(gString,geneLength-gString.Length).Substring(0,geneLength);
            Prefix = gString.Substring(0, 2);
        }

        public override string ToString()
        {
            return "INPUT HEARING " + geneticString.Substring(3);

        }
        public override int GetLength()
        {
            return geneLength;
        }

        public override void Express(Creature owner)
        {
            switch (geneticString.Substring(3, 2))
            {
                default:
                    {
                        break;
                    }
                case "HH":
                    {
                        owner.stats.eSensitivity++;
                        break;
                    }
                case "HV":
                    {
                        owner.stats.eSensitivity--;
                        break;
                    }
                case "HO":
                    {          
                        if (GeneticCode.ToInt(geneticString.Substring(5)) >= earNumberModTreshold)
                        {
                            owner.stats.eEars++;
                        }
                        break;
                    }
                case "HX":
                    {
                        if (GeneticCode.ToInt(geneticString.Substring(5)) >= earNumberModTreshold)
                        {
                            owner.stats.eEars--;
                        }
                        break;
                    }
                case "VH":
                    {
                        //owner.stats
                        break;
                    }
                case "VV":
                    {
                        //owner.stats
                        break;
                    }
                case "VO":
                    {
                        //owner.stats
                        break;
                    }
                case "VX":
                    {
                        //owner.stats
                        break;
                    }
                case "OH":
                    {
                        //owner.stats
                        break;
                    }
                case "OV":
                    {
                        //owner.stats
                        break;
                    }
                case "OO":
                    {
                        //owner.stats
                        break;
                    }
                case "OX":
                    {
                        //owner.stats
                        break;
                    }
                case "XH":
                    {
                        //owner.stats
                        break;
                    }
                case "XV":
                    {
                        //owner.stats
                        break;
                    }
                case "XO":
                    {
                        //owner.stats
                        break;
                    }
                case "XX":
                    {
                        //owner.stats
                        break;
                    }
            }
        }
    }
}
