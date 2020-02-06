using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GeneticCode
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

    string genome;
    bool decoded = false;
    string fillerCode;
    int fillerStartIndex;
    List<Gene> genes;

    public GeneticCode(string genotype, bool checkValidity = true)
    {
        genotype.ToUpper();
        bool isValid = true;
        if (checkValidity)
        {
            isValid = IsValid(genotype);
        }
        if (isValid)
        {
            genome = genotype;
        }
    }


    public void Decode()
    {
        genes = new List<Gene>();

        int readHead = 0;

        bool canContinue = true;
        while (readHead < genome.Length && canContinue)
        {

            string prefix = genome.Substring(readHead, 2);
            switch (prefix)
            {
                default:
                    {
                        canContinue = false;
                        break;
                    }
                case "HH":
                    {
                        CreateInput(genome.Substring(readHead, Assets.Scipts.Input.GENELENGTH));
                        //output.Add("Input "+Input.Create(code.Substring(readHead,5)));
                        readHead += Assets.Scipts.Input.GENELENGTH;
                        break;
                    }
                case "HV":
                    {
                        genes.Add(new ProteinBD(genome.Substring(readHead, ProteinBD.GENELENGTH)));
                        readHead += ProteinBD.GENELENGTH;
                        break;
                    }
                case "HO":
                    {
                        genes.Add(new ProteinS(genome.Substring(readHead, ProteinS.GENELENGTH)));
                        readHead += ProteinS.GENELENGTH;
                        break;
                    }
                case "HX":
                    {
                        genes.Add(new Stat(genome.Substring(readHead, Stat.GENELENGTH)));
                        readHead += Stat.GENELENGTH;
                        break;
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
                    {
                        genes.Add(new InvalidGene(genome.Substring(readHead, InvalidGene.GENELENGTH)));
                        readHead += InvalidGene.GENELENGTH;
                        break;
                    }
                case "XX":
                    {
                        fillerCode = genome.Substring(readHead);
                        fillerStartIndex = readHead;
                        canContinue = false;
                        break;
                    }
            }
        }
        decoded = true;
    }
    /// <summary>
    /// return the genotype as only the bases
    /// </summary>
    /// <returns></returns>
    public string GetRaw()
    {
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
    /// check if gene is the right length. If too long remove bases. If too long add random bases.
    /// </summary>
    /// <param name="gene"></param>
    /// <returns></returns>
    public static string CheckGeneIntegrity(string gene)
    {
        string newGene = gene;
        //Debug.Log(newGene + " checked");
        int missing = 0;
        if (newGene.Length < 2)
        {
            return "";
        }
        switch (newGene.Substring(0, 2))
        {
            case "HH":
                {
                    missing = Assets.Scipts.Input.GENELENGTH - newGene.Length;
                    break;
                }
            case "HV":
                {
                    missing = ProteinBD.GENELENGTH - newGene.Length;
                    break;
                }
            case "HO":
                {
                    missing = ProteinS.GENELENGTH - gene.Length;
                    break;
                }
            case "HX":
                {
                    missing = Stat.GENELENGTH - gene.Length;

                    break;
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
                {
                    missing = InvalidGene.GENELENGTH - newGene.Length;
                    break;
                }
            case "XX":
                {
                    return RerollGene(newGene);
                }
        }
        if (missing < 0)
        {
            newGene = newGene.Substring(0, newGene.Length + missing);
        }
        else if (missing > 0)
        {
            for (int i = 0; i < missing; i++)
            {
                newGene += GetRandomBase();
            }
        }
        //Debug.Log(newGene + " end gene");

        return newGene;
    }
    static string RerollGene(string gene)
    {
        string rrGene = "";
        bool isOnlyX = true;
        foreach (char c in gene)
        {
            if (!(c == 'X'))
            {
                isOnlyX = false;
                break;
            }
        }
        if (isOnlyX)
        {
            rrGene += GetRandomBase() + GetRandomBase();
        }
        else
        {
            rrGene = Shuffle(gene);
        }
        rrGene = CheckGeneIntegrity(rrGene);
        return rrGene;
    }
    static string Shuffle(string gene)
    {
        var chars = gene.ToCharArray();
        for (int i = 0; i < chars.Length - 1; i++)
        {
            int r = Random.Range(i, chars.Length);
            var temp = chars[0];
            chars[0] = chars[r];
            chars[r] = temp;
        }
        string s = "";
        foreach (var c in chars)
        {
            s += c;
        }
        return s;
    }
    /// <summary>
    /// return percentage of similarity between 2 genomes.
    /// </summary>
    /// <param name="g1"></param>
    /// <param name="g2"></param>
    /// <returns></returns>
    static public float Compare(GeneticCode g1, GeneticCode g2)
    {
        int success = 0;
        int fail = 0;

        CompareGenes(g1.genes, g2.genes, ref success, ref fail);


        return success / (success + fail);
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
        foreach (var g in geneCount)
        {
            fails += g.Value;
        }
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

    }
    /// <summary>
    /// mutate the whole genome
    /// </summary>
    /// <returns></returns>
    string Mutate()
    {
        if (!decoded)
            Decode();
        return MutateGenes() + MutateFiller();
    }
    /// <summary>
    /// mutates all genes (mutation not garentee)
    /// </summary>
    /// <returns></returns>
    string MutateGenes()
    {
        string newGenes = "";
        string oldGenes = "";

        int readHead = 0;

        while (readHead < fillerStartIndex)
        {

            string prefix = genome.Substring(readHead, 2);
            switch (prefix)
            {
                case "HH":
                    {
                        MutateGene(genome.Substring(readHead, Assets.Scipts.Input.GENELENGTH), ref newGenes, ref oldGenes);
                        readHead += Assets.Scipts.Input.GENELENGTH;
                        break;
                    }
                case "HV":
                    {
                        MutateGene(genome.Substring(readHead, ProteinBD.GENELENGTH), ref newGenes, ref oldGenes);
                        readHead += ProteinBD.GENELENGTH;
                        break;
                    }
                case "HO":
                    {
                        MutateGene(genome.Substring(readHead, ProteinS.GENELENGTH), ref newGenes, ref oldGenes);
                        readHead += ProteinS.GENELENGTH;
                        break;
                    }
                case "HX":
                    {
                        MutateGene(genome.Substring(readHead, Stat.GENELENGTH), ref newGenes, ref oldGenes);
                        readHead += Stat.GENELENGTH;
                        break;
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
                    {
                        MutateGene(genome.Substring(readHead, InvalidGene.GENELENGTH), ref newGenes, ref oldGenes);
                        readHead += InvalidGene.GENELENGTH;
                        break;
                    }
                case "XX":
                    {
                        Debug.Log("Ho no :(");
                        readHead += 2;
                        break;
                    }
            }
        }

        return oldGenes + newGenes;
    }
    /// <summary>
    /// mutate a gene (mutation not garentee)
    /// </summary>
    /// <param name="gene"></param>
    /// <param name="newGenes"></param>
    /// <param name="oldGenes"></param>
    void MutateGene(string gene, ref string newGenes, ref string oldGenes)
    {
        int duplicationR = Random.Range(0, mutationDUPLICATIONrate);
        int inversionR = Random.Range(0, mutationINVERSIONrate);
        int insertionR = Random.Range(0, mutationINSERTIONrate);
        int supressionR = Random.Range(0, mutationSUPRESSIONrate);

        string newGene = "";
        string oldGene = gene;

        if (duplicationR == 0)
        {
            newGene = gene;
        }
        if (supressionR == 0)
        {
            oldGene = "";
        }
        else
        {
            if (inversionR == 0)
            {
                oldGene = "";
                for (int i = gene.Length; i > 0; i--)
                {
                    oldGene += gene.Substring(i - 1, 1);
                }
            }
            if (insertionR == 0)
            {
                int rIndex = Random.Range(0, gene.Length);
                oldGene.Remove(rIndex, 1);
                string add = "" + GetRandomBase();
                oldGene.Insert(rIndex, add);
            }
        }
        newGenes += newGene;
        oldGenes += CheckGeneIntegrity(oldGene);
    }
    /// <summary>
    /// mutate the filler
    /// </summary>
    /// <returns></returns>
    string MutateFiller()
    {
        string newFiller = GetFiller();
        int add = 0;
        for (int i = 2; i < newFiller.Length; i++)
        {
            int inversionR = Random.Range(0, mutationINVERSIONrate);
            int insertionR = Random.Range(0, mutationINSERTIONrate);
            int supressionR = Random.Range(0, mutationSUPRESSIONrate);

            if (insertionR == 0)
            {
                add++;
            }
            if (supressionR == 0)
            {
                add--;
            }
            if (inversionR == 0)
            {
                newFiller.Remove(i, 1);
                string s = "" + GetRandomBase();
                newFiller.Insert(i, s);
            }
        }
        if (add < 0)
        {
            newFiller.Substring(0, newFiller.Length + add);
        }
        else if (add > 0)
        {
            for (int i = 0; i < add; i++)
            {
                newFiller += GetRandomBase();
            }
        }

        return newFiller;
    }

    string GetGenesToString()
    {
        var builder = new StringBuilder();
        if (!decoded) Decode();
        foreach (var g in genes)
        {
            var s = g.getGeneticString();
            builder.Append(s);
        }
        return builder.ToString();
    }

    #endregion

    #region tests

    public void MutateNumber(int times)
    {
        GeneticCode newGC = this;
        float[] diffs = new float[10];
        for (int i = 0; i < times; i++)
        {
            newGC = new GeneticCode(newGC.Mutate());
            newGC.Decode();
            //Debug.Log(newGC.genes.Count);
            Debug.Log(newGC.GetGenesToString() + newGC.GetFillerLenght());
        }

    }

    public static void MutateCustom(string genome)
    {
        GeneticCode newGC = new GeneticCode(genome);
        for (int i = 0; i < 100; i++)
        {
            Debug.Log(newGC.Mutate());
        }
    }
    public static void MutateCustom()
    {
        MutateCustom("HHOXXHHHHOXXHHXXXXXXXXXXXX");
    }

    public int GetFillerLenght()
    {
        return genome.Length - fillerStartIndex;
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

    /// <summary>
    /// add a gene to the list of genes that is a input gene
    /// </summary>
    /// <param name="gene"></param>
    void CreateInput(string gene)
    {
        switch (gene.Substring(2, 1))
        {
            default:
                {

                    break;
                }
            case "H":
                {
                    genes.Add(new Vision(gene));
                    break;
                }
            case "V":
                {
                    genes.Add(new Hearing(gene));
                    break;
                }
            case "O":
            case "X":
                {
                    genes.Add(new ProteinSensibility(gene));
                    break;
                }
        }
    }

}
