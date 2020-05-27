using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeadGuy : MonoBehaviour
{
    public Canvas canvas;
    public int count;
    public List<string> DeadGuyMemories;
    public List<GameObject> textframes;

    public List<GameObject> usedframes;

    public int total;
    public GameObject tutorial;
    public GameObject Deadguy;
    public GameObject lastDude;

    public AudioSource beepsource;

    public bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        total = DeadGuyMemories.Count;
        lastDude.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewText()
    {

        tutorial.SetActive(false);
        beepsource.Play();

        if (count < total)
        {
            int random = Random.Range(0, textframes.Count);
            int random2 = Random.Range(0, DeadGuyMemories.Count);

            GameObject Choosen = textframes[random];

            Choosen.transform.SetAsLastSibling();
            Choosen.SetActive(true);
            Choosen.GetComponent<DeadGuyText_D>().deadText.text = DeadGuyMemories[random2];
            textframes.Remove(Choosen);
            usedframes.Add(Choosen);
            DeadGuyMemories.Remove(DeadGuyMemories[random2]);
            


            count++;
            
        }

        if (count == total)
        {
            Debug.Log("W");
            Deadguy.SetActive(false);
            StartCoroutine(EndTimer());


        }

    }

    public IEnumerator EndTimer()
    {
        end = true;
        Debug.Log("sfefse");

        while (end)
        {

            Debug.Log("endy");

            yield return new WaitForSeconds(4f);
            EndofGame();
            end = false;

        }

    }

    public void EndofGame()
    {
        Debug.Log("OHOHOHOHO");

        foreach (GameObject text in usedframes)
        {
            text.SetActive(false);
        }

        lastDude.SetActive(true);

    }

}
