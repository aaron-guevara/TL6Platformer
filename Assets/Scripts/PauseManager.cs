using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public static bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                resumeGame();
            }
            else {
                pauseGame();
            }
        }
    }

    public void resumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void pauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void mainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }  

    public void quitGame()
    {
        Application.Quit();
    }
}
