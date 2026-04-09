using UnityEngine;

public class Coin : MonoBehaviour
{
    public LogicS logic;
    private bool isCollected = false;
    public int phase;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicS>();
    }


    void Update()
    {
        if (GameSwitcher.Instance.currentPhase != phase)
        {
            Debug.Log("Coin Deleted");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected && collision.gameObject.CompareTag("Player"))
        {
            isCollected = true;
            logic.AddCoinScore(1);
            Destroy(gameObject);
        }
    }
}