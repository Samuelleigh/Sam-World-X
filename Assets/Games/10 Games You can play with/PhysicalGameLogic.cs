using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PhysicalGameLogic : MonoBehaviour
{

    public PhysicalGameManager manager;
    public UIMaster ui;
    public List<PhysicalGame> activelist;
    public int currentpage;
    [Space]
    public GameObject spread;
    public GameObject single;


    public TextMeshProUGUI title1;
    public TextMeshProUGUI title2;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    [Space]
    public TextMeshProUGUI single1;
    public TextMeshProUGUI single2;
    public TextMeshProUGUI title3;
    public TextMeshProUGUI text3;
    public GameObject sing;
    public GameObject te;
    public GameObject videoFrame;
    public VideoPlayer Video;
    [Space]
    public GameObject imageobject;
    public Image image;



    public 

    void Awake()
    {
        ui = FindObjectOfType<UIMaster>();
        manager = FindObjectOfType<PhysicalGameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BackToMainMenu() 
    {
        SceneManager.LoadScene("Main Menu");

    }


    public void LoadGame(string ID) 
    {

        if (ID == "pinecone") 
        {
            activelist = manager.pinecone;
            
        }
        if (ID == "card") 
        {
            activelist = manager.bussinessCard;
        }

        if (ID == "smartphone")
        {
            activelist = manager.Phone;
        }

        if (ID == "book") 
        {
            activelist = manager.Book;
        }

        if (ID == "vine") 
        {
            activelist = manager.RIPVineCompliation;
        }

        currentpage = -1;
        ui.NextLayer();
        NextPage();
    
    }

    public void NextPage() 
    {

        if (currentpage != activelist.Count - 1)
        {

            currentpage++;

            if (activelist[currentpage].isSpread == true)
            {
                LoadSpread(activelist[currentpage]);
            }
            else
            {
                LoadSingle(activelist[currentpage]);
            }
        }
    
    
    }

    public void PreviousPage()
    {
        if (currentpage != 0)
        {
            currentpage--;

            if (activelist[currentpage].isSpread == true)
            {
                LoadSpread(activelist[currentpage]);
            }
            else
            {
                LoadSingle(activelist[currentpage]);
            }
        }


    }


    public void LoadSpread(PhysicalGame page) 
    {


        spread.SetActive(true);
        single.SetActive(false);

      
        

        title1.text = page.title1;
        title2.text = page.title2;
        text1.text = page.writing1;
        text2.text = page.writing2;
    
    }



    public void LoadSingle(PhysicalGame page) 
    {

        single.SetActive(true);
        spread.SetActive(false);


        imageobject.SetActive(false);

        if (page.SingleGame) 
        {
            te.SetActive(true) ;
            sing.SetActive(false);

            title3.text = page.title1;
            text3.text = page.writing1;
        
        
        }
        else 
        {

            te.SetActive(false);
            sing.SetActive(true);

            single1.text = page.title1;
            single2.text = page.writing1;


        }
        if (page.sprite != null) 
        {

            imageobject.SetActive(true);
            image.sprite = page.sprite;
        
        
        }

        if (page.title1 == "null") 
        {
            single.SetActive(false);
        
        }

        if (page.videoClip != null)
        {
            videoFrame.SetActive(true);
            Video.clip = page.videoClip;

        }
        else 
        {
            videoFrame.SetActive(false);
        
        }


    }


    public void Loadtwitter() 
    {
    
    
    
    
    }

    public void Backhome() 
    {
        ui.PreviousLayer();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
