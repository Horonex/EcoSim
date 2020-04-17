using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimElement : MonoBehaviour
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

    public static void Add(string dialogue,float force)
    {

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

    public static SortedList<float, SimElement> GetInRange(SimElement inRangeOf, int range, float angle)
    {
        SortedList<float, SimElement> inRange = new SortedList<float, SimElement>();

        foreach (var item in elements)
        {
            if (item.InRange(inRangeOf,range, angle))
            {
                inRange.Add(Vector2.Distance(inRangeOf.position,item.position), item);
            }
        }


        return inRange;
    }


    public float GetDistance(SimElement other)
    {
        return Vector2.Distance(position, other.position);
    }

    public float GetAngle(SimElement other)
    {
        var l = new Vector2(other.position.x - position.x, other.position.y - position.y);
        return Vector2.Angle(orientation, l);
    }

    public abstract string GetPheno();

    public bool InRange(SimElement inRangeOf,int range,float angle)
    {
        if(Vector2.Distance(position, inRangeOf.position)<=range 
            && Vector2.Angle(position - inRangeOf.position, inRangeOf.orientation) <= angle / 2
            && Vector2.Angle(position - inRangeOf.position, inRangeOf.orientation) >= -angle / 2 
            && !this == inRangeOf)
        {
            return true;
        }
        return false;
    }

    //public static virtual void Spawn()
    //{

    //}

    public abstract void Tick();

}
