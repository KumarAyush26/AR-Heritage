using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    public GameObject placedPrefab;
    public GameObject instructionText;
    public GameObject infoButton;
    public GameObject resetButton;         // NEW
    public InfoPanelController infoPanelController;
    public AudioController audioController;
    public ARPlaneManager planeManager;   // NEW - drag XR Origin's AR Plane Manager component here (same object)

    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Start()
    {
        if (instructionText != null) instructionText.SetActive(true);  // visible at start
        if (infoButton != null) infoButton.SetActive(false);
        if (resetButton != null) resetButton.SetActive(false);         // hidden until placed
    }

    void Update()
    {
        if (spawnedObject != null) return;

        Vector2 touchPosition;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            touchPosition = Mouse.current.position.ReadValue();
        else
            return;

        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);

            // Hide all plane visualizations once placed
            if (planeManager != null)
            {
                foreach (var plane in planeManager.trackables)
                    plane.gameObject.SetActive(false);

                planeManager.enabled = false;  // stop detecting/updating further planes
            }

            if (instructionText != null) instructionText.SetActive(false);
            if (infoButton != null) infoButton.SetActive(true);
            if (resetButton != null) resetButton.SetActive(true);   // NEW - show once placed
        }
    }

    public void ResetPlacement()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }

        if (planeManager != null)
        {
            planeManager.enabled = true;
        }

        if (instructionText != null) instructionText.SetActive(true);
        if (infoButton != null) infoButton.SetActive(false);
        if (resetButton != null) resetButton.SetActive(false);      // NEW - hide again after reset
        if (infoPanelController != null) infoPanelController.HideInfo();
        if (audioController != null) audioController.StopAudio();
    }
}