using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    public class Stats
    {


        #region stats
        public float size, hp, maxHp, energy, maxEnergy;
        public int GestationTime;
        public int GestationMaturity;
        public int speed;
        public int strength;
        #endregion
        #region inputs
        #region vision
        public int vDistance;
        public int vAngle;
        public int vOrientation;
        public int vNightVision;
        public int vEye;
        #endregion
        #region earing
        public int eSensitivity;
        public int eEars;
        #endregion
        #region pheromone
        public HashSet<string> phero;
        #endregion
        #endregion
        #region protein 

        public List<ProteinBD> pBreakedown;
        public List<ProteinS> pSynthesis;
        internal List<ProteinBD> PBreakedown { get => pBreakedown; set => pBreakedown = value; }
        internal List<ProteinS> PSynthesis { get => pSynthesis; set => pSynthesis = value; }

        #endregion
        #region brain
        public int tweakPercentage;
        public int hiddenLayers;
        public int hiddenSize;
        #endregion


        #region  energy use mod
        /// <summary>
        /// multiply the strength
        /// </summary>
        public const float AttackEnergyCostMod = 1;
        /// <summary>
        /// multiply the distance travel and the size
        /// </summary>
        public const float MoveEnergyCostMod = 1;

        public const float NNEnergyCostMod = 1;

        public const float EatingEnergyCostMod = 1;

        public const float VisionEnergyCostMod = 1;

        public const float HearingEnergyCostMod = 1;

        public const float ReproductionEnergyCostMod = 1;

        public const float ScreamingEnergyCostMod = 1;



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

        public Stats(List<Gene> genes,Creature owner)
        {
            owner.stats = this;
            Init();
            foreach (var g in genes)
            {
                g.Express(owner);
            }


            ClampAll();

            maxHp = size;
            hp = maxHp;
            maxEnergy = size*100;
            energy = maxEnergy / 2;


        }

        void Init()
        {
            #region stats
            size = 0;
            hp = 0;
            maxHp = 0;
            energy = 0;
            maxEnergy = 0;
            GestationTime = 0;
            GestationMaturity = 0;
            speed = 0;
            strength = 0;
            #endregion
            #region inputs
            #region vision
            vDistance = 0;
            vAngle = 0;
            vOrientation = 0;
            vNightVision = 0;
            vEye = 0;
            #endregion
            #region earing
            eSensitivity = 0;
            eEars = 0;
            #endregion
            #region pheromone

            #endregion
            #endregion
            #region protein 

            pBreakedown = new List<ProteinBD>();
            pSynthesis = new List<ProteinS>();


            #endregion
            #region brain
            tweakPercentage = 90;
            hiddenLayers = 0;
            hiddenSize = 0;
            #endregion
        }

        void ClampAll()
        {
            //Clamp(ref size, 1, 4096);
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
            //Clamp(ref ePrecition);
            Clamp(ref eEars,0,64);

            Clamp(ref tweakPercentage,0,256);
            Clamp(ref hiddenLayers,16);

            Clamp(ref size, 1);
            Clamp(ref maxHp, 1, 8192);
            Clamp(ref maxEnergy, 100, 65536);

            //public int hiddenSize;


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
        void Clamp(ref float stat, int min = 0, int max = 1024)
        {
            if (stat < min)
            {
                stat = min;
            }
            else if (stat > max)
            {
                stat = max;
            }
        }

        internal string GetPheno()
        {
            return (size.ToString() + GestationTime.ToString() + GestationMaturity.ToString() + speed.ToString() + strength.ToString());
        }

        public string[] getSomeStats()
        {
            List<string> output = new List<string>();

            output.Add("speed " + speed.ToString());
            output.Add("size " + size.ToString());
            //output.Add("speed " + speed.ToString());

            return output.ToArray();

        }
    }
}
