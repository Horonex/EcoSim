using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : SimElement , IAttackable
{
    public const int CREATURERANGE = 10;
    public const float CREATUREFOA = 90;
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

    Dictionary<string, int> nutrientQuantity;

    public Creature(Vector2 pos)
    {
        position = pos;
    }

 
    // Start is called before the first frame update
    void Start()
    {
        //Genotype = new GeneticCode(setGCode);
        //string s = "";

        //Debug.Log(s);

        //Debug.Log(GetPhenotype());
    }


    #region tests
    


    #endregion

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

    public void Move()
    {

    }
    //public void Turn()
    //{

    //}

    public void Eat()
    {
        var targets = GetEatable();
        foreach (var t in targets)
        {
            t.GetEaten(this);
        }

    }

    public void Attack()
    {
        var targets = GetAttackable();
        foreach (var t in targets)
        {
            t.GetAttacked(this);
        }
    }

    public void Signal()
    {

    }

    private List<IAttackable> GetAttackable()
    {
        var ir = GetInRange(this);
        var output = new List<IAttackable>();
        foreach (var item in ir)
        {
            if(item is IAttackable)
            {
                output.Add((IAttackable)item);
            }
        }

        return output;
    }

    private List<IEatable> GetEatable()
    {
        var ir = GetInRange(this);
        var output = new List<IEatable>();
        foreach (var item in ir)
        {
            if (item is IEatable)
            {
                output.Add((IEatable)item);
            }
        }

        return output;
    }

    public void GetAttacked(Creature attacker)
    {
        //return position.x + "," + position.y;
    }
}
