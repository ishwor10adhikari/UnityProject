using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScripts : MonoBehaviour
{
    // Reference to the start button
    public Button startButton;

    // Reference to the quit button
    public Button quitButton;

    // Name of the scene to load
    public string sceneToLoad = "SampleScene";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add a listener to the start button
        startButton.onClick.AddListener(LoadScene);

        // Add a listener to the quit button
        quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame

    // Load the scene when the start button is clicked
    void LoadScene()
    {
        // Load the scene asynchronously
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    // Quit the game when quit button is clicked
    void QuitGame()
    {
        Application.Quit();
    }
}
