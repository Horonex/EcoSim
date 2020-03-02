using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    abstract public class Input:Gene
    {

        public static Input GetNewInput(string gString)
        {
            switch (gString.Substring(2, 1))
            {
                default:
                    {

                        break;
                    }
                case "H":
                    {
                        return new Vision(gString);
                    }
                case "V":
                    {
                        return new Hearing(gString);
                    }
                case "O":
                case "X":
                    {
                        return new ProteinSensibility(gString);
                    }
            }
            return null;
        }


    }
}
