using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gene 
{
    protected string geneticString;

    public abstract void Express(Creature expressedOn);

    public string getGeneticString()
    {
        return geneticString;
    }

    public override string ToString()
    {
        return getGeneticString();
    }
}
