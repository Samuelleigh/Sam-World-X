using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerControl : MonoBehaviour
{

    public int moveSpeed = 100;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 7f);
    }

    // Update is called once per frame
    void Update()
    {


    }

}
