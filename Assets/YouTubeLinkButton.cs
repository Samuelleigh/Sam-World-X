using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YouTubeLinkButton : MonoBehaviour
{

    public YoutubeLink assignedlink;
    public TextMeshProUGUI titleText;
    public Button button;
    public Button button2;
    public YoutubeLinksFrameUI frameUI;

    public void SetUpButton() 
    {

        frameUI = FindObjectOfType<YoutubeLinksFrameUI>();
        titleText.text = assignedlink.youtubeTitle;

        button.onClick.AddListener(delegate
        {
            frameUI.CopyPasteLink(assignedlink);

        });

        button2.onClick.AddListener(delegate
        {
            frameUI.CopyPasteLink(assignedlink);

        });


    }


}
