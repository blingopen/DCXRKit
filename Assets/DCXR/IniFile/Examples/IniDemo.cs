using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DCXR.Examples
{
    public class IniDemo : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            CreateFile();
            ReadFile();
        }

        void CreateFile()
        {
            string filepath = Application.dataPath + "/DCXR/IniFile/Examples/Inidemo.ini";
               
            IniFile inif = new IniFile();

            // create a new section
            inif.Create_Section("About");

            // create the key and value pairs
            inif.Set_String("Name", "Haikun Huang", "This is my name.");
            inif.Set_IntArray("Create Date",  new int[]{ 5, 2020});
            inif.Set_String("Web", "https://www.quincyhuanghk.com/");

            // create an other section
            inif.Create_Section("Bio");
            inif.Set_String("Lab", "DCXR");
            inif.Set_String("Web", "https://craigyulab.wordpress.com/code/");
;
            // save the file
            inif.SaveTo(filepath);

            Debug.Log("The inifile demo file is saved to " + filepath);

        }

        void ReadFile()
        {
            string filepath = Application.dataPath + "/DCXR/IniFile/Examples/Inidemo.ini";

            IniFile inif = new IniFile();

            // load the file
            inif.Load_File(filepath);

            // get the data
            inif.Goto_Section("About");
            Debug.Log("My name is " + inif.Get_String("Name"));
            Debug.Log("Create Date is " + inif.Get_String("Create Date"));
            Debug.Log("My web " + inif.Get_String("Web"));


            inif.Goto_Section("Bio");
            Debug.Log("My Lab is " + inif.Get_String("Lab"));
            Debug.Log("Lab web " + inif.Get_String("Web"));
        }



    }
}
