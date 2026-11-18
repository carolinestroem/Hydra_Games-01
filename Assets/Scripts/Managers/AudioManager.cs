using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public float volumeSFX;

    [SerializeField]
    private List<AudioSource> listSFX;

    private void Update()
    {
        foreach (AudioSource source in listSFX)
        {
            source.volume = volumeSFX;
        }
    }
}
