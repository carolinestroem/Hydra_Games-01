using System.Collections;
using Unity.Android.Gradle;
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
    [SerializeField]
    private float attackRange;

    [Header("Attack Settings")]
    [SerializeField]
    private float explodeRange;
    [SerializeField]
    private float timeBeforeAttack;

    private bool isAtacking = false;
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

        if (playerDetected && !isAtacking)
        {
            Vector2 playerPos = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
            float distanceToPlayer = playerPos.magnitude;
            Debug.Log("Distance to player: " + distanceToPlayer);

            if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(Attack());
            }
            playerPos.Normalize();
            rb.linearVelocity = new Vector2(playerPos.x, playerPos.y) * moveSpeed;
            if (isAtacking) rb.linearVelocity = Vector2.zero;
        }
    }

    private IEnumerator Attack()
    {
        isAtacking = true;
        yield return new WaitForSeconds(timeBeforeAttack);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRange);
    }
}
