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

    Dictionary<Nutrient, int> nutrientQuantity;

    public Creature(Vector2 pos)
    {
        position = pos;
    }

 
    // Start is called before the first frame update
    void Start()
    {
        Genotype = new GeneticCode(setGCode);
        //string s = "";

        //Debug.Log(s);

        //Test1();
        //Test4();
        //Test5(); 
        Test6();

        //Debug.Log(GetPhenotype());
    }

    void Test6()
    {
        SimElement.Add(new SimElement(new Vector2(1, 1)));
        SimElement.Add(new SimElement(new Vector2(1, 0)));
        SimElement.Add(new SimElement(new Vector2(0, 1)));
        SimElement.Add(new SimElement(new Vector2(-1, 0)));
        SimElement.Add(new SimElement(new Vector2(0, -1)));
        SimElement.Add(new SimElement(new Vector2(-1, -1)));
        SimElement.Add(new SimElement(new Vector2(1, -1)));
        SimElement.Add(new SimElement(new Vector2(-1, 1)));
        SimElement.Add(new Creature(new Vector2(2, 2)));
        //SimElement.Add(new Creature(n))

        orientation = new Vector2(1, 0);
        position = new Vector2(0, 0);
        Add(this);

        var ir = GetAttackable();

        foreach (var item in ir)
        {
            Debug.Log(item.GetAttacked());
        }
    }

    void Test5()
    {
        SimElement.Add(new SimElement(new Vector2(1,1)));
        SimElement.Add(new SimElement(new Vector2(1,0)));
        SimElement.Add(new SimElement(new Vector2(0,1)));
        SimElement.Add(new SimElement(new Vector2(-1,0)));
        SimElement.Add(new SimElement(new Vector2(0,-1)));
        SimElement.Add(new SimElement(new Vector2(2,2)));
        SimElement.Add(new SimElement(new Vector2(-1,-1)));
        SimElement.Add(new SimElement(new Vector2(1,-1)));
        SimElement.Add(new SimElement(new Vector2(-1,1)));

        orientation = new Vector2(1, 0);
        position = new Vector2(0, 0);
        Add(this);

        var ir= SimElement.GetInRange(this);

        foreach (var item in ir)
        {
            Debug.Log(item.position.x + "," + item.position.y);
        }
    }
    void Test4()
    {
        var v1 = new Vector2(1, 1);
        var v2 = new Vector2(0, 0);

        Debug.Log(Vector2.Angle(v1, v2));
    }
    void Test1()
    {
        orientation = new Vector2(-1, 1);
        position = new Vector2(0, 0);

        SimElement simelem = new SimElement();
        simelem.position = new Vector2(-2, 0);
        simelem.orientation = new Vector2(2, 2);
        InRange(simelem, CREATURERANGE, CREATUREFOA);
    }
    void Test2()
    {
        GeneticCode.MutateCustom();


        int j = 100;
        int l = 10;

        for (int n = 1; n < l; n++)
        {
            for (int i = 0; i < j; i++)
            {
                string s = "";
                for (int k = n; k < l; k++)
                {
                    s += GeneticCode.GetRandomBase();
                }

                string output = s + " -> " + GeneticCode.CheckGeneIntegrity(s);

                Debug.Log(output);
            }
        }
    }
    void Test3()
    {
        Genotype.MutateNumber(mutateNumber);
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
            t.GetEaten();
        }

    }

    public void Attack()
    {
        var targets = GetAttackable();
        foreach (var t in targets)
        {
            t.GetAttacked();
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

    public string GetAttacked()
    {
        return position.x + "," + position.y;
    }
}
