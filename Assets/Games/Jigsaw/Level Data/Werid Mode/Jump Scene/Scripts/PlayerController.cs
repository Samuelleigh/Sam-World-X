using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D ridgidbody;
    private Animator animator;
    public bool onGround;
    public int jumpForce;

    public GameLogicJump gm;


    private void Awake()
    {
        ridgidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gm = FindObjectOfType<GameLogicJump>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown("space") && onGround) 
        {
            Debug.Log("hit space");
            ridgidbody.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            animator.SetTrigger("jump");
            onGround = false;

        }



    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

    //   if (collision.gameObject.CompareTag("ground"))
    //   {
    //       Debug.Log("collision");         
    //       onGround = true;
    //   };
    //
    //
    //
    //   if (collision.gameObject.CompareTag("enemy"))
    //   {
    //       Destroy(collision.gameObject);
    //       Destroy(gameObject);
    //
    //   }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("collision");
            onGround = true;
            animator.SetTrigger("land");
        };



        if (collision.gameObject.CompareTag("enemy"))
        {
            gm.ResetGame();
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }



}
