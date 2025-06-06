using System.Collections;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    [SerializeField]
    Transform[] Lanes; // Exposing an array of Transform objects to the Unity editor for lane positions

    [SerializeField]
    GameObject[] Vehicles; // Exposing an array of GameObjects to the Unity editor for traffic vehicle prefabs

    [SerializeField]
    GameObject car; // Exposing a GameObject to the Unity editor for the player car

    [SerializeField]
    float MinSpwanTime = 30f; // Exposing a minimum spawn time variable to the Unity editor with a default value of 30f

    [SerializeField]
    float MaxSpwanTime = 35f; // Exposing a maximum spawn time variable to the Unity editor with a default value of 35f
    private Rigidbody rigidbodyy; // Declaring a private Rigidbody variable to store the player car's Rigidbody component
    private float spawnTimer = 0f; // Declaring a private float variable to store the spawn timer

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() // Called when the script is initialized
    {
        rigidbodyy = car.GetComponent<Rigidbody>(); // Getting the Rigidbody component of the player car
        // Start the traffic spawner coroutine
        StartCoroutine(TrafficSpawner()); // Starting the TrafficSpawner coroutine
    }

    // Coroutine to spawn traffic
    IEnumerator TrafficSpawner() // A coroutine that spawns traffic vehicles
    {
        yield return new WaitForSeconds(4f); // Waiting for 4 seconds before starting the traffic spawn loop
        while (true) // An infinite loop to continuously spawn traffic vehicles
        {
            // Calculate a dynamic timer based on the player car's speed
            float DynamicTimer = UnityEngine.Random.Range(MinSpwanTime, MaxSpwanTime) / CarSpeed(); // Using UnityEngine.Random to calculate a random timer value
            if (CarSpeed() > 1f) // Checking if the player car's speed is greater than 1f
            {
                // Check if the spawn timer has exceeded the minimum spawn interval
                if (spawnTimer >= 2f)
                {
                    SpawnTrafficVehicle(); // Spawning a traffic vehicle if the condition is met
                    spawnTimer = 0f; // Resetting the spawn timer
                }
                else
                {
                    spawnTimer += Time.deltaTime; // Incrementing the spawn timer
                }
            }
            yield return new WaitForSeconds(DynamicTimer); // Waiting for the calculated dynamic timer before spawning the next traffic vehicle
        }
    }

    public float CarSpeed() // A method to calculate the player car's speed
    {
        // Calculate the player car's speed in miles per hour
        float speed = rigidbodyy.linearVelocity.magnitude * 2.23694f; // Converting the player car's linear velocity to miles per hour
        return speed; // Returning the calculated speed
    }

    void SpawnTrafficVehicle() // A method to spawn a traffic vehicle
    {
        // Randomly select a lane and a traffic vehicle prefab
        int randomLaneIndex = UnityEngine.Random.Range(0, Lanes.Length); // Using UnityEngine.Random to randomly select a lane index
        int randomTrafficVehicleIndex = UnityEngine.Random.Range(0, Vehicles.Length); // Using UnityEngine.Random to randomly select a traffic vehicle prefab index

        // Check if the spawn point is clear
        if (IsSpawnPointClear(Lanes[randomLaneIndex].position))
        {
            // Instantiate the traffic vehicle at the selected lane position
            Instantiate(
                Vehicles[randomTrafficVehicleIndex],
                Lanes[randomLaneIndex].position,
                Quaternion.identity
            ); // Instantiating the traffic vehicle at the selected lane position with no rotation
        }
        else
        {
            // If the spawn point is not clear, try again
            SpawnTrafficVehicle();
        }
    }

    bool IsSpawnPointClear(Vector3 spawnPoint) // A method to check if the spawn point is clear
    {
        // Create a sphere cast to check for collisions
        Collider[] hits = Physics.OverlapSphere(spawnPoint, 2f); // Creating a sphere cast with a radius of 2f

        // Check if any vehicles are within the spawn point
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Traffic Vehicle"))
            {
                return false; // Returning false if a vehicle is found within the spawn point
            }
        }

        return true; // Returning true if no vehicles are found within the spawn point
    }

    void Update()
    {
        // Update the spawn timer
        spawnTimer += Time.deltaTime;

        // Check if the spawn timer has exceeded the maximum spawn time
        if (spawnTimer > MaxSpwanTime)
        {
            // Reset the spawn timer
            spawnTimer = 0f;
        }
    }
}
