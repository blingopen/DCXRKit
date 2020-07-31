using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using UnityEngine.Events;

using Debug = UnityEngine.Debug;


/// <summary>
/// The core function of the UPython
///
/// MIT license
/// Created by Haikun Huang
/// Date: 2020.5.26
/// Update: 2020.6.22
/// </summary>

namespace DCXR
{
    [AddComponentMenu("HHK/UPython")]
    public class UPython : MonoBehaviour
    {
        // need to be modified, point the python to your local python program.
        static public string python_program = "python";

        Process proc;
        ProcessStartInfo startInfo;


        // the running relative path and filename;
        public string runningFolder = "../Python";
        public string runningFile = "demo.py";

        public bool showDebug = true;


        // is running?
        public bool isRunning { get; private set; } = false;

        // the python script buffer
        public List<string> script = new List<string>();

        // the output
        public List<string> results = new List<string>();
        public List<string> errs = new List<string>();

        // store the variables with the'@' at the beginning of each line of the output stream
        // e.g., @a=1
        // how to get the "a" variable?
        // e.g., string something = variables["a"];
        public Dictionary<string, string> variables = new Dictionary<string, string>();

        /// <summary>
        /// Create a new Environment so that the previous result and script can be held by other references.
        /// </summary>
        public void NewEnvironment()
        {
            script = new List<string>();
            results = new List<string>();
            errs = new List<string>();
            variables = new Dictionary<string, string>();
        }

        // Build the python file
        void Build(string folder, string filename)
        {
            string filepath = Path.Combine(folder, filename);

            // check if the file existed
            if (File.Exists(filepath))
            {
                // delete the file
                File.Delete(filepath);
            }

            // check if the folder existed
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // create the py file
            FileStream stream = File.Create(filepath);
            StreamWriter writer = new StreamWriter(stream);

            foreach(var s in script)
            {

                writer.WriteLine(s);
            }

            writer.Close();
        }


        /// <summary>
        /// Run the python script
        ///
        /// If the callback function is not null,
        /// then the callback function will be invoked when the python script is done.
        /// </summary>
        /// <param name="callback"></param>
        public void BuildAndRun(string args ="", UnityAction callback = null)
        {
            StopCoroutine(_Run("",null, true));
            isRunning = false;

            StartCoroutine(_Run(args,callback,true));
        }


        public void RunFromFile(string args = "", UnityAction callback = null)
        {
            StopCoroutine(_Run("", null, false));
            isRunning = false;

            StartCoroutine(_Run(args, callback, false));
        }


        // Start is called before the first frame update
        IEnumerator _Run(string args,UnityAction callback, bool buildfile)
        {
            isRunning = true;

            // clear the result and errs
            results.Clear();
            errs.Clear();
            variables.Clear();

            // create the py file
            if(buildfile)
                Build(Path.Combine(Application.dataPath, runningFolder), runningFile);



            startInfo = new ProcessStartInfo(python_program, Path.Combine(Application.dataPath, runningFolder, runningFile) + " " + args); // execute the python file.
            startInfo.WorkingDirectory = Path.Combine(Application.dataPath, runningFolder);
            if (showDebug)
                Debug.Log("[UPython] Start, WorkingDirectory = " + startInfo.WorkingDirectory);

            startInfo.CreateNoWindow = true;         // 不创建新窗口    
            startInfo.UseShellExecute = false;       //不启用shell启动进程  
            startInfo.RedirectStandardOutput = true; // 重定向标准输出    
            startInfo.RedirectStandardError = true;  // 重定向错误输出

            startInfo.WindowStyle = ProcessWindowStyle.Normal;



            yield return null;
            proc = Process.Start(startInfo); // 执行
            yield return null;
            

            //// 重定向数据流
            StreamReader sr = proc.StandardOutput;
            StreamReader srerr = proc.StandardError;

            while (!proc.HasExited)
            {
                yield return null;
            }

            //  读取输出流
            while (!srerr.EndOfStream)
            {
                string line = srerr.ReadLine();
                errs.Add(line);

                
                Debug.LogError("[UPython Error] " + line);
              
                yield return null;
            }


            //  读取输出流
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                results.Add(line);

                if(line.StartsWith("@"))
                {
                    // store
                    string[] pair = line.Split('=');
                    string key = pair[0].Trim().Replace("@", "");
                    string v = pair[1].Trim();
                    if(variables.ContainsKey(key))
                    {
                        variables[key] = v;
                    }
                    else
                    {
                        variables.Add(key, v);
                    }

                }


                if (showDebug)
                    Debug.Log("[UPython] " + line);

                yield return null;
            }

            if (showDebug)
                Debug.Log("[UPython] Exited!");

            if (callback != null)
                callback();



            isRunning = false;

            yield return null;
        }

    }
}