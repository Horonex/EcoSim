using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scipts
{
    public abstract class Gene
    {
        #region mutation constants
        /// <summary>
        /// one in _ chance
        /// </summary>
        static int mutationDUPLICATIONrate = 100;
        /// <summary>
        /// one in _ chance
        /// </summary>
        static int mutationINVERSIONrate = 100;
        /// <summary>
        /// one in _ chance
        /// </summary>
        static int mutationSUPRESSIONrate = 100;
        /// <summary>
        /// one in _ chance
        /// </summary>
        static int mutationINSERTIONrate = 100;

        public static void SetMutationConstants(int duplication = 100, int inversion = 100, int supression = 100, int insertion = 100)
        {
            mutationDUPLICATIONrate = duplication;
            mutationINSERTIONrate = insertion;
            mutationSUPRESSIONrate = supression;
            mutationINSERTIONrate = insertion;
        }

        #endregion

        public static int geneLength=1;

        protected string geneticString;
        protected string Prefix;


        public static int MutationINSERTIONrate { get => mutationINSERTIONrate; }
        public static int MutationSUPRESSIONrate { get => mutationSUPRESSIONrate; }
        public static int MutationINVERSIONrate { get => mutationINVERSIONrate; }
        public static int MutationDUPLICATIONrate { get => mutationDUPLICATIONrate; }
        public int GeneLength { get => geneLength; }

        public static Gene GetNewGene(string gString)
        {

            string prefix = gString.Substring(0, 2);
            switch (prefix)
            {
                default:
                    {
                        break;
                    }
                case "HH":
                    {
                        return Input.GetNewInput(gString);
                    }
                case "HV":
                    {
                        return new ProteinBD(gString);
                    }
                case "HO":
                    {
                        return new ProteinS(gString);
                    }
                case "HX":
                    {
                        return new Stat(gString);
                    }
                case "VH":

                case "VV":

                case "VO":

                case "VX":

                case "OH":

                case "OV":

                case "OO":

                case "OX":

                case "XH":

                case "XV":

                case "XO":

                case "XX":
                    {
                        return new InvalidGene(gString);
                    }
            }
            return null;
        }

        public abstract int GetLength();

        public string getGeneticString()
        {
            return geneticString;
        }

        public override string ToString()
        {
            return getGeneticString();
        }

        public List<Gene> Mutate()
        {
            int duplicationR = Random.Range(0, mutationDUPLICATIONrate);
            int inversionR = Random.Range(0, mutationINVERSIONrate);
            int insertionR = Random.Range(0, mutationINSERTIONrate);
            int supressionR = Random.Range(0, mutationSUPRESSIONrate);

            List<Gene> output = new List<Gene>();


            if (duplicationR == 0)
            {
                Debug.Log("duplicate");
                output.Add(GetNewGene(getGeneticString()));
            }
            if (!(supressionR == 0))
            {
                if (inversionR == 0)
                {
                    Invert();
                    
                }
                if (insertionR == 0)
                {
                    Insert();
                }

            }

            //newGenes += newGene;
            //oldGenes += CheckGeneIntegrity(oldGene);

            return new List<Gene>() {this };
        }

        private void Invert()
        {
            Debug.Log("invert");
            StringBuilder sBuilder = new StringBuilder();
            for (int i = geneticString.Length; i > 0; i--)
            {
                sBuilder.Append(geneticString[i - 1]);
            }
            geneticString = sBuilder.ToString();
        }

        private void Insert()
        {
            Debug.Log("insert");
            int rIndex = Random.Range(0, geneticString.Length);
            geneticString.Remove(rIndex, 1);
            string add = "" + GeneticCode.GetRandomBase();
            geneticString.Insert(rIndex, add);
        }

        public string GetRaw()
        {
            return geneticString;
        }

        /// <summary>
        /// check if gene is the right length. If too long remove bases. If too long add random bases.
        /// </summary>
        /// <param name="gene"></param>
        /// <returns></returns>
        public Gene CheckGeneIntegrity(string gene)
        {
            if(!(geneticString.Substring(0,2)==Prefix))
            {
                return GetNewGene(geneticString);
            }
            return this;
        }

        static protected string Pad(string gString, int quantity)
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(gString);
            while(quantity-->0)
            {
                sBuilder.Append(GeneticCode.GetRandomBase());
            }

            return sBuilder.ToString();
        }


    }
}
