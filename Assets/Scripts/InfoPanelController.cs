using UnityEngine;
using TMPro;

public class InfoPanelController : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;

    [TextArea(3, 10)]
    public string monumentName = "Ram Mandir";

    [TextArea(3, 10)]
    public string monumentInfo = "The Ram Mandir is a Hindu temple located in Ayodhya, Uttar Pradesh, India. It is built at the site believed to be the birthplace of Lord Rama.";

    void Start()
    {
        infoPanel.SetActive(false);
    }

    public void ToggleInfoPanel()
    {
        bool isActive = infoPanel.activeSelf;
        infoPanel.SetActive(!isActive);

        if (!isActive)
        {
            infoText.text = $"<b>{monumentName}</b>\n\n{monumentInfo}";
        }
    }
}