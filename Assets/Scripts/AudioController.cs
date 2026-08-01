using UnityEngine;
using TMPro;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip narrationClip;
    public TextMeshProUGUI buttonText;   // the Play Button's TMP text

    void Update()
    {
        // Detect natural finish (time resets to 0 only when clip ends, not on pause)
        if (audioSource != null && !audioSource.isPlaying && audioSource.time == 0 && buttonText.text == "Pause")
            buttonText.text = "Play";
    }

    public void TogglePlayPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            buttonText.text = "Play";
        }
        else
        {
            if (audioSource.clip != narrationClip)
                audioSource.clip = narrationClip;
            audioSource.Play();
            buttonText.text = "Pause";
        }
    }

    public void StopAudio()
    {
        audioSource.Stop();
        if (buttonText != null) buttonText.text = "Play";
    }
}