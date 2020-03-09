using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class Brain
    {
        public enum NNFillingMode {random, center, empty}

        int hiddenLayers;

        double[,] inputNodes;
        string[] inputTypes;
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
            //90 small
            //9 big
            //1 random
        }

        public Brain(int inputLenght,int hiddenLenght,int outputLenght,int hiddenLayers=1,NNFillingMode FillingMode=NNFillingMode.random)
        {
            InitBrainSize(inputLenght, hiddenLenght, outputLenght, hiddenLayers);
            FillBrain(FillingMode);
        }


        public Brain(SortedList<Input,int> inputs)
        {
            

            //hp
            //energy
            //vision
            //earing
            //pheromones
            //



        }

        private void InitBrainSize(int inputLenght, int hiddenLenght, int outputLenght, int hiddenLayers)
        {
            //set number of hidden layers
            this.hiddenLayers = hiddenLayers;
            //set length of input nodes
            inputNodes = new double[inputLenght, 1];
            //set length of hiddens layers nodes
            hiddenNodes = new double[hiddenLayers][,];
            for (int i = 0; i < hiddenNodes.Length; i++)
            {
                hiddenNodes[i] = new double[hiddenLenght, 1];
            }
            //set length of output nodes
            outputNodes = new double[outputLenght, 1];

            //set length weiht of input to hidden and hidden to outupt
            weightIH = new double[inputLenght, hiddenLenght];
            weightHO = new double[hiddenLenght, outputLenght];

            //set length biases of input to hidden and hidden to outupt
            biasIH = new double[hiddenLenght, 1];
            biasHO = new double[outputLenght, 1];

            //if more than one hidden layer 
            if (hiddenLayers > 1)
            {
                //set length weihts of hidden to hidden
                weightHH = new double[hiddenLayers - 1][,];
                for (int i = 0; i < weightHH.Length; i++)
                {
                    weightHH[i] = new double[hiddenLenght, 1];
                }

                //set length biases of hidden to hidden
                biasHH = new double[hiddenLayers - 1][,];
                for (int i = 0; i < biasHH.Length; i++)
                {
                    biasHH[i] = new double[hiddenLenght, 1];
                }
            }
        }

        private void FillBrain(NNFillingMode FillingMode)
        {
            switch(FillingMode)
            {
                case NNFillingMode.empty:
                    {
                        break;
                    }
                case NNFillingMode.center:
                    {
                        break;
                    }
                case NNFillingMode.random:
                    {                        
                        FillArray(weightIH);
                        FillArray(biasIH);
                        FillArray(weightHO);
                        FillArray(biasHO);
                        foreach(var a in weightHH)
                        {
                            FillArray(a);
                        }
                        foreach (var a in biasHH)
                        {
                            FillArray(a);
                        }
                        break;
                    }
            }
        }

        private void FillArray(double[,] array)
        {
            for(int i=0; i<array.GetLength(0);i++)
            {
                for(int j=0;j<array.GetLength(1);j++)
                {
                    array[i, j] = UnityEngine.Random.Range(-0.5f, 0.5f);
                }
            }
        }

    }
}
