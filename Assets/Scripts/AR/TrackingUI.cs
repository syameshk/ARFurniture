using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class TrackingUI : MonoBehaviour
{
    [SerializeField]
    GameObject m_InstructionParent;

    public GameObject instructionParent
    {
        get => m_InstructionParent;
        set => m_InstructionParent = value;
    }

    [SerializeField]
    [Tooltip("Instructional test for visual UI")]
    TMP_Text m_InstructionText;

    /// <summary>
    /// Get the <c>Instructional Text</c>
    /// </summary>
    public TMP_Text instructionText
    {
        get => m_InstructionText;
        set => m_InstructionText = value;
    }

    [SerializeField]
    [Tooltip("Move device animation")]
    VideoClip m_FindAPlaneClip;

    [SerializeField]
    [Tooltip("Tap to place animation")]
    VideoClip m_TapToPlaceClip;

    [SerializeField]
    [Tooltip("Video player reference")]
    VideoPlayer m_VideoPlayer;

    public VideoPlayer videoPlayer
    {
        get => m_VideoPlayer;
        set => m_VideoPlayer = value;
    }

    [SerializeField]
    [Tooltip("Raw image used for videoplayer reference")]
    RawImage m_RawImage;

    public RawImage rawImage
    {
        get => m_RawImage;
        set => m_RawImage = value;
    }

    [SerializeField]
    [Tooltip("Instruction delay before it disapear")]
    float m_InstructionTime = 4;
    float m_LastInstructionTime = -4;
    [SerializeField]
    CanvasGroup m_CanvasGroup;

    [SerializeField]
    ARPlaneManager m_PlaneManager;

    private bool m_PlacedObject = false;

    public ARPlaneManager planeManager
    {
        get => m_PlaneManager;
        set => m_PlaneManager = value;
    }

    public enum State { None, Looking, PlaneDetected, Placed}
    private State m_State;
    public State state
    {
        get => m_State;
        set
        {
            if(m_State != value) { m_State = value; OnStateChanged(); }
        }
    }

    const string k_MoveDeviceText = "Move Device Slowly";
    const string k_TapToPlaceText = "Tap to Place AR";

    IEnumerator Start()
    {
        state = State.None;
        m_InstructionParent.SetActive(false);
        //Wait till AR session gets ready
        while (ARSession.state < ARSessionState.Ready) {
            yield return null;
        }
        Debug.Log("TrackingUI State :" + ARSession.state);
        state = State.Looking;
    }

    private void OnStateChanged()
    {
        if (state == State.None)
        {
            m_InstructionParent.SetActive(false);
            return;
        }

        if(state == State.Looking)
        {
            //Show Find a Plane UI
            ShowFindAPlane();
            return;
        }

        if(state == State.PlaneDetected)
        {
            //Show Tap to place UI
            ShowTaptoPlace();
            return;
        }

        if(state == State.Placed)
        {
            //Hide all UI
            m_InstructionParent.SetActive(false);
        }
    }

    private void Update()
    {
        //Return if we are not ready
        if(state == State.None)
        {
            return;
        }

        m_CanvasGroup.alpha = (Time.time - m_LastInstructionTime > m_InstructionTime) ? 0 : 1;

        if(state == State.Looking)
        {
            if (PlanesFound())
                state = State.PlaneDetected;
        }

        if(state == State.PlaneDetected)
        {
            if(m_PlacedObject)
                state = State.Placed;
        }
    }

    bool PlanesFound() => m_PlaneManager && m_PlaneManager.trackables.count > 0;

    public void OnPlacedObject() => m_PlacedObject = true;

    private void ShowFindAPlane()
    {
        instructionParent.SetActive(true);
        instructionText.text = k_MoveDeviceText;
        videoPlayer.clip = m_FindAPlaneClip;
        videoPlayer.Play();
        m_LastInstructionTime = Time.time;
    }

    private void ShowTaptoPlace()
    {
        instructionParent.SetActive(true);
        instructionText.text = k_TapToPlaceText;
        videoPlayer.clip = m_TapToPlaceClip;
        videoPlayer.Play();
        m_LastInstructionTime = Time.time;
    }

    [ContextMenu("TestForceLooking")]
    public void TestForceLooking()
    {
        StopAllCoroutines();
        state = State.Looking;
    }

    [ContextMenu("TestForceDetected")]
    public void TestForceDetected()
    {
        state = State.PlaneDetected;
    }

    [ContextMenu("TestFlipPlacementBool")]
    public void TestFlipPlacementBool()
    {
        m_PlacedObject = true;
    }
}
