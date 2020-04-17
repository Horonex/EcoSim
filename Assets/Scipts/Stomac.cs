//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    public class Stomac
    {
        Creature owner;

        Dictionary<string, Nutrient> nutrients = new Dictionary<string, Nutrient>();

        public List<ProteinGene> chemicalGenes;


        public Stomac(Creature owner)
        {
            this.owner = owner;
            chemicalGenes = new List<ProteinGene>();
            foreach (var g in owner.Genotype.GetGenes())
            {
                if(g is ProteinGene)
                {
                    chemicalGenes.Add((ProteinGene)g);
                }
            }
        }

        private void React()
        {
            int reactionAffinity = 0;
            foreach(var n in nutrients.Values)
            {
                reactionAffinity+= n.quantity;
            }
            //reactionAffinity*= temperature;

            reactionAffinity = (int)Mathf.Sqrt(reactionAffinity);
            for(int i=0;i<reactionAffinity;i++)
            {
                ReactOnce();
            }
        }

        private void ReactOnce()
        {
            var PBD= owner.stats.PBreakedown.ToArray();
            var PS = owner.stats.PSynthesis.ToArray();
            int rand = Random.Range(0, PBD.Length + PS.Length);

            if(rand<PBD.Length)
            {
                string substring = PBD[rand].liaison;
                var n = GetWithSubstring(substring);
                Split(n[Random.Range(0,n.Length-1)], substring);
            }
            else
            {
                rand -= PBD.Length;
                var tails = GetWithEnding(PS[rand].liaison[1]);
                var heads = GetWithStarting(PS[rand].liaison[0]);
                Fuse(heads[Random.Range(0, heads.Length - 1)], tails[Random.Range(0, heads.Length - 1)]);
            }

        }

        private Nutrient[] GetWithSubstring(string substring)
        {
            var output = new List<Nutrient>();

            foreach (var n in nutrients.Values)
            {
                if (n.subStrings.ContainsKey(substring) && n.quantity > 0)
                {
                    output.Add(n);
                }
            }

            return output.ToArray();
        }
        private Nutrient[] GetWithStarting(char c)
        {
            var output = new List<Nutrient>();

            foreach (var n in nutrients.Values)
            {
                if (n.molecule[0]==c&&n.quantity>0)
                {
                    output.Add(n);
                }
            }

            return output.ToArray();
        }
        private Nutrient[] GetWithEnding(char c)
        {
            var output = new List<Nutrient>();

            foreach (var n in nutrients.Values)
            {
                if (n.molecule[n.molecule.Length-1] == c && n.quantity > 0)
                {
                    output.Add(n);
                }
            }

            return output.ToArray();
        }



        //eating
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


        //chem reactions
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
