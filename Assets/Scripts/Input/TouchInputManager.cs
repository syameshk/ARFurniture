using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchInputManager : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        EnhancedTouchSupport.Enable();
    }

    private void Awake()
    {
        
    }

    void OnEnable()
    {
        TouchSimulation.Enable();
    }

    private void OnDisable()
    {
        TouchSimulation.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Touch.activeFingers.Count + Touch.activeFingers.Count);
        
    }
}
