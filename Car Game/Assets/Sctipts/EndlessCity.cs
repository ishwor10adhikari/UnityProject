using UnityEngine;

public class EndlessCity : MonoBehaviour
{
    public Transform PlayerCarTransform;
    public Transform OtherCityTransform;
    public float HalfLength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        if (PlayerCarTransform.position.z > transform.position.z + HalfLength + 20f)
        {
            transform.position = new Vector3(
                -0.78f,
                -19.19235f,
                OtherCityTransform.position.z + HalfLength * 2
            );
        }
    }
}
