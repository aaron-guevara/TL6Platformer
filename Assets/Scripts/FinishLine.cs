using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene("WinningScene");
        }
    }


}
