using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class Particle:SimElement, IEatable
    {
        Nutrient nutrient;

        const int decayRate = 1;


        public void Decay()
        {
            nutrient.quantity -= decayRate;
            if(nutrient.quantity<=0)
            {
                Destroy(this);
            }
        }

        public void GetEaten(Creature eater)
        {
            eater.stomac.Add(nutrient);
            Destroy(this);
        }

        public override void Tick()
        {
            Decay();
        }

        public override string GetPheno()
        {
            return nutrient.molecule;
        }
    }
}
