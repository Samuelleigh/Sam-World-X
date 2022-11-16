using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoEyeAnimation : MonoBehaviour
{

    public List<string> triggers;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TiggersCorountine());
    }

    public IEnumerator TiggersCorountine() 
    {

        while (true) 
        {

            yield return new WaitForSeconds(Random.Range(20,35));
            string trigger = triggers[Random.Range(0, triggers.Count)];
            animator.SetTrigger(trigger);
        }

    
    }

    public void TriggerAnimation() 
    {


        string trigger = triggers[Random.Range(0, triggers.Count)];

        animator.SetTrigger(trigger);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
