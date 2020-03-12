using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scipts
{
    class BrainLayer
    {

        public enum NNFillingMode { random, center, empty };

        float[,] nodes;
        float[,] weights;
        float[,] biases;

        public BrainLayer(BrainLayer parent, float tweakPercentage)
        {
            weights = DuplicateArray(parent.weights,tweakPercentage);
            biases = DuplicateArray(parent.biases, tweakPercentage);
        }

        public BrainLayer(int size)
        {
            nodes = new float[size, 1];
            weights = new float[size, 1];
            biases = new float[size, 1];
        }

        public void Fill(NNFillingMode FillingMode)
        {
            switch (FillingMode)
            {
                case BrainLayer.NNFillingMode.empty:
                    {
                        break;
                    }
                case BrainLayer.NNFillingMode.center:
                    {
                        break;
                    }
                case BrainLayer.NNFillingMode.random:
                    {
                        FillArray(weights);
                        FillArray(biases);
                        break;
                    }
            }
        }

        private void FillArray(float[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = UnityEngine.Random.Range(-0.5f, 0.5f);
                }
            }
        }


        private float[,] DuplicateArray(float[,] item, float tweakPourcentage)
        {
            var output = new float[item.GetLength(0), item.GetLength(1)];
            for (int i = 0; i < item.GetLength(0); i++)
            {
                for (int j = 0; j < item.GetLength(0); j++)
                {
                    float rand = UnityEngine.Random.Range(-(tweakPourcentage / 100), tweakPourcentage / 100);
                    output[i, j] = item[i, j] + rand;
                }
            }
            return output;
        }
    }
}
