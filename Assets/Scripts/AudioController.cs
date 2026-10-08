using UnityEngine;
using TMPro;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip narrationClip;
    public TextMeshProUGUI buttonText;   // the Play Button's TMP text

    void Update()
    {
        // Only check time once a clip actually exists, to avoid the
        // "resource that is not a clip" warning before first Play().
        if (audioSource != null && audioSource.clip != null &&
            !audioSource.isPlaying && audioSource.time == 0f &&
            buttonText.text == "Pause")
        {
            buttonText.text = "Play";
        }
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