using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchInputManager : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        EnhancedTouchSupport.Enable();
    }
}
