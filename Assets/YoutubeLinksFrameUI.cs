using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MovingJigsaw;

public class YoutubeLinksFrameUI : MonoBehaviour
{

    public List<YoutubeLink> youtubeLinks;
    public GameObject linkObjectPrefab;
    public Transform contentLayoutObject;

    public TMP_InputField inputField;

    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < youtubeLinks.Count; i++) 
        {

            GameObject g = Instantiate(linkObjectPrefab, contentLayoutObject);
            g.GetComponent<YouTubeLinkButton>().assignedlink = youtubeLinks[i];
            g.GetComponent<YouTubeLinkButton>().SetUpButton();


        }
    }

    public void CopyPasteLink(YoutubeLink link) 
    {
        inputField.text = link.youtubeURL;
        FindObjectOfType<VideoPathScript>().UpdatePath();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
