using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    public GameObject placedPrefab;
    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        Vector2 touchPosition;

        // Try touch first (real device), fall back to mouse (Editor testing)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            touchPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (spawnedObject == null)
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            else
                spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
        }
    }
}