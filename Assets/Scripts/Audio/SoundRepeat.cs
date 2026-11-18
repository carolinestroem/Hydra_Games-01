using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundRepeat : MonoBehaviour
{
    [SerializeField]
    private float lowPitch;
    [SerializeField]
    private float highPitch;
    [SerializeField]
    private Playercontroller playercontroller;
    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private GameObject cookieMonsterScream;
    private bool hasScreamed = false;

    // Update is called once per frame
    void Update()
    {
        if (playercontroller != null)
        {
            if (playercontroller.isMoving)
            {
                if (sound.isPlaying) return;
                sound.pitch = Random.Range(lowPitch, highPitch);
                sound.Play();
            }
        }

        else if (enemy != null)
        {
            if (enemy.isPursuing && !enemy.isAttacking)
            {
                if (sound.isPlaying) return;
                sound.pitch = Random.Range(lowPitch, highPitch);
                sound.Play();
            }
            else if (enemy.isAttacking && !hasScreamed)
            {
                sound.Stop();
                hasScreamed = true;
                Instantiate(cookieMonsterScream, transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
        else
        {
            Debug.LogError("No script was assigned to SoundRepeat in: " + gameObject.name);
        }
    }
}