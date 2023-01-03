using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{
    // Reference to AR tracked image manager component
    private ARTrackedImageManager _trackedImagesManager;

    // List of prefabs to instantiate as their corresponding 2D images
    public GameObject[] ArPrefabsToPlace;

    // Keep dictionary array of created prefabs
    private readonly Dictionary<string, GameObject> _ArPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();

        foreach(GameObject arPrefab in ArPrefabsToPlace)
        {
            GameObject newArObject = Instantiate(arPrefab, Vector3.zero, Quaternion.identity);
            newArObject.name = arPrefab.name;
            _ArPrefabs.Add(arPrefab.name, newArObject);
            newArObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
       
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARImage(trackedImage); 
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _ArPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position); 
    }

    private void AssignGameObject(string name, Vector3 position)
    {
        if(ArPrefabsToPlace != null)
        {
            _ArPrefabs[name].transform.position = position;
            _ArPrefabs[name].SetActive(true);
        }
    }
}
