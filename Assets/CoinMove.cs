using UnityEngine;

public class CoinMove : MonoBehaviour
{
    public float moveSpeed = 5f; // سرعة حركة العملات
    public float deadZone = -50f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}