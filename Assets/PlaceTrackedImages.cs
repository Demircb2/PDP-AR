using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceTrackedImages : MonoBehaviour
{
    private ARTrackedImageManager _trackedImageManager;

    public GameObject[] ArPrefabs;

    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();
    void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
    }
    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
           // Loop through all new tracked images that have been detected
        foreach (var trackedImage in eventArgs.added)
        {   
            //Get the name of the referance image
            var imageName = trackedImage.referenceImage.name;
            // Now loop over the array of prefabs
            foreach (var curPrefab in ArPrefabs)
            {
                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0)
                    && !_instantiatedPrefabs.ContainsKey(imageName))
                        {
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
