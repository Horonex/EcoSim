using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    public class Stats
    {


        #region stats
        public int size, hp, maxHp, energy, maxEnergy;
        int GestationTime;
        int GestationMaturity;
        int speed;
        int strength;
        #endregion
        #region inputs
        #region vision
        int vDistance;
        int vAngle;
        int vOrientation;
        int vNightVision;
        int vEye;
        #endregion
        #region earing
        int eSensitivity;
        int ePrecition;
        int eEars;
        #endregion
        #region pheromone
        HashSet<string> phero;
        #endregion
        #endregion
        #region protein 

        List<ProteinBD> pBreakedown;
        List<ProteinS> pSynthesis;
        internal List<ProteinBD> PBreakedown { get => pBreakedown; set => pBreakedown = value; }
        internal List<ProteinS> PSynthesis { get => pSynthesis; set => pSynthesis = value; }

        #endregion
        #region brain
        int tweakPercentage;
        int hiddenLayers;
        #endregion




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
            Clamp(ref speed);
            Clamp(ref strength);
            Clamp(ref GestationMaturity, 0, 128);
            Clamp(ref GestationTime);

            Clamp(ref vDistance);
            Clamp(ref vAngle);
            Clamp(ref vOrientation);
            Clamp(ref vNightVision);
            Clamp(ref vEye,0,64);

            Clamp(ref eSensitivity);
            Clamp(ref ePrecition);
            Clamp(ref eEars,0,64);

            Clamp(ref tweakPercentage,0,256);
            Clamp(ref hiddenLayers,16);

        }

        void Clamp(ref int stat, int min = 0, int max = 1024)
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
