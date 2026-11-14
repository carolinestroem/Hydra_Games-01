using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GunController gunController;

    private void Start()
    {
        StartCoroutine(DeleteBulletAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(gunController.guns[gunController.gunInt].bulletSpeed * Time.deltaTime, 0, 0);
    }

    public void SetGunController(GunController controller)
    {
        gunController = controller;

        Debug.Log("Bullet received GunController: " + gunController);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(1, transform.position);
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DeleteBulletAfterTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
