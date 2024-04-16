using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject canvasVictory; 
    public string tagObjective = "objective";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagObjective))
        {
            canvasVictory.SetActive(true);
            Debug.Log("You win!");
        }
    }
}