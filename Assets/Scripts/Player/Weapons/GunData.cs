using UnityEngine;

public class GunData : MonoBehaviour
{
    [System.Serializable]
    public class GunSettings
    {
        public string gunName;
        public float gunDamage;
        public float gunRecoil;
        public float gunWaitTime;
        public float gunReloadTime;

        public float bulletSpeed;
    }
}
