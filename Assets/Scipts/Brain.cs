using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class Brain
    {
        int hiddenLayers;

        double[,] inputNodes;
        double[][,] hiddenNodes;
        double[,] outputNodes;

        double[,] weightIH;
        double[][,] weightHH;
        double[,] weightHO;

        double[,] biasIH;
        double[][,] biasHH;
        double[,] biasHO;



        public Brain(Brain parent,float tweakPourcentage)
        {

        }
        public Brain(int inputLenght,int hiddenLenght,int outputLenght,int hiddenLayers=1)
        {
            this.hiddenLayers = hiddenLayers;
            inputNodes = new double[inputLenght, 1];
            hiddenNodes = new double[hiddenLayers][,];
            for (int i = 0; i < hiddenNodes.Length; i++)
            {
                hiddenNodes[i] = new double[hiddenLenght, 1];
            }
            outputNodes = new double[outputLenght, 1];

            weightIH = new double[inputLenght, hiddenLenght];
            weightHO = new double[hiddenLenght, outputLenght];

            biasIH = new double[hiddenLenght, 1];
            biasHO = new double[outputLenght, 1];

            hiddenLayers--;
            if(hiddenLayers>0)
            {
                weightHH = new double[hiddenLayers][,];
                for (int i = 0; i < weightHH.Length; i++)
                {
                    weightHH[i] = new double[hiddenLenght, 1];
                }

                biasHH = new double[hiddenLayers][,];
                for (int i = 0; i < biasHH.Length; i++)
                {
                    biasHH[i] = new double[hiddenLenght, 1];
                }
            }


        }





    }
}
