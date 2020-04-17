using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scipts;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool useSeed;
    [SerializeField] int seed;
    [SerializeField] bool useSavedRandomState;
    [SerializeField] Random.State rState;

    [SerializeField] int dupRate;
    [SerializeField] int inverRate;
    [SerializeField] int supRate;
    [SerializeField] int inserRate;

    [SerializeField] Creature toInst;
    [SerializeField] Creature toPrint;

    public HashSet<Creature> creatures;
    const int MaxCreature = 1000;

    public static GameManager instance;

    public int time = 0;

    int test = 0;
    int maxTest = -1;

    float lastTick = 0;
    public float TimeBetweenTick;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        InitRdm();
        Gene.SetMutationConstants(duplication: dupRate, inversion: inverRate, supression: supRate, insertion: inserRate);

        creatures = new HashSet<Creature>();
        var fill = GeneticCode.GetNewFiller();
        SpawnCreature(new GeneticCode("HXHOOOOOHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh", fill));
        SpawnCreature(new GeneticCode("HXHVHHHxHXHOhhhxHXHOhhhxHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh", fill));


        Tick();
    }
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitRdm()
    {
        if (useSavedRandomState)
        {
            Random.state = rState;
        }
        else if (useSeed)
        {
            Random.InitState(seed);
        }
    }

    void Tick()
    {
        lastTick = Time.time;
        HashSet<Creature> ticking = new HashSet<Creature>(creatures);


        //gather info
        foreach (var c in ticking)
        {
            c.GatherInfo();
        }
        //evaluate NN
        foreach (var c in ticking)
        {
            c.EvaluateNN();
        }
        //perform action
        foreach (var c in ticking)
        {
            //c.PerformAction();
            c.PerformAction(Creature.actionTypes.attack);
            c.PerformAction(Creature.actionTypes.eat);
            c.PerformAction(Creature.actionTypes.reproduce);
            c.PerformAction(Creature.actionTypes.scream);
            //c.PerformAction(Creature.actionTypes.signal);
        }
        //resolve action
        foreach (var c in ticking)
        {
            c.ResolveActions();
        }

        //move
        foreach (var c in ticking)
        {
            c.PerformAction(Creature.actionTypes.move);
        }

        TickNext();
        test++;
        time++;
        Debug.Log(creatures.Count);
        
    }

    void TickNext()
    {
        if (test != maxTest)
        {
            var t = Time.time - lastTick;

            if (t > TimeBetweenTick)
            {
                Tick();
            }
            else
            {
                Invoke("Tick", TimeBetweenTick - t);
            }
        }
    }


    public void SpawnCreature(Brain brain,GeneticCode GCode)
    {
        if (creatures.Count < MaxCreature)
        {

            Creature newCreature = Instantiate(toInst);

            newCreature.Init(brain, GCode, new Vector2(0, 0), new Vector2(1, 0));

            creatures.Add(newCreature);
        }

        //creatures.Add()
    }
    public void SpawnCreature(GeneticCode GCode)
    {
        if(creatures.Count<MaxCreature)
        {

        Creature newCreature = Instantiate(toInst);

            newCreature.Init(BrainMaker.OneSizeBrain(newCreature), GCode,new Vector2(0,0),new Vector2(1,0));


        creatures.Add(newCreature);
        }
    }


}
