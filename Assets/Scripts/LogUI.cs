using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_Text;

    private static LogUI instance;

    private void Awake()
    {
        instance = this;
    }

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
        LogInternal(logString);
    }


    private void LogInternal(string message)
    {
        m_Text.text = message + "\n" +m_Text.text;
    }

    public static void Log(string message)
    {
        if (instance != null)
            instance.LogInternal(message);
    }
}
