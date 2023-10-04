using UnityEngine;

public class CameraAspectRatioFixer : MonoBehaviour
{
    public float targetAspect = 9f / 16f; // Desired Aspect Ratio, e.g., 16:9

    void Start ()
    {
        Camera mainCamera = GetComponent<Camera>();
        if (mainCamera.orthographic)
        {
            float windowAspect = (float)Screen.width / Screen.height;
            float scaleHeight = windowAspect / targetAspect;
            mainCamera.orthographicSize /= scaleHeight;
        }
    }
}
