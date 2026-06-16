using NUnit.Framework;
using UnityEngine;

public class RenderControl : MonoBehaviour
{
    [SerializeField] private Camera[] cameras;
    [SerializeField] private float timeBetweenFrames;
    private float nextFrame;
    void Awake()
    {
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }
        nextFrame = Time.time + timeBetweenFrames/24f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextFrame)
        {
            nextFrame = Time.time + timeBetweenFrames/24f;
            foreach (Camera cam in cameras) 
            {
                cam.Render();
            }
        }
    }
}
