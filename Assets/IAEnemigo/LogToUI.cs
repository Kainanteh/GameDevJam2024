using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LogToUI : MonoBehaviour
{
    public TextMeshPro logText;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logText.text += logString + "\n";
    }
}
