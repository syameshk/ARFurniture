using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadHelper : MonoBehaviour
{
    SceneHelper helper;
    // Start is called before the first frame update
    void Start()
    {
        helper = FindObjectOfType<SceneHelper>();
        if (helper == null)
            Debug.LogWarning("Could not find SceneHelper");
    }

    public void LoadARView()
    {
        Debug.Log("LoadARView");
        if (helper != null)
            helper.LoadARView();
    }

    public void UnloadARView()
    {
        Debug.Log("LoadARView");
        if (helper != null)
            helper.UnloadARView();
    }
}
