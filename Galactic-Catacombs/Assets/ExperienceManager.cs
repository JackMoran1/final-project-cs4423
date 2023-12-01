using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public float xpValue = 10f; 
    public AudioClip pickupSound;

    private AudioSource audioSource;
    private float lifetime = 10f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.GainXP(xpValue);
            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, pickupSound.length);
        }
    }
}
}
