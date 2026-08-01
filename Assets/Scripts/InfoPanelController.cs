using UnityEngine;
using TMPro;

public class InfoPanelController : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public GameObject playButton;   // NEW - only visible when panel is open

    [TextArea(3, 10)] public string monumentName = "Ram Mandir";
    [TextArea(3, 10)] public string monumentInfo = "The Ram Mandir is a Hindu temple located in Ayodhya...";

    void Start()
    {
        infoPanel.SetActive(false);
        if (playButton != null) playButton.SetActive(false);
    }

    public void ToggleInfoPanel()
    {
        bool isActive = infoPanel.activeSelf;
        infoPanel.SetActive(!isActive);
        if (playButton != null) playButton.SetActive(!isActive);

        if (!isActive)
            infoText.text = $"<b>{monumentName}</b>\n\n{monumentInfo}";
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
        if (playButton != null) playButton.SetActive(false);
    }
}