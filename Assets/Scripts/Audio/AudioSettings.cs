
using System.Collections;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private bool activePitchRandomizer;

    [SerializeField]
    private bool deleteAudio;

    [SerializeField]
    private float lowPitch;

    [SerializeField]
    private float highPitch;

    private void Start()
    {
        audioSource.pitch = activePitchRandomizer ? Random.Range(lowPitch, highPitch) : 1f;

        audioSource.Play();

        if (deleteAudio)
        {
            StartCoroutine(DestroyWhenAudioEnds());
        }
    }

    private IEnumerator DestroyWhenAudioEnds()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    public void DeleteAudio()
    {
        audioSource.Stop();
        Destroy(gameObject);
    }
}
