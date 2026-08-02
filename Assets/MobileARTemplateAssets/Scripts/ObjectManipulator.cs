using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ObjectManipulator : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public float minScale = 0.05f;
    public float maxScale = 2f;

    private Vector3 initialScale;
    private float initialPinchDistance;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        initialScale = transform.localScale;
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        var touches = Touch.activeTouches;

        if (touches.Count == 1)
        {
            // One finger = rotate
            Touch t = touches[0];
            if (t.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                float deltaX = t.delta.x;
                transform.Rotate(Vector3.up, -deltaX * rotationSpeed, Space.World);
            }
        }
        else if (touches.Count == 2)
        {
            // Two fingers = pinch to scale
            Touch t0 = touches[0];
            Touch t1 = touches[1];

            float currentDistance = Vector2.Distance(t0.screenPosition, t1.screenPosition);

            if (t0.phase == UnityEngine.InputSystem.TouchPhase.Began || t1.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                initialPinchDistance = currentDistance;
                initialScale = transform.localScale;
            }
            else if (initialPinchDistance > 0)
            {
                float scaleFactor = currentDistance / initialPinchDistance;
                Vector3 newScale = initialScale * scaleFactor;

                float clamped = Mathf.Clamp(newScale.x, minScale, maxScale);
                transform.localScale = new Vector3(clamped, clamped, clamped);
            }
        }
    }
}
