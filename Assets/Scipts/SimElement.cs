using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimElement : MonoBehaviour
{

    Vector2 position;
    float orientation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool InRange(Vector2 pos,int range,float angle)
    {
        if(Vector2.Distance(position,pos)<=range)
        {

        }


        return false;
    }


}
