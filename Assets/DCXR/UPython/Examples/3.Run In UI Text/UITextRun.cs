using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Example
///
/// MIT license
/// Created by Haikun Huang
/// Date: 2020.5.26
/// </summary>
namespace DCXR.Examples
{
    public class UITextRun : MonoBehaviour
    {

        public UPython python;

        public InputField code;

        public void Run()
        {
            python.NewEnvironment();
            python.script.Add(code.text);
            python.BuildAndRun("", null);

        }



    }
}
