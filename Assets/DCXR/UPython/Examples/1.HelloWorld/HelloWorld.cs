using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example
///
/// MIT license
/// Created by Haikun Huang
/// Date: 2020.5.26
/// </summary>
/// 
namespace DCXR.Examples
{

    public class HelloWorld : MonoBehaviour
    {

        public UPython python;

        public string say = "Hello World!";

        void Start()
        {
            python.script.Add("import sys");
            python.script.Add(string.Format("say = '{0}'", say));
            python.script.Add("print(say)");
            python.script.Add("print('My first name is ' + sys.argv[1])");
            python.script.Add("print('My last  name is ' + sys.argv[2])");
            python.script.Add(
                "b=1\n" +
                "c=2\n" +
                "d=b+c\n" +
                "print('@d='+str(d))" +
                "");

            python.BuildAndRun("Haikun Huang", Done); // Run the python.
        }

        void Done()
        {
            Debug.Log("The variable 'd' has a value " + python.variables["d"]);
            Debug.Log("The Hello World program is done.");
        }

    }
}
