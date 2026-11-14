using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playercontroller : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    [Header("Controller")]
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    private float moveX;
    private float moveY;

    [Header("Claw")]
    [SerializeField]
    private GameObject attackHitbox;
    [SerializeField]
    private float attackHitbixDistance;
    [SerializeField]
    private GameObject clawAim;
    [SerializeField]
    private float clawAimDistance;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float attackWaitTime;
    private bool canAttack = true;

    [Header("Gun")]
    [SerializeField]
    private GameObject gun;
    private bool hasGun = false;
    private bool isGunActive = false;
    private Vector2 recoilVelocity;

    [Header("Health And Damage")]
    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    [SerializeField]
    private float playerImmortalityTime;
    private float currentPlayerImmortalityTime;
    private bool isImmortal = false;
    [SerializeField]
    private float explosionKnockback;
    private Vector2 knockbackVelocity;
    [SerializeField]
    private float knockbackDecay;

    [Header("Collectables")]
    [SerializeField]
    private int gunPartsNeeded;
    private int gunPartsCollected = 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();

        clawAim.transform.rotation = Quaternion.Euler(0, 0, angle);
        clawAim.transform.position = transform.position + (Vector3)(direction * clawAimDistance);

        if (Input.GetMouseButtonDown(0) && canAttack && !isGunActive)
        {
            attackHitbox.transform.position = transform.position;
            attackHitbox.transform.rotation = Quaternion.identity;

            attackHitbox.transform.rotation = Quaternion.Euler(0, 0, angle);
            attackHitbox.transform.position = attackHitbox.transform.position + (Vector3)(direction * attackHitbixDistance);

            StartCoroutine(Attack());
        }

        if (hasGun)
        {
            if (Mouse.current.scroll.ReadValue().y > 0 || Mouse.current.scroll.ReadValue().y < 0)
            {
                gun.SetActive(!gun.activeSelf);
                clawAim.SetActive(!gun.activeSelf);

                if (gun.activeSelf) isGunActive = true;
                else isGunActive = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 movementVelocity = new Vector2(moveX, moveY).normalized * speed;
        recoilVelocity = Vector2.Lerp(recoilVelocity, Vector2.zero, 5f * Time.deltaTime);

        rb.linearVelocity = movementVelocity + knockbackVelocity + recoilVelocity;

        knockbackVelocity = Vector2.MoveTowards(knockbackVelocity, Vector2.zero, knockbackDecay * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GunPart"))
        {
            Destroy(collision.gameObject);
            gunPartsCollected++;

            if (gunPartsCollected >= gunPartsNeeded)
            {
                hasGun = true;
            }
        }
    }

    public void ApplyRecoil(Vector2 force)
    {
        recoilVelocity += force;
    }

    public void DamageByExplosion(Vector2 enemyPos)
    {
        if (isImmortal) return;
        isImmortal = true;
        currentHealth -= 1;

        Vector2 knockbackDirection = ((Vector2)transform.position - enemyPos).normalized;
        knockbackVelocity = knockbackDirection * explosionKnockback;
        StartCoroutine(ImmortalityTimer());
    }

    private IEnumerator ImmortalityTimer()
    {
        currentPlayerImmortalityTime = playerImmortalityTime;
        sr.color = Color.black;

        while (currentPlayerImmortalityTime > 0)
        {
            currentPlayerImmortalityTime -= Time.deltaTime;

            float t = 1 - (currentPlayerImmortalityTime / playerImmortalityTime);

            Color color = Color.Lerp(Color.black, Color.white, t);
            color.a = sr.color.a;

            sr.color = color;

            yield return null;
        }

        sr.color = new Color(1, 1, 1, sr.color.a);
        isImmortal = false;
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        attackHitbox.SetActive(true);

        yield return new WaitForSeconds(attackTime);
        attackHitbox.SetActive(false);

        yield return new WaitForSeconds(attackWaitTime);
        canAttack = true;
    }
}
