using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI SpeedText;

    [SerializeField]
    TextMeshProUGUI DistanceText;

    [SerializeField]
    TextMeshProUGUI ScoreText;

    [SerializeField]
    TextMeshProUGUI TotalScoreText;

    [SerializeField]
    TextMeshProUGUI TotalDistanceText;

    [SerializeField]
    TextMeshProUGUI MaximumSpeedText;

    [SerializeField]
    GameObject car;

    [SerializeField]
    Transform CarTransform;

    [SerializeField]
    GameObject GameOverPannel;

    [SerializeField]
    GameObject SpeedIcon;

    [SerializeField]
    GameObject DistanceIcon;

    [SerializeField]
    GameObject ScoreIcon;

    [SerializeField]
    Button mainMenuButton; // Added reference to main menu button

    private float maximumSpeed = 0f;
    private Rigidbody rigidbodyy;
    private float speed = 0f;
    private float distance = 0f;
    private float score = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the Rigidbody
        rigidbodyy = car.GetComponent<Rigidbody>();
        GameOverPannel.SetActive(false);
        SpeedIcon.SetActive(true);
        DistanceIcon.SetActive(true);
        ScoreIcon.SetActive(true);
        Time.timeScale = 1f;

        // Add a listener to the main menu button
        mainMenuButton.onClick.AddListener(LoadScene1);
    }

    // Update is called once per frame
    void Update()
    {
        DistanceUi();
        SpeedUi();
        ScoreUi();
        MaximumSpeed();
    }

    public float CarSpeed()
    {
        float speed = rigidbodyy.linearVelocity.magnitude * 2.23694f;
        return speed;
    }

    void DistanceUi()
    {
        distance = CarTransform.position.z / 1000;
        DistanceText.text = distance.ToString("0.00" + "Km");
    }

    public void SpeedUi()
    {
        speed = CarSpeed();
        SpeedText.text = speed.ToString("0" + "Km/h");
    }

    public void ScoreUi()
    {
        score = CarTransform.position.z * 6;
        ScoreText.text = score.ToString("0");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        GameOverPannel.SetActive(true);
        SpeedIcon.SetActive(false);
        DistanceIcon.SetActive(false);
        ScoreIcon.SetActive(false);
        TotalScoreText.text = score.ToString("0");
        TotalDistanceText.text = distance.ToString("0.00" + "Km");
    }

    void MaximumSpeed()
    {
        float CurrentSpeed = CarSpeed();
        if (CurrentSpeed > maximumSpeed)
        {
            maximumSpeed = CurrentSpeed;
        }
        MaximumSpeedText.text = maximumSpeed.ToString("0" + "Km/h");
    }

    public void Tryagain()
    {
        var CurrentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(CurrentScene.name);
    }

    // Load Scene1 when main menu button is clicked
    void LoadScene1()
    {
        SceneManager.LoadScene("Scene1");
    }
}
