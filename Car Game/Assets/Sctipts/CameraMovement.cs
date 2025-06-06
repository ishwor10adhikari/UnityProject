using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform PlayerCarTransform;
    public float OffSet = -5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.z = PlayerCarTransform.position.z + OffSet;
        transform.position = cameraPos;
    }
}
