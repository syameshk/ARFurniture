using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PinchScaleAndRotate : MonoBehaviour
{
    public bool invertScale = true;

    ReadOnlyArray<Touch> touches;
    bool isFirstFrameWithTwoTouches = true;
    float cachedTouchAngle;
    float cachedTouchDistance;

    void Update()
    {
        
        this.touches = Touch.activeTouches;

        if (touches.Count == 2)
        {
            float currentTouchDistance = Vector2.Distance(this.touches[0].screenPosition, this.touches[1].screenPosition);
            float diff_y = this.touches[0].screenPosition.y - this.touches[1].screenPosition.y;
            float diff_x = this.touches[0].screenPosition.x - this.touches[1].screenPosition.x;
            float currentTouchAngle = Mathf.Atan2(diff_y, diff_x) * Mathf.Rad2Deg;

            if (this.isFirstFrameWithTwoTouches)
            {
                this.cachedTouchDistance = currentTouchDistance;
                this.cachedTouchAngle = currentTouchAngle;
                this.isFirstFrameWithTwoTouches = false;
                //Raise touch start
                TouchStart();
                return;
            }

            float angleDelta = currentTouchAngle - this.cachedTouchAngle;
            float scaleMultiplier = invertScale ? (this.cachedTouchDistance / currentTouchDistance) : (currentTouchDistance / this.cachedTouchDistance);

            //Raise on value changed
            TouchUpdate(angleDelta, scaleMultiplier);
        }
        else
        {
            //Raise touch end events
            if (!this.isFirstFrameWithTwoTouches)
            {
                //Raise touch end
                TouchEnd();
            }
            this.isFirstFrameWithTwoTouches = true;
        }
    }

    public GameObject spawnedObject;

    float scale
    {
        get
        {
            return spawnedObject.transform.localScale.x;
        }
        set
        {
            spawnedObject.transform.localScale = Vector3.one * value;
        }
    }

    float angle
    {
        get
        {
            return spawnedObject.transform.rotation.eulerAngles.y;
        }
        set
        {
            spawnedObject.transform.rotation = Quaternion.AngleAxis(value, Vector3.up);
        }
    }

    private void TouchStart()
    {
        this.cachedScale = this.scale;
        //this.cachedAugmentationRotation = this.angle;
        this.cachedRotation = lastRotation;
        scaleStarted = true;
    }

    private void TouchEnd()
    {
        this.cachedScale = this.scale;
        //this.cachedAugmentationRotation = this.angle;
        this.cachedRotation = lastRotation;
        scaleStarted = false;
    }

    private void TouchUpdate(float angleDelta, float scaleMultiplier)
    {
        if (!scaleStarted)
            return;

        if (this.enableRotation)
        {
            lastRotation = this.cachedRotation - angleDelta * 3;
            this.angle = lastRotation;
        }
        if (this.enableRotation && this.enableScaling)
        {
            // Optional Pinch Scaling can be enabled via Inspector for this Script Component
            float scaleAmount = this.cachedScale * scaleMultiplier;
            float scaleAmountClamped = Mathf.Clamp(scaleAmount, ScaleRangeMin, ScaleRangeMax);
            this.scale = scaleAmountClamped;
        }
    }

    public bool enableRotation;
    public bool enableScaling;

    float cachedScale;
    float cachedRotation;
    float lastRotation = 0;
    bool scaleStarted = false;

    [SerializeField]
    float ScaleRangeMin = 0.25f;
    [SerializeField]
    float ScaleRangeMax = 5;

}
