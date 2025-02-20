using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void moveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
