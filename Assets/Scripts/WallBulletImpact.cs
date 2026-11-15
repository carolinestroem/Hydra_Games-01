using UnityEngine;

public class WallBulletImpact : MonoBehaviour
{
    [SerializeField]
    private GameObject wallImpactEffect;
    public void WallHit(Vector2 bulletPos, Vector2 bulletDirection)
    {
        float angle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;

        Instantiate(wallImpactEffect, bulletPos, Quaternion.Euler(0, 0, angle + 90));
    }
}
