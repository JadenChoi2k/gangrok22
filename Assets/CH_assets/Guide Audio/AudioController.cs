using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    private int currentIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPrevious()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            PlayClip();
        }
    }

    public void PlayCurrent()
    {
        PlayClip();
    }

    public void PlayNext()
    {
        if (currentIndex < audioClips.Length - 1)
        {
            currentIndex++;
            PlayClip();
        }
    }

    private void PlayClip()
    {
        audioSource.clip = audioClips[currentIndex];
        audioSource.Play();
    }
}
