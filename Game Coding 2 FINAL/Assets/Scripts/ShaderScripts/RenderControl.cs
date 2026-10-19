using UnityEngine;

public class RenderControl : MonoBehaviour
{
    [SerializeField] private Camera[] cameras;
    [SerializeField] private float timeBetweenFrames;
    [SerializeField] private int numAlternations;

    private int currentCameraIndex;
    private float nextFrame;
    void Awake()
    {
        currentCameraIndex = 0;
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
            for(int i = 0; i < 2; i++)
            {
                cameras[i + currentCameraIndex].Render();
            }
            currentCameraIndex = (currentCameraIndex + 2) % cameras.Length;
        }
    }
}
