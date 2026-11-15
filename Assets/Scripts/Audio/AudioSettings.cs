using System.Collections;
using UnityEngine;

public class DestroyAfterAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private bool activePitchRandomizer;
    [SerializeField]
    private float lowPitch;
    [SerializeField]
    private float highPitch;

    void Start()
    {
        audioSource.pitch = activePitchRandomizer ? Random.Range(lowPitch, highPitch) : 1f;
        audioSource.Play();
        StartCoroutine(DestroyWhenAudioEnds());
    }

    IEnumerator DestroyWhenAudioEnds()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
