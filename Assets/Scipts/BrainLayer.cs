using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scipts
{
    public class BrainLayer
    {

        public enum NNFillingMode { random, center, empty };

        float[,] nodes;
        float[,] weights;//form this layer to the next
        float[,] biases;


        public BrainLayer(BrainLayer parent, float tweakPercentage)
        {
            nodes = new float[parent.nodes.GetLength(0), 1];
            weights = DuplicateArray(parent.weights, tweakPercentage);
            biases = DuplicateArray(parent.biases, tweakPercentage);
        }

        public BrainLayer(BrainLayer parent,int size,int nextSize, float tweakPercentage)
        {
            nodes = new float[size, 1];
            weights = new float[size, nextSize];
            biases = new float[size, 1];

            weights = DuplicateArray(parent.weights, size, nextSize, tweakPercentage);
            biases = DuplicateArray(parent.biases, size, 1, tweakPercentage);



            //bool xPSmaller = false;
            //bool yPSmaller = false;

            //if(parent.weights.GetLength(0)< weights.GetLength(0))
            //{
            //    xPSmaller = true;
            //}
            //if(parent.weights.GetLength(1)<weights.GetLength(0))
            //{
            //    yPSmaller = true;
            //}





            //weights = DuplicateArray(parent.weights,tweakPercentage);
            //biases = DuplicateArray(parent.biases, tweakPercentage);
        }

        public BrainLayer(BrainLayer parent, int size, int nextSize, float tweakPercentage,int parralleStart,int parentStart,string[] ownerOrder, string[] parentOrder)
        {
            nodes = new float[size, 1];
            weights = new float[size, nextSize];
            biases = new float[size, 1];

            weights = DuplicateArray(parent.weights, size, nextSize, tweakPercentage);
            biases = DuplicateArray(parent.biases, size, 1, tweakPercentage);


            //foreach order in child
            for (int i = 0; i < ownerOrder.Length; i++)
            {
                //find corresponding
                for (int j = 0; j < parentOrder.Length; j++)
                {
                    //if found 
                    if(ownerOrder[i]==parentOrder[j])
                    {
                        parentOrder[j] = "";
                        if(! (i==j))
                        {
                            //switch row
                            var temp = new string[nextSize];
                            for(int k=0;k<temp.Length&&k<parent.biases.GetLength(0);k++)
                            {
                                Swap(ref weights[i + parralleStart, k], ref parent.weights[i + parentStart, k]);
                            }
                            Swap(ref biases[i + parralleStart, 0], ref parent.weights[i + parentStart, 0]);
                        }
                    }
                }
            }

            //weights = DuplicateArray(parent.weights, tweakPercentage);
            //biases = DuplicateArray(parent.biases, tweakPercentage);
        }

        void Swap(ref float x,ref float y)
        {
            var temp = x;
            x = y;
            y = temp;
        }


        public BrainLayer(int size,int nextSize)
        {
            nodes = new float[size, 1];
            weights = new float[size, nextSize];
            biases = new float[size, 1];
        }

        public int GetLength()
        {
            return nodes.GetLength(0);
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

        public void SetNode(int index,float value)
        {
            if(index>=nodes.GetLength(0)||index<0)
            {
                return;
            }
            nodes[index, 0] = value;
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

        #region matrix manipulation
        private float[,] DuplicateArray(float[,] item, float tweakPourcentage)
        {
            var output = new float[item.GetLength(0), item.GetLength(1)];
            for (int i = 0; i < item.GetLength(0); i++)
            {
                for (int j = 0; j < item.GetLength(1); j++)
                {
                    float rand = UnityEngine.Random.Range(-(tweakPourcentage / 100), tweakPourcentage / 100);
                    output[i, j] = item[i, j] + rand;
                }
            }
            return output;
        }

        private float[,] DuplicateArray(float[,] item,int x,int y, float tweakPourcentage)
        {
            var output = new float[x, y];
            
            for (int i = 0; i < item.GetLength(0) && i < x; i++) 
            {
                
                for (int j = 0; j < item.GetLength(0) && j < y; j++)
                {
                    float rand = UnityEngine.Random.Range(-(tweakPourcentage / 100), tweakPourcentage / 100);
                    output[i, j] = item[i, j] + rand;
                }
            }
            //for (int i = 0; i < x; i++) 
            //{
            //    int j = 0;
            //    if(i< item.GetLength(0))
            //    {
            //        j = item.GetLength(0);
            //    }
            //    for (; j < y; j++) 
            //    {
            //        output[i, j] =
            //    }
            //}
            return output;
        }


        private float[,] DotProduct(float[,] m1, float[,] m2)
        {
            int r1 = m1.GetLength(0);
            int c1 = m1.GetLength(1);

            int c2 = m2.GetLength(1);

            float[,] output = new float[r1, c2];

            for (int i = 0; i < r1; i++)
            {
                for (int j = 0; j < c2; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < c1; k++)
                    {
                        sum += m1[i, k] * m2[k, j];
                    }
                    output[j, j] = sum;
                }
            }
            return output;
        }

        private float[,] Transpose(float[,] matrix)
        {
            var output = new float[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    output[j, i] = matrix[i, j];
                }
            }
            return output;
        }

        private float[,] Add(float[,] m1, float[,] m2)
        {
            var output = new float[m1.GetLength(0), m1.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m1.GetLength(1); j++)
                {
                    output[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return output;
        }

        private float Sigmoid(float x)
        {
            return (1.0f / (1.0f + Mathf.Exp(-x)));
        }

        private float[,] Sigmoid(float[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = Sigmoid(matrix[i, j]);
                }
            }
            return matrix;
        }
        #endregion

        public float GetNode(int index)
        {
            if (index >= nodes.GetLength(0) || index < 0) throw new Exception("index out of nodes range");
            return nodes[index,0]+biases[index,0];
        }
        public float[] GetNode(int startIndex,int count)
        {
            if (startIndex+count >= nodes.GetLength(0) || startIndex < 0) throw new Exception("index out of nodes range");
            var output = new float[count];
            for (int i = 0; i < count; i++)
            {
                output[i] = nodes[startIndex + i,0] + biases[startIndex + i, 0];
            }
            return output;
        }

        public void FeedForward(BrainLayer previous)
        {
            nodes = Sigmoid(Add(DotProduct(Transpose(previous.weights), previous.nodes), previous.biases));
            

        }

    }
}
