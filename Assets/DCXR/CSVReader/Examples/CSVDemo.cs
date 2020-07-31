using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DCXR.Examples
{
    public class CSVDemo : MonoBehaviour
    {

        CSVReader reader;
        // Start is called before the first frame update
        void Start()
        {
            reader = new CSVReader();
            reader.Load(Application.dataPath + "/DCXR/CSVReader/Examples/Demo.CSV");

            while(!reader.EOF())
            {
                Debug.Log("------");
                Debug.Log("name: " + reader.Get_String("name"));
                Debug.Log("Worker1: " + reader.Get_String("Worker1"));
                Debug.Log("Worker2: " + reader.Get_String("Worker2"));
                Debug.Log("Code is: " + reader.Get_String_Array("Code"));

                reader.Next(); // move the cursor
            }

        }


    }
}


