using UnityEngine;
using TMPro;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip narrationClip;
    public TextMeshProUGUI buttonText;

    private void Update()
    {
        // Don't try to read time unless we have a valid AudioClip.
        if (audioSource == null || buttonText == null)
            return;

        if (audioSource.clip == null)
            return;

        // Detect when narration naturally finishes.
        if (!audioSource.isPlaying &&
            audioSource.time >= audioSource.clip.length &&
            buttonText.text == "Pause")
        {
            buttonText.text = "Play";
        }
    }

    public void TogglePlayPause()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioController: AudioSource is not assigned.");
            return;
        }

        if (buttonText == null)
        {
            Debug.LogWarning("AudioController: Button Text is not assigned.");
            return;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            buttonText.text = "Play";
        }
        else
        {
            if (narrationClip == null)
            {
                Debug.LogWarning(
                    "AudioController: Narration Clip is not assigned."
                );
                return;
            }

            if (audioSource.clip != narrationClip)
            {
                audioSource.clip = narrationClip;
            }

            audioSource.Play();
            buttonText.text = "Pause";
        }
    }

    public void StopAudio()
    {
        if (audioSource == null)
            return;

        audioSource.Stop();

        if (buttonText != null)
            buttonText.text = "Play";
    }
}