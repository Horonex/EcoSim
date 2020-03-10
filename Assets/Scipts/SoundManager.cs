using Assets.Scipts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;



    List<Sound> sounds;

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
}
