using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.ComponentModel;

/// <summary>
///
/// MIT license
/// Created by Haikun Huang
/// Date: 2016
/// </summary>
///

namespace DCXR
{
    public class CSVReader
    {
        string filePath = "";

        // mapping table
        // use this table for mapping header to index
        Dictionary<string, int> indexTable;

        // context List
        List<List<string>> contextList;
        // iter cursor
        int cursor = 0;


        public CSVReader()
        {
            Clear();
        }

        public void Clear()
        {
            indexTable = new Dictionary<string, int>();
            contextList = new List<List<string>>();
            cursor = 0;
        }


        // load file, rreturn false if the given file isn't exist.
        // if mapping_header == false, then will skip this step, it is for appending an extra file to the previous one (with same format).
        public bool Load(string path, bool mapping_header = true)
        {
            // check if exist
            if (!File.Exists(path))
            {
                return false;
            }

            filePath = path;

            StreamReader sr = new StreamReader(path);

            // head
            string line = sr.ReadLine();
            if (mapping_header)
            {
                string[] headers = line.Split(',');
                for (int i = 0; i < headers.Length; i++)
                {
                    // replace "\"" to ""
                    headers[i] = headers[i].Replace("\"", "");
                    // Debug.Log ("header: " + headers[i]);
                    indexTable.Add(headers[i], i);
                }
            }

            // context
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] data = line.Split(',');

                List<string> current = new List<string>();

                for (int i = 0; i < data.Length;)
                {
                    // string
                    if (!data[i].StartsWith("\""))
                    {
                        current.Add((data[i++]));
                    }
                    // array e.g: "a,a,a,a"
                    else
                    {
                        string str = "";
                        for (; i < data.Length;)
                        {
                            str += data[i++];
                            if (data[i - 1].EndsWith("\""))
                            {
                                break;
                            }
                            else
                            {
                                str += ",";
                            }
                        }

                        // replace "\"" to ""
                        str = str.Replace("\"", "");
                        current.Add(str);
                    }

                }

                contextList.Add(current);

            }

            sr.Close();
            return true;

        }

        public int Count()
        {
            return contextList.Count;
        }

        // if count >0 then return true, otherwise return false
        public bool Reset_Cursor()
        {
            cursor = 0;
            return !EOF();
        }

        // move to next index and cheak the EOF flag, return TURE is has next.
        // Reset_Cursor(); do {...} while (Next());
        public bool Next()
        {
            cursor++;
            return !EOF();
        }

        public bool EOF()
        {
            return cursor >= contextList.Count;
        }

        // get the origin record
        public List<string> Get_Origin_Record()
        {
            return contextList[cursor];
        }

        // get the data by the given column name
        public string Get_String(string colName)
        {
            return Get_Origin_Record()[indexTable[colName]];
        }

        // get the array by the give column name
        public string[] Get_String_Array(string colName)
        {
            return Get_String(colName).Split(',');
        }



    }
}