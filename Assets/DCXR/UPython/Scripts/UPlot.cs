using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using UnityEngine.Events;

using Debug = UnityEngine.Debug;
using System;

/// <summary>
/// UPlot
/// The help function of the UPython
///
/// MIT license
/// Created by Haikun Huang
/// Date: 2020.6.22
/// </summary>

namespace DCXR
{

    public class UPlot : MonoBehaviour
    {
        public UPython python;


        public void SimplePlot(List<float> xdata, List<float> ydata, string title="", string xLable="", string yLable = "")
        {
            // Python
            List<string> pyText = new List<string>();
            pyText.Add("# importing the required module ");
            pyText.Add("import matplotlib.pyplot as plt ");
            pyText.Add("");
            pyText.Add("# x axis values ");
            pyText.Add(string.Format("x = [{0}] ", GenerateData(xdata))); // embeds the data into Python
            pyText.Add("# corresponding y axis values ");
            pyText.Add(string.Format("y = [{0}] ", GenerateData(ydata))); // embeds the data into Python
            pyText.Add("");
            pyText.Add("# plotting the points ");
            pyText.Add("plt.plot(x, y) ");
            pyText.Add("");
            pyText.Add("# naming the x axis ");
            pyText.Add(string.Format("plt.xlabel('{0}') ", xLable));
            pyText.Add("# naming the y axis ");
            pyText.Add(string.Format("plt.ylabel('{0}') ",yLable));
            pyText.Add("");
            pyText.Add("# giving a title to my graph ");
            pyText.Add(string.Format("plt.title('{0}') ", title));
            pyText.Add("");
            pyText.Add("# function to show the plot ");
            pyText.Add("plt.show() ");
            pyText.Add("");
            pyText.Add("");


            // run
            python.script.AddRange(pyText);
            python.BuildAndRun();
        }


        string GenerateData(List<float> data)
        {
            string result = "";

            result += data[0].ToString();

            for (int i = 1; i < data.Count; i++)
            {
                result += "," + data[i].ToString();
            }

            return result;
        }


    }
}
