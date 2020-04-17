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
            SoundManager.instance.AddSound(this);
        }

        public static void New(Creature moving,float speed)
        {

            var wave = " ";
            var force = speed * moving.stats.size;
            var position = moving.position;
            new Sound(wave,force,position);
        }

        public static void New(string dialogue,float force,Vector2 position)
        {
            new Sound(dialogue, force, position);
        }

        public override string GetPheno()
        {
            return "";
        }

        public override void Tick()
        {
        }
    }
}
