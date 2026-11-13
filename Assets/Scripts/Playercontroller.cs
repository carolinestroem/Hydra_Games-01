using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;

    [Header("Claw")]
    [SerializeField]
    private GameObject attackHitbox;
    [SerializeField]
    private float attackHitbixDistance;
    [SerializeField]
    private float attackTime;

    private float moveX;
    private float moveY;

    private bool canAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            attackHitbox.transform.position = transform.position;
            attackHitbox.transform.rotation = Quaternion.identity;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = mousePosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            direction.Normalize();

            attackHitbox.transform.rotation = Quaternion.Euler(0, 0, angle);
            attackHitbox.transform.position = attackHitbox.transform.position + (Vector3)(direction * attackHitbixDistance);

            StartCoroutine(Attack());
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX, moveY).normalized * speed;
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        attackHitbox.SetActive(false);
        canAttack = true;
    }
}
