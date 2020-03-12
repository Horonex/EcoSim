using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrient 
{

    [SerializeField] public string molecule;
    [SerializeField] public int quantity;

    static Dictionary<string, int> conectionEnergie = new Dictionary<string, int>(16) {
        { "HH", 1 }, { "HV", -1 },{"HO",1},{"HX",-1},
        { "VH", -2 }, { "VV", -1 },{"VO",2},{"VX",-2},
        { "OH", 1 }, { "OV", -3 },{"OO",-1},{"OX",2},
        { "XH", 3 }, { "XV", 1 },{"XO",-1},{"XX",-2}};

    public static List<string> exothermicConections = Exothermics();
    private static List<string> Exothermics()
    {
        List<string> output = new List<string>();
        foreach (var s in conectionEnergie)
        {
            if(s.Value>0)
            {
                output.Add(s.Key);
            }
        }
        return output;
    }

    public static List<string> endoConections = Endothermics();
    private static List<string> Endothermics()
    {
        List<string> output = new List<string>();
        foreach (var s in conectionEnergie)
        {
            if (s.Value < 0)
            {
                output.Add(s.Key);
            }
        }
        return output;
    }


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
