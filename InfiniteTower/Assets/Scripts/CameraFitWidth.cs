using UnityEngine;

public class CameraFitWidth : MonoBehaviour
{
    [Tooltip("The width of the world in units.")]
    public float targetWorldWidth = 10f;
    void Awake() {
        Camera cam = GetComponent<Camera>();
        cam.orthographic = true;
        float screenAspect = (float)Screen.height / Screen.width;
        cam.orthographicSize = (targetWorldWidth * screenAspect) / 2f;
        

        
    }
    
}
