using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    public class Vision : Input
    {
        new static int geneLength = 7;

        const int eyeNumberModTreshold = 15;
                
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

        public override void Express(Creature owner)
        {
            switch(geneticString.Substring(3,2))
            {
                default:
                    {
                        break;
                    }
                case "HH":
                    {
                        owner.stats.vDistance++;
                        break;
                    }
                case "HV":
                    {
                        owner.stats.vDistance--;
                        break;
                    }
                case "HO":
                    {
                        owner.stats.vAngle++;
                        break;
                    }
                case "HX":
                    {
                        owner.stats.vAngle--;
                        break;
                    }
                case "VH":
                    {
                        owner.stats.vOrientation++;
                        break;
                    }
                case "VV":
                    {
                        owner.stats.vOrientation--;
                        break;
                    }
                case "VO":
                    {
                        owner.stats.vNightVision++;
                        break;
                    }
                case "VX":
                    {
                        owner.stats.vNightVision--;
                        break;
                    }
                case "OH":
                    {
                        if(GeneticCode.ToInt(geneticString.Substring(5))>=eyeNumberModTreshold)
                        {
                            owner.stats.vEye++;
                        }
                        break;
                    }
                case "OV":
                    {
                        if (GeneticCode.ToInt(geneticString.Substring(5)) >= eyeNumberModTreshold)
                        {
                            owner.stats.vEye--;
                        }
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
