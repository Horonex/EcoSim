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

    HashSet<Creature> creatures;


    // Start is called before the first frame update
    void Start()
    {
        InitRdm();
        Gene.SetMutationConstants(duplication: dupRate, inversion: inverRate, supression: supRate, insertion: inserRate);
       
        Test2();
        //Test();
        //Test3();


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

    void Test()
    {
        string gs1 = "HVHVHVHVHVHVHVHVHVHXHVHV";
        string gs2 = "HVHVVHVHHVHVVHVHHVHXHVHV";
        string f1 = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        string f2 = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        var g1 = new GeneticCode(gs1, f1);
        var g2 = new GeneticCode(gs2, f2);

        Debug.Log(GeneticCode.Compare(g1, g2));

    }

    void Test2()
    {
        int reproduceNumber = 0;
        GeneticCode GC = new GeneticCode("HVHVHVHVHVHVHVHVHVHXHVHV", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        Debug.Log(GC.GetRaw());
        int number = 100;
        var g= GC.MutateNumber(number);
        for (int i = 0; i < g.Length; i++)
        {
            var r = GeneticCode.Compare(GC, g[i]);
            Debug.Log(r);
            if(r>1-Creature.MAXGENDIFFREP)
            {
                reproduceNumber++;
            }
        }
        Debug.Log(g[number - 1].GetRaw() +" "+ g[number - 1].GetFiller());
        Debug.Log(reproduceNumber);
    }
    void Test3()
    {
        var g1 = new GeneticCode("HVHVHVHVHVHVHVHVHVHXHVHV", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        var g2 = new GeneticCode(g1);
        Debug.Log(g1.GetRaw());
        Debug.Log(g2.GetRaw());

        Debug.Log(GeneticCode.Compare(g1, g2));
    }

    void Tick()
    {
        //gather info
        foreach (var c in creatures)
        {
            c.GatherInfo();
        }
        //evaluate NN
        foreach (var c in creatures)
        {
            c.EvaluateNN();
        }
        //perform action
        foreach (var c in creatures)
        {
            c.PerformAction();
        }
        //resolve action
        //move
        foreach (var c in creatures)
        {
            c.Move();
        }


    }

    void UpdateConstants()
    {

    }


   

}
