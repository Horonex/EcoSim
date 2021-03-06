﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    public class StatGene:Gene
    {
        new static int geneLength = 8;
        const int TYPELENGTH = 2;
        public static readonly int[,] CLAMPVALUES = new int[16, 2] {
            { 0, 1023 },
            { 0, 1023 },
            { 0, 4095 },
            { 0, 2565 },
            { 0, 1023 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 } };

        public enum StatType { size,speed,gestationTime,gestationMaturity,strength}

        public StatType type;
        public int value;
   
        public StatGene(string gString)
        {
            geneticString = Pad(gString, geneLength - gString.Length).Substring(0, geneLength);
            Prefix = gString.Substring(0, 2);
            SetType();
            SetValue();
        }

        void SetType()
        {
            type = (StatType) GeneticCode.ToInt(geneticString.Substring(0, TYPELENGTH));
        }
        void SetValue()
        {
            value = GeneticCode.ToInt(geneticString.Substring(TYPELENGTH+2));
        }

        public override string ToString()
        {
            return "STAT " + type + ": " + value.ToString();
        }
        public override int GetLength()
        {
            return geneLength;
        }

        public override void Express(Creature owner)
        {

            int value = GeneticCode.ToInt(geneticString.Substring(4));
            switch (geneticString.Substring(2, 2))
            {
                default:
                    {
                        break;
                    }
                case "HH":
                    {
                        
                        break;
                    }
                case "HV":
                    {
                        owner.stats.size += value;
                        break;
                    }
                case "HO":
                    {
                        owner.stats.speed += value;
                        break;
                    }
                case "HX":
                    {
                        owner.stats.GestationTime += value;
                        break;
                    }
                case "VH":
                    {
                        owner.stats.GestationMaturity += value;
                        break;
                    }
                case "VV":
                    {
                        owner.stats.strength += value;
                        break;
                    }
                case "VO":
                    {
                        //owner.stats
                        break;
                    }
                case "VX":
                    {
                        //owner.stats
                        break;
                    }
                case "OH":
                    {
                        //owner.stats
                        break;
                    }
                case "OV":
                    {
                        //owner.stats
                        break;
                    }
                case "OO":
                    {
                        //owner.stats
                        break;
                    }
                case "OX":
                    {
                        //owner.stats
                        break;
                    }
                case "XH":
                    {
                        //owner.stats
                        break;
                    }
                case "XV":
                    {
                        //owner.stats
                        break;
                    }
                case "XO":
                    {
                        //owner.stats
                        break;
                    }
                case "XX":
                    {
                        //owner.stats
                        break;
                    }
            }
        }
    }
}
