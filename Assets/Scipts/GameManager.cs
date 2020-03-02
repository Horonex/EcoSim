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
        //Gene.SetMutationConstants(duplication: dupRate, inversion: inverRate, supression: supRate, insertion: inserRate);
        Test();

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
        string gs1 = "hvhvhvhvhvhvhvhvhvhv";
        string gs2 = "hvhvhvhvhvhvhvhvhvhxhvhv";
        string f1 = "xxxxxxxxxx";
        string f2 = "xxhhxxxxhx";

        var g1 = new GeneticCode(gs1, f1); 
        var g2 = new GeneticCode(gs2, f2);

        Debug.Log(GeneticCode.Compare(g1, g2));

    }



    void UpdateConstants()
    {

    }


   

}
