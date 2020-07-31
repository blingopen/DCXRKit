using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// Helper Tool
///
/// MIT license
/// Created by Haikun Huang
/// Date: 2020.5.26
/// </summary>
namespace DCXR.Examples
{

    public class Parser : MonoBehaviour
    {
        public InputField inputText;
        public InputField outputText;

        public void Parse()
        {
            // string
            //outputText.text = "string pyText = \n";

            //string[] data = inputText.text.Split('\n');

            //foreach(var d in data)
            //{
            //    outputText.text += string.Format("\"{0}\\n\"+\n", d);
            //}
            //outputText.text += "\"\";";


            // list
            outputText.text = "List<string> pyText = new List<string>();\n";

            string[] data = inputText.text.Split('\n');

            foreach (var d in data)
            {
                outputText.text += string.Format("pyText.Add(\"{0}\");\n", d);
            }


        }

    }
}
