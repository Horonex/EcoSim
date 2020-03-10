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

        float[,] inputNodes;
        Dictionary<string, int> Genes;
        float[][,] hiddenNodes;
        float[,] outputNodes;

        float[,] weightIH;
        float[][,] weightHH;
        float[,] weightHO;

        float[,] biasIH;
        float[][,] biasHH;
        float[,] biasHO;



        public Brain(SortedList<Input, int> inputs, Brain parent, float tweakPourcentage=0)
        {           
            weightIH = DuplicateArray(parent.weightIH, tweakPourcentage);
            weightHO = DuplicateArray(parent.weightHO, tweakPourcentage);

            biasIH = DuplicateArray(parent.biasIH, tweakPourcentage);
            biasHO = DuplicateArray(parent.biasHO, tweakPourcentage);

            weightHH = new float[parent.weightHH.Length][,];
            for (int i = 0; i < weightHH.Length; i++)
            {
                weightHH[i] = DuplicateArray(parent.weightHH[i], tweakPourcentage);
            }
                
            biasHH = new float[parent.biasHH.Length][,];
            for (int i = 0; i < biasHH.Length; i++)
            {
                biasHH[i] = DuplicateArray(parent.biasHH[i], tweakPourcentage);
            }

            //90 small
            //9 big
            //1 random
        }

        public Brain(int inputLenght,int hiddenLenght,int outputLenght,int hiddenLayers=1,NNFillingMode FillingMode=NNFillingMode.random)
        {
            InitBrainSize(inputLenght, hiddenLenght, outputLenght, hiddenLayers);
            FillBrain(FillingMode);
        }

        public Brain(SortedList<Input,int> inputs, Brain parent)
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
            inputNodes = new float[inputLenght, 1];
            //set length of hiddens layers nodes
            hiddenNodes = new float[hiddenLayers][,];
            for (int i = 0; i < hiddenNodes.Length; i++)
            {
                hiddenNodes[i] = new float[hiddenLenght, 1];
            }
            //set length of output nodes
            outputNodes = new float[outputLenght, 1];

            //set length weiht of input to hidden and hidden to outupt
            weightIH = new float[inputLenght, hiddenLenght];
            weightHO = new float[hiddenLenght, outputLenght];

            //set length biases of input to hidden and hidden to outupt
            biasIH = new float[hiddenLenght, 1];
            biasHO = new float[outputLenght, 1];

            //if more than one hidden layer 
            if (hiddenLayers > 1)
            {
                //set length weihts of hidden to hidden
                weightHH = new float[hiddenLayers - 1][,];
                for (int i = 0; i < weightHH.Length; i++)
                {
                    weightHH[i] = new float[hiddenLenght, 1];
                }

                //set length biases of hidden to hidden
                biasHH = new float[hiddenLayers - 1][,];
                for (int i = 0; i < biasHH.Length; i++)
                {
                    biasHH[i] = new float[hiddenLenght, 1];
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

        private void FillArray(float[,] array)
        {
            for(int i=0; i<array.GetLength(0);i++)
            {
                for(int j=0;j<array.GetLength(1);j++)
                {
                    array[i, j] = UnityEngine.Random.Range(-0.5f, 0.5f);
                }
            }
        }

        private float[,] DuplicateArray(float[,] item)
        {
            var output = new float[item.GetLength(0), item.GetLength(1)];
            for (int i = 0; i < item.GetLength(0); i++)
            {
                for (int j = 0 ; j < item.GetLength(0); j++)
                {
                    output[i, j] = item[i, j];
                }
            }
            return output;
        }
        private float[,] DuplicateArray(float[,] item, float tweakPourcentage)
        {
            var output = new float[item.GetLength(0), item.GetLength(1)];
            for (int i = 0; i < item.GetLength(0); i++)
            {
                for (int j = 0; j < item.GetLength(0); j++)
                {
                    float rand = UnityEngine.Random.Range(-(tweakPourcentage / 100), tweakPourcentage / 100);
                    output[i, j] = item[i, j]+rand;
                }
            }
            return output;
        }

        public void GatherInfo(Creature senser)
        {

        }

        public void Evaluate()
        {

        }
    }
}
