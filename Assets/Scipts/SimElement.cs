using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimElement : MonoBehaviour
{

    public Vector2 position;
    public Vector2 orientation;

    static List<SimElement> elements = new List<SimElement>();


    public SimElement() { }
    public SimElement(Vector2 pos)
    {
        position = pos;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<SimElement> GetAll()
    {
        return elements;
    }

    public static void Add(SimElement item)
    {
        elements.Add(item);
    }


    public static List<SimElement> GetInRange(SimElement inRangeOf)
    {
        List<SimElement> inRange = new List<SimElement>();

        foreach (var item in elements)
        {
            if(item.InRange(inRangeOf,Creature.CREATURERANGE,Creature.CREATUREFOA))
            {
                inRange.Add(item);
            }
        }


        return inRange;
    }

    public bool InRange(SimElement inRangeOf,int range,float angle)
    {
        if(Vector2.Distance(position, inRangeOf.position)<=range && Vector2.Angle(position - inRangeOf.position, inRangeOf.orientation) <= angle / 2 && !this == inRangeOf)
        {
            return true;
        }
        return false;
    }

    //public static virtual void Spawn()
    //{

    //}

}
