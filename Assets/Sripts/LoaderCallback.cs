using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LoaderCallback : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isFirstTimeUpdate = false;
    public VideoPlayer video;

    private void Start()
    {
        video = GameObject.Find("VideoPanel").GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
    }

    private void CheckOver(VideoPlayer source)
    {
        isFirstTimeUpdate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstTimeUpdate)
        {
            isFirstTimeUpdate = false;
            Loader.LoaderCalback();
        }
    }
}
