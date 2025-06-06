using UnityEngine;

public class LaneMovement : MonoBehaviour
{
    public Transform PlayerCarTransform;
    public float OffSet = -5;

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.z = PlayerCarTransform.position.z + OffSet;
        transform.position = cameraPos;
    }
}
