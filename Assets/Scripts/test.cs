using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDebugLogger : MonoBehaviour
{
    private ARPlaneManager planeManager;

    void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
    }

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        Debug.Log($"Planes added: {args.added.Count}, updated: {args.updated.Count}, removed: {args.removed.Count}");
    }
}