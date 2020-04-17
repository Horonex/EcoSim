using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;



namespace Assets.Scipts
{
    public class Brain
    {
        const float speedMod = 0.01f;


        public Creature owner;

        public int hiddenLayerCount;

        public BrainLayer inputs;
        //Dictionary<string, int> Genes;
        //List<BrainGene> s;

        public BrainLayer[] hiddens;

        public BrainLayer outputs;

        public string[] PheromoneOrder;

        #region internal
        enum inputType {hp,energy,prengnent,vision,earing,pheromone };
        enum outputType {move,turn,attack,eat,reproduce,pheromone,dialogue,scream };
        #endregion
        Dictionary<inputType,int> IStartIndexes=new Dictionary<inputType, int>
        {{inputType.hp,0 },{inputType.energy,1},{inputType.prengnent,2 },{inputType.vision,3 },{inputType.earing,27 } };
        Dictionary<outputType, int> OStartIndexes=new Dictionary<outputType, int>
        { { outputType.move, 0 },{ outputType.turn,1 },{ outputType.attack, 2 },{ outputType.eat, 3 },{ outputType.reproduce, 4 },{outputType.dialogue ,5},{outputType.scream ,6},{outputType.pheromone ,7}};

        delegate float gather();
        delegate float[] gatherM();


        #region internal
        public Vector2 movement;
        public bool willAttack;
        public bool willEat;
        public bool willReproduce;
        public List<string> signals;
        public string dialogue;
        public float screamForce;
        #endregion
        string[] pheromones;


        public Brain(Creature owner)
        {
            this.owner = owner;
        }

        public Brain()
        {

        }

        #region brain creation
        public void SetHiddenLayers(int number)
        {
            hiddenLayerCount = number;
        }

        public void SetLayers(BrainLayer input,BrainLayer[] hidden,BrainLayer output)
        {
            inputs = input;
            hiddens = hidden;
            outputs = output;
        }

        public void SetGeneOrder(string[] order)
        {
            PheromoneOrder = order;
        }

        public void SetInputLength(int visionLength,int hearingLength)
        {
            IStartIndexes.Add(inputType.earing, 3 + visionLength);
            IStartIndexes.Add(inputType.pheromone, 3 + visionLength+hearingLength);
        }
        #endregion

        //public Brain(SortedList<Input, int> inputGenes, Brain parent, float tweakPourcentage=0)
        //{
        //    inputs = new BrainLayer(parent.inputs,tweakPourcentage);
        //    outputs = new BrainLayer(parent.outputs, tweakPourcentage);          

        //    hiddens = new BrainLayer[parent.hiddens.Length];
        //    for (int i = 0; i < hiddens.Length; i++)
        //    {
        //        hiddens[i] = new BrainLayer(parent.hiddens[i], tweakPourcentage);
        //    }
        //}

        //public Brain(int inputLenght,int hiddenLenght,int outputLenght,int hiddenLayers=1,BrainLayer.NNFillingMode FillingMode= BrainLayer.NNFillingMode.random)
        //{
        //    InitBrainSize(inputLenght, hiddenLenght, outputLenght, hiddenLayers);
        //    FillBrain(FillingMode);
        //}

        //public Brain(SortedList<Input,int> inputGenes, Brain parent)
        //{
        //    //init start index
        //    int count = 0;
        //    foreach (var i in IStartIndexes.Values)
        //    {
        //        count += i;
        //    }
        //    inputs = new BrainLayer(count);

        //    //hp
        //    //energy
        //    //pregnant
        //    //vision
        //    //earing
        //    //pheromones
        //    //

        //}

        //private void InitBrainSize(int inputLenght, int hiddenLenght, int outputLenght, int hiddenLayers)
        //{
        //    //set number of hidden layers
        //    this.hiddenLayerCount = hiddenLayers;
        //    //set length of input nodes
        //    inputs = new BrainLayer(inputLenght);
        //    //set length of hiddens layers nodes
        //    hiddens = new BrainLayer[hiddenLayers];
        //    for (int i = 0; i < hiddens.Length; i++)
        //    {
        //        hiddens[i] = new BrainLayer(hiddenLenght);
        //    }
        //    //set length of output nodes
        //    outputs = new BrainLayer(outputLenght);

