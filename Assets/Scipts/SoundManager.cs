using Assets.Scipts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;



    public static List<Sound> sounds=new List<Sound>();

    public void AddSound(Sound s)
    {
        sounds.Add(s);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Sound> GetAllSounds()
    {
        return sounds;
    }
        
    public SortedList<string,float> GetEarable(Creature listener)
    {
        return GetEarable(listener.position);
    }
    public SortedList<string, float> GetEarable(Vector2 listenerAt)
    {
        SortedList<string, float> output = new SortedList<string, float>();
        foreach (var s in sounds)
        {
            var dis = Vector2.Distance(s.position, listenerAt);
            if (dis<s.force)
            {
                output.Add(s.wave, s.force - dis);
            }
        }
        return output;
    }

    internal SortedList<float, Sound> GetHearable(Creature listener)
    {
        SortedList<float, Sound> output = new SortedList<float, Sound>();
        foreach (var s in sounds)
        {
            float f = s.force - listener.GetDistance(s);
            if(f>0)
            {
                output.Add(f, s);
            }
        }

        return output;
    }
}
