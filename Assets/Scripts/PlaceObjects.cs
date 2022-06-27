using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjects : MonoBehaviour
{
    public CatalogItemSelect selectedItem;
    public GameObject m_PlacedPrefab;
    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public UnityEvent onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    [SerializeField]
    bool m_CanReposition = true;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        if (selectedItem != null)
            m_PlacedPrefab = selectedItem.Selected.Model;
        else
            Debug.LogWarning("Need to set selected item object");

        Debug.Log("PlaceObjects Awake");
    }

    void Update()
    {
        //Debug.Log("PlaceObjects Update "+Input.mousePosition);
        //Debug.Log(Touch.activeFingers.Count + Touch.activeFingers.Count);
        if (Touch.activeTouches.Count > 0)
        {
            Debug.Log("PlaceObjects Update "+ Touch.activeTouches.Count);
            Touch touch = Touch.activeTouches[0];

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.screenPosition, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    if(spawnedObject == null)
                    {
                        Debug.Log("Added object");
                        spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);

                        if (onPlacedObject != null)
                        {
                            onPlacedObject.Invoke();
                        }
                    }
                    else
                    {
                        if (m_CanReposition)
                        {
                            spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                        }
                    }

                    
                }
            }
        }
    }

    [ContextMenu("ForcePlaceItem")]
    public void ForcePlaceItem()
    {
        Vector3 position = new Vector3(0, -0.5f, 2);
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(m_PlacedPrefab, position, Quaternion.identity);

            if (onPlacedObject != null)
            {
                onPlacedObject.Invoke();
            }
        }
        else
        {
            if (m_CanReposition)
            {
                spawnedObject.transform.SetPositionAndRotation(position, Quaternion.identity);
            }
        }
    }
}
