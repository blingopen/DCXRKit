using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DCXR.Examples
{
    public class PlotDemo : MonoBehaviour
    {
        public UPlot plot;

        // Start is called before the first frame update
        void Start()
        {
            plot.SimplePlot(GenerateData(100, false), GenerateData(100, true), "I am a Title", "Iterations", "Value");

        }

        List<float> GenerateData(int numOfData, bool rand)
        {
            List<float> result = new List<float>();

            if (rand)
            {

                for (int i = 0; i < numOfData; i++)
                {
                    result.Add(UnityEngine.Random.Range(0f, 1f));
                }
            }
            else
            {
                for (int i = 0; i < numOfData; i++)
                {
                    result.Add(i);
                }
            }

            return result;
        }
    }
}