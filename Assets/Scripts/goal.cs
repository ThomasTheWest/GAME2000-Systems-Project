using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    //public GameObject canvasVictory; 
    public string tagObjective = "objective";

    public int sceneIndex;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagObjective))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}