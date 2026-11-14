using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static GunData;

public class GunController : MonoBehaviour
{
    [Header("Assignments")]
    [SerializeField]
    private Playercontroller playerController;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform gunPoint;
    [SerializeField]
    private GameObject shootSFX;

    [Header("Aim Control")]
    [SerializeField]
    private float distance;

    [Header("Gun Settings")]
    public int gunInt;

    public List<GunSettings> guns = new List<GunSettings>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("GunController Loaded Succesfully in: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        // aim
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = player.position + (Vector3)(direction * distance);

        // shoot
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(shootSFX, transform.position, Quaternion.Euler(0f, 0f, 90f));

            GameObject newBullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            newBullet.GetComponent<Bullet>().SetGunController(this);
            // Recoil
            playerController.ApplyRecoil(-direction * guns[gunInt].gunRecoil);
        }
    }
}
