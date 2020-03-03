using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GeneticCode
{


    string genome;
    bool decoded = false;
    bool encoded = false;
    string fillerCode;
    const int fillerLength = 32;
    List<Gene> genes;

    public GeneticCode(string genotype,string filler, bool checkValidity = true)
    {
        genotype= genotype.ToUpper();
        filler=filler.ToUpper();
        bool isValid = true;
        if (checkValidity)
        {
            isValid = IsValid(genotype) && IsValid(filler);
        }
        if (isValid)
        {
            genome = genotype;
            fillerCode = filler;
            encoded = true;
        }
        else
        {
            throw new System.Exception("Genetic Code Invalid! Make Sure The Genetic Code Only Use H V O or X");

        }
    }
    public GeneticCode(GeneticCode parent)
    {
        
        genes = new List<Gene>();
        foreach (var g in parent.GetGenes())
        {
            foreach (var m in g.Mutate())
            {
                genes.Add(m);
            }
        }
        fillerCode = parent.MutateFiller();
        decoded = true;
    }

    private void Encode()
    {
        StringBuilder sBuilder = new StringBuilder();
        foreach (var g in genes)
        {
            sBuilder.Append(g.GetRaw());
        }
        genome = sBuilder.ToString();
    }

    public void Decode()
    {
        if (decoded) return;

        genes = new List<Gene>();
        int readHead = 0;

        while(readHead<genome.Length)
        {
            Gene newGene = Gene.GetNewGene(genome.Substring(readHead));
            readHead += newGene.GetLength();
            genes.Add(newGene);
        }

        decoded = true;
    }
    /// <summary>
    /// return the genotype as only the bases
    /// </summary>
    /// <returns></returns>
    public string GetRaw()
    {
        if(!encoded)
        {
            Encode();
        }
        return genome;
    }
    /// <summary>
    /// return the filler part of the genome
    /// </summary>
    /// <returns></returns>
    public string GetFiller()
    {
        if (!decoded)
            Decode();
        return fillerCode;
    }
    /// <summary>
    /// get the list of genes
    /// </summary>
    /// <returns></returns>
    public List<Gene> GetGenes()
    {
        if (!decoded)
            Decode();
        return genes;
    }

    #region utility (conversion, check, comparisons, mutation)
    /// <summary>
    /// convert the genotype to an int (genotype is base 4)
    /// </summary>
    /// <returns></returns>
    int ToInt()
    {
        return ToInt(genome);
    }
    /// <summary>
    /// convert a genetic String to an int (genotype is base 4)
    /// </summary>
    /// <param name="geneticString"></param>
    /// <returns></returns>
    public static int ToInt(string geneticString)
    {
        int value = 0;
        for (int i = 0; i < geneticString.Length; i++)
        {
            int converted = 0;
            switch (geneticString.Substring(i, 1))
            {
                default:
                    {
                        throw new System.Exception("Invalide Genetic Code");
                    }
                case "H":
                    {
                        converted = 0;
                        break;
                    }
                case "V":
                    {
                        converted = 1;
                        break;
                    }
                case "O":
                    {
                        converted = 2;
                        break;
                    }
                case "X":
                    {
                        converted = 3;
                        break;
                    }
            }
            value += (int)Mathf.Pow(4, i) * converted;
        }
        return value;
    }
    /// <summary>
    /// convert a genetic string to int and check if even
    /// </summary>
    /// <param name="geneticString"></param>
    /// <returns></returns>
    static bool IsEven(string geneticString)
    {
        return ToInt(geneticString) % 2 == 0;
    }
    /// <summary>
    /// check if the genotype contain only the right bases(H,V,O and X)
    /// </summary>
    /// <returns></returns>
    bool IsValid()
    {
        return IsValid(genome);
    }
    /// <summary>
    /// check if a genetic string contain only the right bases(H,V,O and X)
    /// </summary>
    /// <param name="geneticString"></param>
    /// <returns></returns>
    static bool IsValid(string geneticString)
    {
        bool isValid = true;
        foreach (char c in geneticString)
        {
            if (c == 'H' || c == 'V' || c == 'O' || c == 'X')
            {
                continue;
            }
            else
            {
                isValid = false;
                break;
            }
        }
        return isValid;
    }
    /// <summary>
    /// Convert an int into a genetic base. 0 to 3. 
    /// </summary>
    /// <param name="newBase"></param>
    /// <returns></returns>
    public static char GetNewBase(int newBase)
    {
        char c = ' ';
        switch (Mathf.Abs(newBase) % 4)
        {
            case 0:
                {
                    c = 'H';
                    break;
                }
            case 1:
                {
                    c = 'V';
                    break;
                }
            case 2:
                {
                    c = 'O';
                    break;
                }
            case 3:
                {
                    c = 'X';
                    break;
                }
        }
        return c;
    }
    /// <summary>
    /// return a random genetic base
    /// </summary>
    /// <returns></returns>
    public static char GetRandomBase()
    {
        return GetNewBase(Random.Range(0, 4));
    }

    /// <summary>
    /// return percentage of similarity between 2 genomes.
    /// </summary>
    /// <param name="g1"></param>
    /// <param name="g2"></param>
    /// <returns></returns>
    static public float Compare(GeneticCode g1, GeneticCode g2)
    {
        if (!g1.decoded) g1.Decode();
        if (!g2.decoded) g2.Decode();
        int success = 0;
        int fail = 0;

        CompareGenes(g1.genes, g2.genes, ref success, ref fail);

        CompareFiller(g1.fillerCode, g2.fillerCode, ref success, ref fail);

        Debug.Log(success + "s");
        Debug.Log(fail + "f");

        return (float)success / (float)(success + fail);
    }
    /// <summary>
    /// compare this genome with an other one
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public float Compare(GeneticCode other)
    {
        return Compare(this, other);
    }
    /// <summary>
    /// compare the genes of 2 genomes and export the number of similarity and differences
    /// </summary>
    /// <param name="g1"></param>
    /// <param name="g2"></param>
    /// <param name="successes"></param>
    /// <param name="fails"></param>
    static void CompareGenes(List<Gene> g1, List<Gene> g2, ref int successes, ref int fails)
    {
        Dictionary<string, int> geneCount = new Dictionary<string, int>();
        if(g2.Count<g1.Count)
        {
            var temp = g1;
            g1 = g2;
            g2 = temp;
        }


        foreach (var g in g1)
        {
            string s = g.getGeneticString();
            if (!geneCount.ContainsKey(s))
            {
                geneCount.Add(s, 1);
            }
            else
            {
                geneCount[s]++;
            }
        }
        foreach (var g in g2)
        {
            string s = g.getGeneticString();
            if (geneCount.ContainsKey(s) && geneCount[s] > 0)
            {
                geneCount[s]--;
                successes++;
            }
            else
            {
                fails++;
            }
        }

        //foreach (var g in geneCount.Values)
        //{
        //    fails += g;
        //    Debug.Log("fail++");

        //}
    }
    /// <summary>
    /// compare every bases of the filler of 2 genomes and export the number of similarity and differences
    /// </summary>
    /// <param name="f1"></param>
    /// <param name="f2"></param>
    /// <param name="successes"></param>
    /// <param name="fails"></param>
    static void CompareFiller(string f1, string f2, ref int successes, ref int fails)
    {
        if (f1.Length > f2.Length)
        {

            var temp = f1;
            f1 = f2;
            f2 = temp;

        }

        for (int i=0;i<f1.Length;i++)
        {
            if(f1[i]==f2[i])
            {
                successes++;
            }
            else
            {
                fails++;
            }
        }
    }

    /// <summary>
    /// mutate the filler
    /// </summary>
    /// <returns></returns>
    string MutateFiller()
    {
        string nFiller = fillerCode;

        //List<int> indexs = new List<int>();
        for (int i = 0; i < fillerLength; i++)
        {
            if (Random.Range(0, Gene.MutationINSERTIONrate) == 0)
            {
                nFiller.Remove(i, 1);
                nFiller.Insert(i, GetRandomBase().ToString());
            }
        }
        //StringBuilder sBuilder = new StringBuilder();
        //var indexArray = indexs.ToArray();
        //for (int i = 0; i < indexArray.Length; i++)
        //{
        //    if (i == 0)
        //    {
        //        sBuilder.Append(GetFiller().Substring(0, indexArray[0]));
        //        GetFiller().
        //        sBuilder.Append(GetRandomBase());
        //    }
        //    else if (i == indexArray.Length - 1)
        //    {
        //        sBuilder.Append(GetFiller().Substring(indexArray[i]) + 1);
        //    }
        //    else
        //    {
        //        sBuilder.Append(GetFiller().Substring(indexArray[i] + 1, indexArray[i + 1] - indexArray[i] - 1));
        //        sBuilder.Append(GetRandomBase());
        //    }
        //}

        return nFiller;
    }

    string GetGenesToString()
    {
        if (!decoded) Decode();
        var builder = new StringBuilder();
        foreach (var g in genes)
        {
            var s = g.getGeneticString();
            builder.Append(s);
        }
        return builder.ToString();
    }

    #endregion

    #region tests

    public GeneticCode[] MutateNumber(int times)
    {
        GeneticCode[] gCodes = new GeneticCode[times];
        gCodes[0] = this;
        for (int i = 1; i < times; i++)
        {
            gCodes[i] = new GeneticCode(gCodes[i - 1]);
        }
        return gCodes;
    }


    public int GetFillerLenght()
    {
        return fillerLength;
    }

    #endregion

    public override string ToString()
    {
        string s = "";
        foreach(var g in GetGenes())
        {
            s += g.ToString() + "\n";
        }
        return s;
    }

    public static string GetNewFiller()
    {
        StringBuilder sBuilder = new StringBuilder();
        for(int i=0;i<fillerLength;i++)
        {
            sBuilder.Append(GetRandomBase());
        }
        return sBuilder.ToString();
    }

}
