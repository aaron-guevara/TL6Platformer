using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collison.tag == "Player")
        {
            SceneManager.LoadScene(WinningScreen)
        }
    }


}