        //}

        public void FillBrain(BrainLayer.NNFillingMode FillingMode)
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
            gatherI(IStartIndexes[inputType.hp], 1, senser.GetHp);
            gatherI(IStartIndexes[inputType.energy], 1, senser.GetEnergy);
            gatherI(IStartIndexes[inputType.prengnent], 1, senser.Pregnant);
            gatherI(IStartIndexes[inputType.vision],8*3 , senser.Look);
            gatherI(IStartIndexes[inputType.earing],8*3 , senser.Hear);
            //gatherI(IStartIndexes[inputType.pheromone], inputs.GetLength()- startIndexes[inputStsrtIndex.pheromone], senser.Look);
        }

        public void Evaluate()
        {
            FeedForward();
            SetActions();
        }

        private void gatherI(int startIndex,int length,gather func)
        {
            for (int i = 0; i < length; i++)
            {
                inputs.SetNode(startIndex + i, func());
            }
        }

        private void gatherI(int startIndex, int length, gatherM func)
        {
            for (int i = 0; i < length; i++)
            {
                var info = func();
                for (int j = 0; j < info.Length; j++, i++)
                {
                    inputs.SetNode(startIndex + i, info[j]);
                }
            }
        }
   

        void FeedForward()
        {
            hiddens[0].FeedForward(inputs);
            if(hiddens.Length>1)
            {
                for (int i = 1; i < hiddens.Length; i++)
                {
                    hiddens[i].FeedForward(hiddens[i - 1]);
                }
            }
            outputs.FeedForward(hiddens[hiddens.Length - 1]);
        }

        private float SigmoidClamp(float x,float maxValue,float minValue)
        {

            var sigm = (1.0f / (1.0f + Mathf.Exp(-x)));

            return (sigm * (maxValue - minValue)) + minValue;
        }
        private float LinearClamp(float x,float maxValue,float minValue)
        {
            //x/= x maxvalue
            return (x * (maxValue - minValue)) + minValue;
        }

        private float GetOutput(outputType type)
        {
            return outputs.GetNode(OStartIndexes[type]);
        }

        private void SetActions()
        {
            ResetActions();
            //set movement
            var a = Mathf.Atan2(owner.orientation.y, owner.orientation.x);
            a += SigmoidClamp(GetOutput(outputType.turn),Mathf.PI*-1, Mathf.PI);

            var distance = GetOutput(outputType.move);
            movement = new Vector2(Mathf.Cos(a) * distance*speedMod, Mathf.Sin(a) * distance*speedMod);

            //set attacking this turn
            if (outputs.GetNode(OStartIndexes[outputType.attack])>0)
            {
                willAttack = true;
            }
            //set attacking this 
            if (outputs.GetNode(OStartIndexes[outputType.eat]) > 0)
            {
                willEat = true;
            }
            //set attacking this turn
            if (outputs.GetNode(OStartIndexes[outputType.reproduce])>0)
            {
                willReproduce = true;
            }

            for (int i = 0; i < OStartIndexes[outputType.dialogue]- OStartIndexes[outputType.pheromone]; i++)
            {
                if (outputs.GetNode(i + OStartIndexes[outputType.pheromone]) > 0) 
                {
                    signals.Add(pheromones[i]);
                }
            }
            if(outputs.GetNode(OStartIndexes[outputType.scream]) > 0)
            {
                //owner.Scream()
            }

        }
        private void ResetActions()
        {
            movement=new Vector2();
            willAttack=false;
            willEat = false;
            willReproduce = false;
            signals=new List<string>();
            dialogue="";
            screamForce=0;
        }
    }
}
