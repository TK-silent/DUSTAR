using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public Camera cameraA;
    public Camera cameraB;
    public float transitionDuration = 2.0f;
    private float t = 0.0f;
    private bool isTransitioning = false;

    void Update()
    {
        if (isTransitioning)
        {
            t += Time.deltaTime / transitionDuration;
            Camera.main.transform.position = Vector3.Lerp(cameraA.transform.position, cameraB.transform.position, t);
            Camera.main.transform.rotation = Quaternion.Slerp(cameraA.transform.rotation, cameraB.transform.rotation, t);

            if (t >= 1.0f)
            {
                isTransitioning = false;
                t = 0.0f;
            }
        }
    }

    public void StartTransition()
    {
        t = 0.0f;
        isTransitioning = true;
    }
}