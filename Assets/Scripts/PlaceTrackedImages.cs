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
            //var imageName = trackedImage.referenceImage.name;
            //foreach (var curPrefab in ArPrefabsToPlace)
            //{
            //    if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0 && !_ArPrefabs.ContainsKey(imageName))
            //    {
            //        var newPrefab = Instantiate(curPrefab, trackedImage.transform);
            //        _ArPrefabs[imageName] = newPrefab;
            //    }
            //}
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARImage(trackedImage); 
        //    _instatiatedPrefabs[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _ArPrefabs[trackedImage.name].SetActive(false);
        //    Destroy(_instatiatedPrefabs[trackedImage.referenceImage.name]);
        //    _instatiatedPrefabs.Remove(trackedImage.referenceImage.name);
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
            _ArPrefabs[name].SetActive(true);
            _ArPrefabs[name].transform.position = position;
            foreach(GameObject go in _ArPrefabs.Values)
            {
                if(go.name != name)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}
