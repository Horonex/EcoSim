using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class Stat:Gene
    {
        public const int GENELENGTH = 8;
        const int TYPELENGTH = 2;
        public static readonly int[,] CLAMPVALUES = new int[16, 2] {
            { 0, 2 },
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
            { 0, 0 } };

        public enum StatType { sex,size,speed,gestationTime,gestationMaturity,strength}

        StatType type;
        int value;
   
        public Stat(string geneticString)
        {
            this.geneticString = geneticString;
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

        public override void Express(Creature expressedOn)
        {
           //expressedOn.
        }

    }
}
