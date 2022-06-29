using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHelper : MonoBehaviour
{

    [SerializeField]
    string m_ARSceneName = "ARView";
    [SerializeField]
    Camera m_UICamera;
    [SerializeField]
    Canvas m_MainCanvas;
    [SerializeField]
    GameObject m_EventSystem;

    public void LoadARView()
    {
        //m_EventSystem.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(m_ARSceneName, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnARSceneLoaded;
    }

    private void OnARSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= OnARSceneLoaded;
        //Hide UI
        m_MainCanvas.gameObject.SetActive(false);
        //Hide UI Camera
        m_UICamera.gameObject.SetActive(false);
        //m_EventSystem.gameObject.SetActive(false);
    }

    public void UnloadARView()
    {
        SceneManager.UnloadSceneAsync(m_ARSceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        SceneManager.sceneUnloaded += OnARSceneUnloaded;
    }

    private void OnARSceneUnloaded(Scene arg0)
    {
        SceneManager.sceneUnloaded -= OnARSceneUnloaded;
        //Show UI
        m_MainCanvas.gameObject.SetActive(true);
        //Show Camera
        m_UICamera.gameObject.SetActive(true);
        //m_EventSystem.gameObject.SetActive(true);
    }
}
