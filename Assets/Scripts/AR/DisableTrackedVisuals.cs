using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DisableTrackedVisuals : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Disables spawned planes and ARPlaneManager")]
    bool m_DisablePlaneRendering;

    public bool disablePlaneRendering
    {
        get => m_DisablePlaneRendering;
        set => m_DisablePlaneRendering = value;
    }

    [SerializeField]
    ARPlaneManager m_PlaneManager;

    public ARPlaneManager planeManager
    {
        get => m_PlaneManager;
        set => m_PlaneManager = value;
    }

    [SerializeField]
    PlaceObjects m_PlaceObjects;

    void OnEnable()
    {
        m_PlaceObjects.onPlacedObject.AddListener(OnPlacedObject);
    }

    void OnDisable()
    {
        m_PlaceObjects.onPlacedObject.RemoveListener(OnPlacedObject);
    }

    void OnPlacedObject()
    {
        if (m_DisablePlaneRendering)
        {
            m_PlaneManager.SetTrackablesActive(false);
            m_PlaneManager.enabled = false;
        }
    }

    public void TogglePlaneDetection()
    {
        m_PlaneManager.enabled = !m_PlaneManager.enabled;
        if (m_PlaneManager.enabled)
        {
            SetAllPlanesActive(true);
        }
        else
        {
            SetAllPlanesActive(false);
        }
    }

    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in m_PlaneManager.trackables)
            plane.gameObject.SetActive(value);
    }
}
