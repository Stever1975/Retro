using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyScipt : MonoBehaviour {
    public float force = 500f;
    private Animator anim;


    void Awake()
    {
        Debug.Log("hello");

     //   anim = GetComponent<Animator>();


    }
	// Use this for initialization
	void Start () {
		
	}
	

   
     void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            target.gameObject.GetComponent<Player>().BouncePlayerWithBouncy(force);
        //    StartCoroutine(a)
        }
    }


}
