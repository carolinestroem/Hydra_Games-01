using UnityEngine;

public class Claw : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Claw collided with: " + collision.gameObject.name);
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage, transform.position);
        }
    }
}
