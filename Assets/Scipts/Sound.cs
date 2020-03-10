using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    public class Sound:SimElement
    {
        public string wave;
        public float force;

        public Sound(string dialogue,float force,Vector2 position)
        {
            wave = dialogue;
            this.force = force;
            this.position = position;
        }

        public Sound(Creature moving,float speed)
        {
            wave = " ";
            force = speed * moving.stats.size;
            position = moving.position;
        }

    }
}
