//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    class Stomac
    {
        Creature owner;

        Dictionary<string, Nutrient> nutrients = new Dictionary<string, Nutrient>();


        public Stomac(Creature owner)
        {
            this.owner = owner;
        }

        private void React()
        {

        }


        public void Add(Nutrient item)
        {
            if(nutrients.ContainsKey(item.molecule))
            {
                nutrients[item.molecule].Transfer(item);
            }
            else
            {
                Nutrient newNutrient = new Nutrient(item.molecule);
                newNutrient.Transfer(item);
                nutrients.Add(item.molecule, newNutrient);
            }
        }

        private void Fuse(Nutrient head,Nutrient tail)
        {
            head.Decrease();
            tail.Decrease();
            Nutrient newNutrient = new Nutrient(head.molecule + tail.molecule, 1);
            Add(newNutrient);
        }

        private void Split(Nutrient nutrient,string sString)
        {
            nutrient.Decrease();
            int cutIndex = Random.Range(0,nutrient.subStrings[sString].Count);

            Nutrient head = new Nutrient(nutrient.molecule.Substring(0, cutIndex),1);
            Nutrient tail = new Nutrient(nutrient.molecule.Substring(cutIndex),1);
            Add(head);
            Add(tail);
        }
    }
}
