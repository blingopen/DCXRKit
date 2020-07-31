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
namespace DCXR.Examples
{

    public class UseCase : MonoBehaviour
    {

        public UPython python;


        // Start is called before the first frame update
        void Start()
        {
            // script
            List<string> pyText = new List<string>();
            pyText.Add("from sklearn import datasets");
            pyText.Add("digits=datasets.load_digits()");
            pyText.Add("print(digits.data.shape)");
            pyText.Add("import matplotlib.pyplot as plt");
            pyText.Add("plt.gray()");
            pyText.Add("plt.matshow(digits.images[0])");
            pyText.Add("plt.show()");
            pyText.Add("");
            pyText.Add("digits=datasets.load_digits()");
            pyText.Add("digits.keys()");
            pyText.Add("n_samples,n_features=digits.data.shape");
            pyText.Add("print((n_samples,n_features))");
            pyText.Add("");
            pyText.Add("print(digits.data.shape)");
            pyText.Add("print(digits.images.shape)");
            pyText.Add("");
            pyText.Add("import numpy as np");
            pyText.Add("print(np.all(digits.images.reshape((1797,64))==digits.data))");
            pyText.Add("");
            pyText.Add("fig=plt.figure(figsize=(6,6))");
            pyText.Add("fig.subplots_adjust(left=0,right=1,bottom=0,top=1,hspace=0.05,wspace=0.05)");
            pyText.Add("");
            pyText.Add("");
            pyText.Add("for i in range(64):");
            pyText.Add("    ax=fig.add_subplot(8,8,i+1,xticks=[],yticks=[])");
            pyText.Add("    ax.imshow(digits.images[i],cmap=plt.cm.binary,interpolation='nearest')");
            pyText.Add("    ");
            pyText.Add("    ");
            pyText.Add("    ax.text(0,7,str(digits.target[i]))");
            pyText.Add("plt.show()");

            // run
            python.script.AddRange(pyText);
            python.BuildAndRun();
        }
    }
}
