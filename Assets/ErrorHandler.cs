using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.IO;

public class ErrorHandler : MonoBehaviour {

    void Start()
    {
        Application.logMessageReceived += HandleLog;
    }

    public static void HandleLog(string logString, string stackTrace, LogType type)
    {
        string output = "";
        string stack = "";

        output = logString;
        stack = stackTrace;

        if(Directory.Exists(Application.dataPath+@"\Midnight Error Logs") == false)
        {
            try
            {
                Directory.CreateDirectory(Application.dataPath + @"\Midnight Error Logs");

                System.IO.File.WriteAllText(Application.dataPath + @"\Midnight Error Logs\errpr_logs.txt", "Midnight Error Logs");
                System.IO.File.AppendAllText(Application.dataPath + @"\Midnight Error Logs\errpr_logs.txt", "\r\n");
                System.IO.File.AppendAllText(Application.dataPath + @"\Midnight Error Logs\errpr_logs.txt", "\r\n[" + System.DateTime.Now + "] " + type.ToString() + " Occured: " + logString.ToString() + Environment.NewLine);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }

            finally { }
        } else
        {
            if(type == LogType.Warning || type == LogType.Error)
            {
                System.IO.File.AppendAllText(Application.dataPath + @"\Midnight Error Logs\errpr_logs.txt", "\r\n[" + System.DateTime.Now + "] " + type.ToString() + " Occured: " + logString.ToString() + Environment.NewLine);
            }


        }
    }

}
