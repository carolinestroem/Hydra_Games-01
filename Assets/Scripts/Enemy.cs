using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed;

    [Header("Detection Settings")]
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float detectionRange;

    private bool playerDetected = false;
    private bool playerOutOfSight = false;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, (playerTransform.position - transform.position).normalized * detectionRange, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerTransform.position - transform.position).normalized, detectionRange, ~LayerMask.GetMask("Enemy"));

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            playerDetected = true;
        }
        else
        {
            Debug.Log("Player not detected.");
        }

        if (playerDetected)
        {
            Vector2 playerPos = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
            playerPos.Normalize();

            rb.linearVelocity = new Vector2(playerPos.x, playerPos.y) * moveSpeed;
        }
    }
}
