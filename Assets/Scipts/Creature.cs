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
    public static float MAXGENDIFFREP = 0.05f;

    public GeneticCode Genotype;
    [SerializeField] string setGCode;
    [SerializeField] int mutateNumber=10;

    //List<ProteinBD> proteinBreakDowns;
    //List<ProteinS> proteinSynthesis;

    public Stats stats;

    private Brain brain;

    private Stomac stomac;


    //Dictionary<string, int> nutrientQuantity;

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
        string pheno = "";
        pheno += stats.GetPheno();
        pheno += Genotype.GetFiller().Substring(2, PHENOTYPEFILLERLENGHT);
        return pheno;
    }
    
    public void Reproduce(Creature other)
    {
        if(Genotype.Compare(other.Genotype)>1 -MAXGENDIFFREP)
        {

        }
    }
    public void Reproduce()
    {

    }


    public void MoveTo()
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

    public void AddEnergie(int amount)
    {
        stats.energy+= amount;
        if(stats.energy < 0)
        {
            Die();
        }
    }

    public void Scream(string dialogue, float force)
    {

    }

    #region lifeCycle

    public void GatherInfo()
    {
        brain.GatherInfo(this);
    }

    public void EvaluateNN()
    {
        brain.Evaluate();
    }

    public void PerformAction()
    {

    }

    public void Move()
    {

    }

    #endregion

    void Die()
    {

    }

}
