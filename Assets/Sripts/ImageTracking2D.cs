using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARTrackedImageManager))] //Obrigar a existir um componente deste tipo no projecto


public class ImageTracking2D : MonoBehaviour
{

    [SerializeField]
    private GameObject[] placeablePrefabs;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private GameObject NotFindAlert;
    private bool ActivateAlert = true;


    //Awake is called either when an active GameObject that contains the script is initialized when a Scene loads,
    //or when a previously inactive GameObject is set to active, or after a GameObject created with Object.
    //Instantiate is initialized. Use Awake to initialize variables or states before the application starts.
    private void Awake()
    {

        NotFindAlert = GameObject.Find("AlertToFind");

        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        //associate prefabs to the images to be tracked on the ARTrackedImageManager 
        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    //when the object its enabled
    private void OnEnable()
    {
        //get the events of the trackedImageManager
        trackedImageManager.trackedImagesChanged += imageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= imageChanged;

    }

    //dependind the type ->have a deferent action
    private void imageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        ActivateAlert = true;

        //a new image was tracked, the prefab needs to bem added on image
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        //update positions
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        //desable all images that desapears
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }

        NotFindAlert.SetActive(ActivateAlert);
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name; //get name of the image tracked
        Vector3 position = trackedImage.transform.position; //get position of the image traked
        Quaternion rotation = trackedImage.transform.rotation;

        Vector3 posOffset = new Vector3(0, -0.0578f, -0.0261f);

        GameObject prefab = spawnedPrefabs[name]; //get prefab with same name
        prefab.transform.rotation = rotation * Quaternion.Euler(-90f, 0, -180f); //rotate on that position
        prefab.transform.position = position + posOffset;   //set possition the same as image

        switch (trackedImage.trackingState)
        {
            case UnityEngine.XR.ARSubsystems.TrackingState.None:
            case UnityEngine.XR.ARSubsystems.TrackingState.Limited:
                prefab.SetActive(false);       //set activated the prefab
                break;
            case UnityEngine.XR.ARSubsystems.TrackingState.Tracking:
                prefab.SetActive(true);       //set activated the prefab
                ActivateAlert = false;
                break;
            default:
                prefab.SetActive(false);       //set activated the prefab
                break;
        }
    }
}

