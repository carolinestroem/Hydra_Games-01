using System.Collections;
using UnityEngine;

public class DestroyAfterParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;

    void Start()
    {
        particle.Play();
        StartCoroutine(DestroyWhenParticleEnds());
    }

    IEnumerator DestroyWhenParticleEnds()
    {
        while (particle.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
