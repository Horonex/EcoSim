using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    static class BrainMaker
    {


        const int OSBEyesNumber = 8;
        const int OSBEarsNumber = 8;
        const int OSBHLayersNumber = 2;
        const int OSBHSize = 30;

        //static public Brain GetBrain(Creature owner, Creature parent)
        //{
        //    Brain b = new Brain(owner);

        //    #region set brain size
        //    int HLayers = owner.stats.hiddenLayers;
        //    b.SetHiddenLayers(HLayers);
        //    #endregion

        //    #region set gene order
        //    HashSet<string> pheroHash = new HashSet<string>();
        //    var geneOrder = new string[pheroHash.Count];

        //    int index = 0;
        //    foreach (var s in pheroHash)
        //    {
        //        geneOrder[index] = s;
        //        index++;
        //    }
        //    b.SetGeneOrder(geneOrder);
        //    #endregion

        //    #region create layers
        //    BrainLayer inputBL;

        //    int VSize = owner.stats.vEye;
        //    //var HGenes = owner.GetGenes(Creature.GeneTypes.hearing);
        //    int HSize = owner.stats.eEars;
        //    var PGenes = owner.GetGenes(Creature.GeneTypes.pheromone);
        //    foreach (var g in PGenes)
        //    {
        //        pheroHash.Add(g.GetGeneticString().Substring(3));
        //    }
        //    int outputSize = 7;
        //    int inputSize = (HSize * 3) + (VSize * 3) + (PGenes.Count * 2);

        //    //inputBL = new BrainLayer(parent.brain.inputs, inputSize, owner.stats.hiddenSize, owner.stats.tweakPercentage,7+ (HSize * 3) + (VSize * 3), geneOrder,parent.brain.PheromoneOrder);

        //    BrainLayer[] hiddenBL=new BrainLayer[HLayers];
        //    if (parent.brain.hiddenLayerCount < HLayers) 
        //    {
        //        for (int i = 0; i < parent.brain.hiddenLayerCount - 1; i++)
        //        {
        //            hiddenBL[i] = new BrainLayer(parent.brain.hiddens[i], owner.stats.hiddenSize, owner.stats.hiddenSize, owner.stats.tweakPercentage);
        //        }
        //        //fill missing -1
        //        for (int i= parent.brain.hiddenLayerCount;i< HLayers - 1; i++)
        //        {
        //            hiddenBL[i] = new BrainLayer(owner.stats.hiddenSize, owner.stats.hiddenSize);
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < HLayers-1; i++)
        //        {
        //            hiddenBL[i] = new BrainLayer(parent.brain.hiddens[i],owner.stats.hiddenSize, owner.stats.hiddenSize, owner.stats.tweakPercentage);
        //        }
        //    }

        //    /////hiddentBL[last]= new BL(parent.brain.hiddens[last]

        //    //int outputSize = 7;
        //    //outputSize += owner.GetGenes(Creature.GeneTypes.outputGeneP).Count;

        //    //hiddenBL[HLayers - 1] = new BrainLayer(owner.stats.hiddenSize, outputSize);

        //    BrainLayer outputBL;

        //    outputSize += owner.GetGenes(Creature.GeneTypes.outputGeneP).Count;

        //    outputBL = new BrainLayer(parent.brain.outputs, outputSize, 1, owner.stats.tweakPercentage);


        //    //b.SetLayers(inputBL,hiddenBL,outputBL);
        //    #endregion


        //    #region set input start index

        //    b.SetInputLength(VSize, HSize);

        //    #endregion

        //    return b;

        //}

        //static public Brain GetBrain(Creature owner)
        //{
        //    Brain b = new Brain(owner);

        //    #region set brain size
        //    int HLayers = owner.stats.hiddenLayers;
        //    b.SetHiddenLayers(HLayers);
        //    #endregion
        //    #region create layers
        //    int inputSize = 2;

        //    //var VGenes = owner.GetGenes(Creature.GeneTypes.vision);
        //    int VSize = owner.stats.vEye;
        //    //var HGenes = owner.GetGenes(Creature.GeneTypes.hearing);
        //    int HSize = owner.stats.eEars;
        //    var PGenes = owner.GetGenes(Creature.GeneTypes.pheromone);
        //    HashSet<string> pheroHash = new HashSet<string>();
        //    foreach (var g in PGenes)
        //    {
        //        pheroHash.Add(g.GetGeneticString().Substring(3));
        //    }
            
        //    inputSize += (HSize * 3) + (VSize * 3) + (PGenes.Count * 2);
            
        //    var hiddens = new BrainLayer[HLayers];

        //    for (int i = 0; i < HLayers-1; i++)
        //    {
        //        hiddens[i] = new BrainLayer(owner.stats.hiddenSize, owner.stats.hiddenSize);
        //    }
        //    int outputSize = 7;
        //    outputSize += owner.GetGenes(Creature.GeneTypes.outputGeneP).Count;

        //    hiddens[HLayers - 1] = new BrainLayer(owner.stats.hiddenSize, outputSize);

        //    b.SetLayers(new BrainLayer(inputSize,owner.stats.hiddenSize),hiddens,new BrainLayer(outputSize,1));
        //    #endregion
        //    #region set gene order
        //    var geneOrder = new string[pheroHash.Count];

        //    int index = 0;
        //    foreach (var s in pheroHash)
        //    {
        //        geneOrder[index] = s;
        //        index++;
        //    }
        //    b.SetGeneOrder(geneOrder);
        //    #endregion
        //    #region set input start index

        //    b.SetInputLength(VSize, HSize);

        //    #endregion

        //    return b;
        //}

        static public Brain OneSizeBrain(Creature owner)
        {

            Brain b = new Brain(owner);

            #region set brain size
            int HLayers = OSBHLayersNumber;
            b.SetHiddenLayers(HLayers);
            #endregion
            #region create layers
            int inputSize = 2;

            //var VGenes = owner.GetGenes(Creature.GeneTypes.vision);
            int VSize = OSBEyesNumber;
            //var HGenes = owner.GetGenes(Creature.GeneTypes.hearing);
            int HSize = OSBEarsNumber;     
           
            inputSize += (HSize * 3) + (VSize * 3);

            var hiddens = new BrainLayer[HLayers];

            for (int i = 0; i < HLayers - 1; i++)
            {
                hiddens[i] = new BrainLayer(OSBHSize,OSBHSize);
            }
            int outputSize = 7;

            hiddens[HLayers - 1] = new BrainLayer(OSBHSize, outputSize);

            b.SetLayers(new BrainLayer(inputSize, OSBHSize), hiddens, new BrainLayer(outputSize, 1));
            #endregion

            b.FillBrain(BrainLayer.NNFillingMode.random);

            return b;
        }

        static public Brain OneSizeBrain(Creature owner, Creature parent)
        {
            Brain b = new Brain();

            #region set brain size
            int HLayers = OSBHLayersNumber;
            b.SetHiddenLayers(HLayers);
            #endregion

            #region create layers
            BrainLayer inputBL;

            inputBL = new BrainLayer(parent.brain.inputs, owner.stats.tweakPercentage);

            BrainLayer[] hiddenBL = new BrainLayer[HLayers];

            for (int i = 0; i < HLayers; i++)
            {
                hiddenBL[i] = new BrainLayer(parent.brain.hiddens[i], owner.stats.tweakPercentage);
            }

            BrainLayer outputBL;

            outputBL = new BrainLayer(parent.brain.outputs, owner.stats.tweakPercentage);

            b.SetLayers(inputBL,hiddenBL,outputBL);
            #endregion

            return b;
        }

        static public Brain GetBrain(Creature owner, Creature parent1, Creature parent2)
        {
            throw new NotImplementedException("2 parent brain creation not supported");
        }
    }
}
