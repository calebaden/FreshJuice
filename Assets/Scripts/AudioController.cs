using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSource playerSource;

    [SerializeField]
    private AudioClip[] soundFX;

	// Use this for initialization
	private void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        playerSource = PlayerScript.Instance.GetComponent<AudioSource>();
        CallJuice(GameController.Instance.isJuicy);
	}
	
	// Update is called once per frame
	private void Update ()
    {
        if (audioSource.enabled)
        {
            audioSource.volume = PlayerScript.Instance.GetVelocity().magnitude / 6 + 0.5f;
            audioSource.pitch = PlayerScript.Instance.GetVelocity().magnitude / 10 + 0.5f;
        }
    }

    public void PlaySingle (int index, float volume)
    {
        playerSource.PlayOneShot(soundFX[index], volume / 5);
    }

    public void CallJuice (bool isJuicy)
    {
        if (isJuicy)
        {
            audioSource.enabled = true;
        }
        else
        {
            audioSource.enabled = false;
        }
    }
}
