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
        int hiddenLayers;

        BrainLayer inputs;
        Dictionary<string, int> Genes;

        BrainLayer[] hiddens;

        BrainLayer outputs;




        public Brain(SortedList<Input, int> inputGenes, Brain parent, float tweakPourcentage=0)
        {
            inputs = new BrainLayer(parent.inputs,tweakPourcentage);
            outputs = new BrainLayer(parent.outputs, tweakPourcentage);          

            hiddens = new BrainLayer[parent.hiddens.Length];
            for (int i = 0; i < hiddens.Length; i++)
            {
                hiddens[i] = new BrainLayer(parent.hiddens[i], tweakPourcentage);
            }
        }

        public Brain(int inputLenght,int hiddenLenght,int outputLenght,int hiddenLayers=1,BrainLayer.NNFillingMode FillingMode= BrainLayer.NNFillingMode.random)
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
            inputs = new BrainLayer(inputLenght);
            //set length of hiddens layers nodes
            hiddens = new BrainLayer[hiddenLayers];
            for (int i = 0; i < hiddens.Length; i++)
            {
                hiddens[i] = new BrainLayer(hiddenLenght);
            }
            //set length of output nodes
            outputs = new BrainLayer(outputLenght);

        }

        private void FillBrain(BrainLayer.NNFillingMode FillingMode)
        {
            inputs.Fill(FillingMode);

            foreach (var l in hiddens)
            {
                l.Fill(FillingMode);
            }

            outputs.Fill(FillingMode);
        }

        public void GatherInfo(Creature senser)
        {

        }

        public void Evaluate()
        {

        }
    }
}
