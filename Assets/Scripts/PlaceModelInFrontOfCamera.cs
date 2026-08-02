using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceModelInFrontOfCamera : MonoBehaviour
{
    public GameObject placedPrefab;
    public Camera arCamera;
    public float placementDistance = 1.5f;
    public ARAnchorManager anchorManager;

    public GameObject instructionText;
    public GameObject infoButton;
    public GameObject resetButton;
    public GameObject placeButton;
    public InfoPanelController infoPanelController;
    public AudioController audioController;

    private GameObject spawnedObject;
    private ARAnchor currentAnchor;

    void Start()
    {
        if (instructionText != null) instructionText.SetActive(true);
        if (infoButton != null) infoButton.SetActive(false);
        if (resetButton != null) resetButton.SetActive(false);
        if (placeButton != null) placeButton.SetActive(true);
    }

    public void PlaceModel()
    {
        if (spawnedObject != null) return;

        Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * placementDistance;
        Quaternion spawnRotation = Quaternion.Euler(0f, arCamera.transform.eulerAngles.y, 0f);

        // Create an empty anchor GameObject at the target pose
        GameObject anchorObject = new GameObject("PlacementAnchor");
        anchorObject.transform.position = spawnPosition;
        anchorObject.transform.rotation = spawnRotation;

        currentAnchor = anchorObject.AddComponent<ARAnchor>();

        // Parent the model to the anchor
        spawnedObject = Instantiate(placedPrefab, Vector3.zero, Quaternion.identity, anchorObject.transform);
        spawnedObject.transform.localPosition = Vector3.zero;
        spawnedObject.transform.localRotation = Quaternion.identity;

        if (instructionText != null) instructionText.SetActive(false);
        if (infoButton != null) infoButton.SetActive(true);
        if (resetButton != null) resetButton.SetActive(true);
        if (placeButton != null) placeButton.SetActive(false);
    }

    public void ResetPlacement()
    {
        if (currentAnchor != null)
        {
            Destroy(currentAnchor.gameObject);   // this also destroys spawnedObject since it's a child
            currentAnchor = null;
            spawnedObject = null;
        }

        if (instructionText != null) instructionText.SetActive(true);
        if (infoButton != null) infoButton.SetActive(false);
        if (resetButton != null) resetButton.SetActive(false);
        if (placeButton != null) placeButton.SetActive(true);
        if (infoPanelController != null) infoPanelController.HideInfo();
        if (audioController != null) audioController.StopAudio();
    }
}