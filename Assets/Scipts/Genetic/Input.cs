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
        private enum Type { vision,earing,pheromone };
        private Type type;

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

        public static SortedList<Input,int> ToSortedList(List<Input> list)
        {
            var output = new SortedList<Input, int>();
            foreach (var item in list)
            {
                output.Add(item, (int)item.type);
            }
            return output;
        }

    }
}
