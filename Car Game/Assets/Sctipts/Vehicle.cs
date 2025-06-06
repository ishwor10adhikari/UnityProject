using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float speed = 0f;

    void Update()
    {
        transform.Translate(0f, 0f, speed * Time.deltaTime);
        if (gameObject.transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }
}
