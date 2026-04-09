using UnityEngine;

public class MiddlePipe : MonoBehaviour
{
    public LogicS logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicS>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player (assuming the player has the tag "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            logic.AddPipeScore(1); // Add 1 to the pipe score
        }
    }
}