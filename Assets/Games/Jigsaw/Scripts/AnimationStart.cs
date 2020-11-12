using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovingJigsaw
{
    public class AnimationStart : MonoBehaviour
    {
        public Animator ani;
        public string starttrigger;
        private void Awake()
        {
            ani = gameObject.GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {
            ani.SetTrigger(starttrigger);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
