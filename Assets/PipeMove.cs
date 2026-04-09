using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public float MoveSpeed = 5; // سرعة حركة الأنابيب
    public float deadZone = -50;
    public int phase;

    void Update()
    {
        transform.position += Vector3.left * MoveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone || GameSwitcher.Instance.currentPhase != phase)
        {
            Debug.Log("Pipe Deleted");
            Destroy(gameObject);
        }
    }
}