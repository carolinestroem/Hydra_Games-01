using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
