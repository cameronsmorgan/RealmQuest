using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouroutineRunner : MonoBehaviour
{
    private static CouroutineRunner _instance;

    public static CouroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("CoroutineRunner");
                _instance = obj.AddComponent<CouroutineRunner>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public void StartDelayedAction(IEnumerator action)
    {
        StartCoroutine(action);
    }
}

