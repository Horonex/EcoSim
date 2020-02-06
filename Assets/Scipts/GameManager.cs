﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GeneticCode.SetMutationConstants(duplication: dupRate, inversion: inverRate, supression: supRate, insertion: inserRate);
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

    void UpdateConstants()
    {

    }

}
