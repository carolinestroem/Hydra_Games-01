
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Playercontroller playerController;

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

    private bool playerDetected = false;
    private bool isAttacking = false;

    [Header("Attack Settings")]
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float explodeRange;
    [SerializeField]
    private float timeBeforeAttack;

    [Header("Health And Damage")]
    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    [SerializeField]
    private float damageKnockback;
    [SerializeField]
    private float knockbackDecay;

    private Vector2 knockbackVelocity;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        Debug.DrawRay(transform.position, directionToPlayer * detectionRange, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, ~LayerMask.GetMask("Enemy"));

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            playerDetected = true;
        }

        if (playerDetected && !isAttacking)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 movementVelocity = Vector2.zero;

        if (playerDetected && !isAttacking)
        {
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
            movementVelocity = directionToPlayer * moveSpeed;
        }

        rb.linearVelocity = movementVelocity + knockbackVelocity;

        knockbackVelocity = Vector2.MoveTowards(knockbackVelocity, Vector2.zero, knockbackDecay * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage, Vector2 playerPos)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 knockbackDirection = ((Vector2)transform.position - playerPos).normalized;
        knockbackVelocity = knockbackDirection * damageKnockback;
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(timeBeforeAttack);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerTransform.position - transform.position).normalized, explodeRange, ~LayerMask.GetMask("Enemy"));

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            playerController.DamageByExplosion(transform.position);
        }

        yield return new WaitForSeconds(0.1f);

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRange);
    }
}
