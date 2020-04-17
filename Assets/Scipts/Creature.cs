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
    public static float MAXGENDIFFREP = 0.5f;

    [SerializeField] string setGCode;
    [SerializeField] int mutateNumber=10;

    public enum GeneTypes {input,hearing,vision,pheromone,proteinGene,proteinBD,proteinS,BrainGene,statGene,outputGeneP }

    //List<ProteinBD> proteinBreakDowns;
    //List<ProteinS> proteinSynthesis;
    public GeneticCode Genotype;

    public Stats stats;

    public Brain brain;

    public Stomac stomac;

    //private float[] ;

    private int lastReproductionTime;

    public void Init(Brain brain, GeneticCode GCode,Vector2 pos,Vector2 ang)
    {
        this.brain = brain;
        Genotype = GCode;
        stats = new Stats(new List<StatGene>());
        stomac = new Stomac(this);
        stats = new Stats(Genotype.GetGenes(), this);

        brain.owner = this;

        position = pos;
        orientation = ang;

    }


    //Dictionary<string, int> nutrientQuantity;

    public enum actionTypes {move, attack, eat, reproduce, signal, scream}


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

            GameManager.instance.SpawnCreature(BrainMaker.OneSizeBrain(this, this), Genotype);
        }
        else
        {
            Debug.Log("creature too different");
        }
    }
    public void Reproduce()
    {
        var ir = new List<Creature>();
        foreach (var item in GameManager.instance.creatures)
        {
            if(!(item==this))
            //if (item.InRange(this, 100, 360))
            {
                ir.Add(item);
            }
        }
        if(ir.Count>0)
        Reproduce(ir[0]);

        AddEnergie((int)( Stats.ReproductionEnergyCostMod * -1));
    }


    public void Eat()
    {
        var targets = GetEatable();
        foreach (var t in targets)
        {
            t.GetEaten(this);
        }
        AddEnergie((int)(Stats.EatingEnergyCostMod * -1));

    }

    public void Attack()
    {
        var targets = GetAttackable();
        foreach (var t in targets)
        {
            t.GetAttacked(this);
        }
        AddEnergie((int)(stats.strength * Stats.AttackEnergyCostMod * -1));
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
        AddHP(-attacker.stats.strength);
    }

    public void AddEnergie(float amount)
    {
        stats.energy+= amount;
        if(stats.energy < 0)
        {
            AddHP(stats.energy);
            stats.energy = 0;
        }
    }

    public void AddHP(float amount)
    {
        stats.hp += amount;
    }

    public void Scream(string dialogue, float force)
    {

    }

    public void Scream()
    {

    }


    #region lifeCycle
    public void ResolveActions()
    {
        transform.position = new Vector3(position.x, position.y);
        transform.Rotate(new Vector3(0, 0,transform.rotation.eulerAngles.x - Vector2.Angle(Vector2.right, orientation)));

        if (stats.hp < 0)
        {
            Die();
        }

    }

    public void GatherInfo()
    {
        brain.GatherInfo(this);
    }

    public void EvaluateNN()
    {
        brain.Evaluate();
    }

    public void PerformAction(actionTypes actionToPerform)
    {
        switch (actionToPerform)
        {
            case actionTypes.move:
                Move(brain.movement);
                //Debug.Log(brain.movement);
                break;
            case actionTypes.attack:
                if (brain.willAttack)
                {
                    Attack();
                }
                break;
            case actionTypes.eat:
                if (brain.willEat) 
                {
                    Eat();
                }
                break;
            case actionTypes.reproduce:
                if (brain.willReproduce && Time.time - lastReproductionTime > -1) 
                {
                    Reproduce();
                    lastReproductionTime = GameManager.instance.time;
                }
                break;
            case actionTypes.signal:
                foreach (var s in brain.signals)
                {

                }
                break;
            case actionTypes.scream:
                if (brain.screamForce > 0) 
                {
                    Debug.Log("scream");
                    Scream(brain.dialogue, brain.screamForce);
                }
                break;
        }
    }

    public void Move(float angle,float distance)
    {

        var a = Vector2.Angle(Vector2.right, orientation);
        a += Vector2.Angle(Vector2.right, orientation);

        a *= Mathf.Deg2Rad;



        Vector2 motion = new Vector2(Mathf.Cos(a) * distance*stats.speed, Mathf.Sin(a) * distance * stats.speed);
        position += motion;
        orientation = motion.normalized;

        Sound.New(this, distance);
    }
    public void Move(Vector2 move)
    {
        position += move * stats.speed;
        orientation = move.normalized;



    }

    #endregion

    public float[] Hear()
    {
        float[] output = new float[8 * 3];

        var sounds = SoundManager.instance.GetHearable(this);

        int i = 0;
        foreach (var item in sounds)
        {
            if(sounds.Count-i<8)
            {
                i++;
                continue;
            }
            else
            {
                output[i] = GetAngle(item.Value);
                output[i + 1] = GeneticCode.ToInt(item.Value.wave);
                output[i + 2] = item.Key;
                i++;
            }

        }

        return output;
    }

    public float[] Look()
    {
        float[] output = new float[8 * 3];


        int i = 0;
        foreach (var item in GetInRange(this, stats.vDistance, stats.vAngle))
        {

            if(i>=8)
            {
                
                break;
            }
            output[i]=GeneticCode.ToInt(item.Value.GetPheno());
            output[i+1]=GetAngle(item.Value);
            output[i+2]= GetDistance(item.Value);
            i++;
        }

        return output;
    }
    public float GetHp()
    {
        return stats.hp;
    }
    public float GetEnergy()
    {
        return stats.energy;
    }
    //public float[] Sense()
    //{

    //}
    public float Pregnant()
    {
        return 0;
    }

    void Die()
    {
        GameManager.instance.creatures.Remove(this);
        Destroy(this.gameObject);
    }

    public override void Tick()
    {
       // throw new System.NotImplementedException();
    }

    public List<Gene> GetGenes(params GeneTypes[] type)
    {

        HashSet<string> strings=new HashSet<string>();
        for (int i = 0; i < type.Length; i++)
        {
            switch (type[i])
            {
                case GeneTypes.BrainGene:
                    strings.Add("VH");
                    break;
                case GeneTypes.hearing:
                    strings.Add("HHV");
                    break;
                case GeneTypes.input:
                    strings.Add("HH");
                    break;
                case GeneTypes.pheromone:
                    strings.Add("HHO");
                    strings.Add("HHX");
                    break;
                case GeneTypes.proteinBD:
                    strings.Add("HV");
                    break;
                case GeneTypes.proteinGene:
                    strings.Add("HV");
                    strings.Add("HO");
                    break;
                case GeneTypes.proteinS:
                    strings.Add("HO");
                    break;
                case GeneTypes.statGene:
                    strings.Add("HX");
                    break;
                case GeneTypes.vision:
                    strings.Add("HHH");
                    break;
                case GeneTypes.outputGeneP:
                    strings.Add("VVV");
                    strings.Add("VVH");
                    break;
            }
        }

        var output = new List<Gene>();


        foreach (var g in Genotype.GetGenes())
        {
            if (strings.Contains(g.GetGeneticString().Substring(0, 3)) || strings.Contains(g.GetGeneticString().Substring(0, 2))) 
            {
                output.Add(g);
            }
        }


        return output;
    }

    public override string GetPheno()
    {
        return GetPhenotype();
    }
}
