using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    public class Stats
    {
        public int size, hp, maxHp, energy, maxEnergy;

        int GestationTime;
        int GestationMaturity;
        int speed;
        int strength;


        public Stats(List<StatGene> sGenes)
        {
            foreach(var g in sGenes)
            {
                switch(g.type)
                {
                    case StatGene.StatType.size:
                        {
                            size += g.value;
                            break;
                        }
                    case StatGene.StatType.speed:
                        {
                            speed += g.value;
                            break;
                        }
                    case StatGene.StatType.strength:
                        {
                            strength += g.value;
                            break;
                        }
                    case StatGene.StatType.gestationMaturity:
                        {
                            GestationMaturity += g.value/8;
                            break;
                        }
                    case StatGene.StatType.gestationTime:
                        {
                            GestationTime += g.value;
                            break;
                        }

                }
            }

            maxHp = size;
            hp = maxHp;
            maxEnergy = size;
            energy = maxEnergy / 2;



        }

        void ClampAll()
        {
            Clamp(ref size, 1, 4096);
            Clamp(ref speed, 0, 1024);
            Clamp(ref strength, 0, 1024);
            Clamp(ref GestationMaturity, 0, 128);
            Clamp(ref GestationTime, 0, 1024);

        }

        void Clamp(ref int stat,int min,int max)
        {
            if(stat<min)
            {
                stat = min;
            }
            else if(stat>max)
            {
                stat = max;
            }
        }

        internal string GetPheno()
        {
            return (size.ToString() + GestationTime.ToString() + GestationMaturity.ToString() + speed.ToString() + strength.ToString());
        }
    }
}
