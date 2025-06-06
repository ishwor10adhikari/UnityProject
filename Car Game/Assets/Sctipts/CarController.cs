using System.Collections;
using UnityEngine; // Import UnityEngine namespace for game development

public class NewMonoBehaviourScript : MonoBehaviour // Define a new MonoBehaviour class
{
    [SerializeField]
    AudioSource touchSound; // Corrected variable name

    [SerializeField]
    AudioSource touchSoundD; // Corrected variable name

    [SerializeField]
    AudioSource Startmusic;

    [SerializeField]
    AudioSource BrakeSound;

    public WheelCollider FrontRightWheelCollider; // Reference to the front right wheel collider
    public WheelCollider FrontLeftWheelCollider; // Reference to the front left wheel collider
    public WheelCollider BackRightWheelCollider; // Reference to the back right wheel collider
    public WheelCollider BackLeftWheelCollider; // Reference to the back left wheel collider

    public Transform FrontRightWheelTransform; // Reference to the front right wheel transform
    public Transform FrontLeftWheelTransform; // Reference to the front left wheel transform
    public Transform BackRightWheelTransform; // Reference to the back right wheel transform
    public Transform BackLeftWheelTransform; // Reference to the back left wheel transform
    public Transform carCenterOfMassTransform;
    private Rigidbody rigidbodyy;
    public float motorForce = 200f;
    public float SteeringAngle = 30f;
    public float FrontWheelBreakForce = 2000f;
    public float BackWheelBreakForce = 3000f;
    private float VerticalInput;
    private float HorizontalInput;

    [SerializeField]
    UIManager uimanager;
    private bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the Rigidbody and set the center of mass
        rigidbodyy = GetComponent<Rigidbody>();
        rigidbodyy.centerOfMass = carCenterOfMassTransform.localPosition;
    }

    // Update is called once per frame
    private void Update() // Use Update for input handling
    {
        if (!isGameOver)
        {
            GetInput(); // Get input from the user
            ApplyBreak(); // Apply braking
            Horn();
            EngineSound();
            GameMusic();
        }
    }

    private void FixedUpdate() // FixedUpdate is used for physics updates
    {
        if (!isGameOver)
        {
            MotorForce(); // Apply motor torque to the wheels
            Updatewheels(); // Update the visual representation of the wheels
            Steering(); // Apply steering
            PowerSteering();
            Debug.Log(CarSpeed());
        }
    }

    void GetInput()
    {
        VerticalInput = Input.GetAxis("Vertical");
        HorizontalInput = Input.GetAxis("Horizontal");
    }

    void ApplyBreak()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FrontRightWheelCollider.brakeTorque = FrontWheelBreakForce;
            FrontLeftWheelCollider.brakeTorque = FrontWheelBreakForce;
            BackRightWheelCollider.brakeTorque = BackWheelBreakForce;
            BackLeftWheelCollider.brakeTorque = BackWheelBreakForce;
            rigidbodyy.linearDamping = 1f;
        }
        else
        {
            FrontRightWheelCollider.brakeTorque = 0f;
            FrontLeftWheelCollider.brakeTorque = 0f;
            BackRightWheelCollider.brakeTorque = 0f;
            BackLeftWheelCollider.brakeTorque = 0f;
            rigidbodyy.linearDamping = 0f;
        }
    }

    void MotorForce() // Method to apply motor torque to the wheels
    {
        FrontLeftWheelCollider.motorTorque = motorForce * VerticalInput; // Set motor torque for the left front wheel
        FrontRightWheelCollider.motorTorque = motorForce * VerticalInput; // Set motor torque for the right front wheel
    }

    void Steering()
    {
        FrontRightWheelCollider.steerAngle = SteeringAngle * HorizontalInput;
        FrontLeftWheelCollider.steerAngle = SteeringAngle * HorizontalInput;
    }

    void PowerSteering() //for to do stratight car afeter every horizontal input,with smoothly
    {
        if (HorizontalInput == 0)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(0, 0, 0),
                Time.deltaTime
            );
        }
    }

    void Updatewheels() // Method to update the wheel transforms
    {
        RotateWheel(FrontRightWheelCollider, FrontRightWheelTransform); // Update front right wheel
        RotateWheel(FrontLeftWheelCollider, FrontLeftWheelTransform); // Update front left wheel
        RotateWheel(BackRightWheelCollider, BackRightWheelTransform); // Update back right wheel
        RotateWheel(BackLeftWheelCollider, BackLeftWheelTransform); // Update back left wheel
    }

    void RotateWheel(WheelCollider wheelCollider, Transform transform) // Method to rotate a wheel
    {
        // Position of the wheel
        // Rotation of the wheel
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot); // Get the world position and rotation from the wheel collider
        transform.SetPositionAndRotation(pos, rot);
    }

    public float CarSpeed()
    {
        float speed = rigidbodyy.linearVelocity.magnitude * 2.23694f;
        return speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Traffic Vehicle"))
        {
            isGameOver = true;
            uimanager.GameOver();
            touchSound.Stop();
            touchSoundD.Stop();
            Startmusic.Stop();
        }
    }

    public void Horn()
    {
        if (!isGameOver && Input.GetKeyDown(KeyCode.P))
        {
            touchSound.volume = 0.5f; // Set volume to 50%
            if (touchSound != null)
            {
                touchSound.Play();
            }
            else
            {
                Debug.LogWarning("touchSound is not assigned.");
            }
        }
    }

    public void EngineSound()
    {
        if (!isGameOver && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            // Calculate the volume based on the car's speed
            float speed = CarSpeed();
            float maxVolume = 3.0f; // Maximum volume
            float minVolume = 0.5f; // Minimum volume
            float volume = Mathf.Lerp(minVolume, maxVolume, speed / 100f); // Map speed to volume

            touchSoundD.volume = volume; // Set the volume

            if (touchSoundD != null)
            {
                touchSoundD.Play();
            }
            else
            {
                Debug.LogWarning("touchSoundD is not assigned.");
            }
        }
    }

    private bool isMusicPlaying = false;

    public void GameMusic()
    {
        if (
            !isGameOver
            && !isMusicPlaying
            && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        )
        {
            Startmusic.volume = 6f; // Set volume to 50%
            if (Startmusic != null)
            {
                Startmusic.Play();
                isMusicPlaying = true;
            }
            else
            {
                Debug.LogWarning("Startmusic is not assigned.");
                // Startmusic.Stop(); // This line will throw an error because Startmusic is null
                isMusicPlaying = false;
            }
        }

        // Stop the music when the game is over
        if (isGameOver && isMusicPlaying)
        {
            if (Startmusic != null)
            {
                Startmusic.Stop();
                isMusicPlaying = false;
            }
        }
    }
}
