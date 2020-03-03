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

    // Start is called before the first frame update
    void Start()
    {
        InitRdm();
        Gene.SetMutationConstants(duplication: dupRate, inversion: inverRate, supression: supRate, insertion: inserRate);
        //Test2();
        //Test();
        Test3();


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
        GeneticCode GC = new GeneticCode("HVHVHVHVHVHVHVHVHVHXHVHV", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        Debug.Log(GC.GetRaw());
        var g= GC.MutateNumber(100);
        for (int i = 0; i < g.Length; i++)
        {
            Debug.Log(GeneticCode.Compare(GC, g[i]));
        }
        Debug.Log(GC.GetRaw());
        Debug.Log(g[99].GetRaw());
        Debug.Log(GC.Compare(g[99]));

    }
    void Test3()
    {
        var g1 = new GeneticCode("HVHVHVHVHVHVHVHVHVHXHVHV", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        var g2 = new GeneticCode(g1);
        Debug.Log(g1.GetRaw());
        Debug.Log(g2.GetRaw());



        Debug.Log(GeneticCode.Compare(g1, g2));
    }



    void UpdateConstants()
    {

    }


   

}
