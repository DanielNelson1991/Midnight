﻿using UnityEngine;
using System;
using System.IO;

public class ErrorHandler : MonoBehaviour {

    private static string _ErrorLoggerName = "Midnight_Error_Log.txt";

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

                System.IO.File.WriteAllText(Application.dataPath + @"\Midnight Error Logs\"+_ErrorLoggerName, "Midnight Error Logs");
                System.IO.File.AppendAllText(Application.dataPath + @"\Midnight Error Logs\"+_ErrorLoggerName, "\r\n");
                System.IO.File.AppendAllText(Application.dataPath + @"\Midnight Error Logs\"+_ErrorLoggerName, "\r\n[" + System.DateTime.Now + "] " + type.ToString() + " Occured: " + logString.ToString() + Environment.NewLine);
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
                System.IO.File.AppendAllText(Application.dataPath + @"\Midnight Error Logs\"+_ErrorLoggerName, "\r\n[" + System.DateTime.Now + "] " + type.ToString() + " Occured: " + logString.ToString() + Environment.NewLine);
            }


        }
    }

}
