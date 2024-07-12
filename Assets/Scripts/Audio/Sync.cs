using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;
    public float repeatInterval = 2f; // Tekrarlama aral��� (�rne�in her 2 saniyede bir)

    void Start()
    {
        // AudioSource'a ses dosyas�n� ve loop �zelli�ini ayarla
        audioSource.clip = soundClip;
        audioSource.loop = true;

        // Sesin tekrarlanmas�n� sa�lamak i�in bir coroutine kullanabilirsiniz
        StartCoroutine(RepeatSound());
    }

    IEnumerator RepeatSound()
    {
        while (true)
        {
            // Ses dosyas�n� ba�lat
            audioSource.Play();

            // Belirli bir s�re bekleyip tekrar ba�lat
            yield return new WaitForSeconds(repeatInterval);
        }
    }
}
