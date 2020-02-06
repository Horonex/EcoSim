using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    const int PHENOTYPEFILLERLENGHT=16;
    /// <summary>
    /// Maximum gentic difference for successful reproduction (percentage)
    /// </summary>
    static float MAXGENDIFFREP = 0.05f;


    public GeneticCode Genotype;
    [SerializeField] string setGCode;
    [SerializeField] int mutateNumber=10;

    [SerializeField] public int[] stats = new int[16];

    List<ProteinBD> proteinBreakDowns;
    List<ProteinS> proteinSynthesis;

    Dictionary<Nutrient, int> nutrientQuantity;


 
    // Start is called before the first frame update
    void Start()
    {
        Genotype = new GeneticCode(setGCode);
        //string s = "";

        //Debug.Log(s);
        Genotype.MutateNumber(mutateNumber);
        //GeneticCode.MutateCustom();


        //int j = 100;
        //int l = 10;

        //for (int n = 1; n < l; n++)
        //{
        //    for (int i = 0; i < j; i++)
        //    {
        //        string s = "";
        //        for (int k = n; k < l; k++)
        //        {
        //            s += GeneticCode.GetRandomBase();
        //        }

        //        string output = s + " -> " + GeneticCode.CheckGeneIntegrity(s);

        //        Debug.Log(output);
        //    }
        //}






        //Debug.Log(GetPhenotype());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string GetGeneticCode()
    {
        return Genotype.GetRaw();
    }

    public string GetPhenotype()
    {
        ClampStats();
        string pheno = "";
        for (int i = 0; i < stats.Length; i++)
        {
            for (int j = 10; j < Stat.CLAMPVALUES[i, 1]; j *= 10)
            {
                if (stats[i] / j <= 0)
                {
                    pheno += 0;
                }
            }
            pheno += stats[i];
        }        
        pheno += Genotype.GetFiller().Substring(2, PHENOTYPEFILLERLENGHT);
        return pheno;
    }
    
    void ClampStats()
    {
        stats[0] = stats[0] % (Stat.CLAMPVALUES[0, 1] - Stat.CLAMPVALUES[0, 0]+1) + Stat.CLAMPVALUES[0, 0];
        for (int i = 1; i < stats.Length; i++)
        {
            if (stats[i] < Stat.CLAMPVALUES[i, 0])
            {
                stats[i] = Stat.CLAMPVALUES[i, 0];
            }
            else if(stats[i] > Stat.CLAMPVALUES[i, 1])
            {
                stats[i] = Stat.CLAMPVALUES[i, 1];
            }
        }
    }

    public void Reproduce(Creature other)
    {
        if(Genotype.Compare(other.Genotype)>1 -MAXGENDIFFREP)
        {

        }
    }
}
