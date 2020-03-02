using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrient 
{

    [SerializeField] public string molecule;
    [SerializeField] int quantity;

    public Dictionary<string, List<int>> subStrings;

    public Nutrient(string molecule,int quantity=0)
    {
        this.molecule = molecule;
        this.quantity = quantity;

        for(int i=0;i<molecule.Length-1;i++)
        {
            string sString = molecule.Substring(i, 2);
            if (subStrings.ContainsKey(sString))
            {
                subStrings[sString].Add(i);
            }
            else
            {
                subStrings.Add(sString, new List<int>() { i });
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transfer(Nutrient nutrient)
    {
        quantity += nutrient.quantity;
        nutrient.quantity = 0;
    }

    public void Decrease()
    {
        quantity--;
    }

}
