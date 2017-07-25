using UnityEngine;
using System.Collections;
using System;
public class JumpAI : MonoBehaviour
{

    public float forceY = 300f;
    public Player player;
    private Rigidbody2D myBody;
    private Animator anim;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(Attack());
    }


    void Update()
    {
        if (Math.Abs(player.transform.position.x - this.transform.position.x) < 100)
        {
            myBody.AddForce(new Vector2(-4, 0));
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 7));

        forceY = UnityEngine.Random.Range(250, 550);

        myBody.AddForce(new Vector2(0, forceY));
        anim.SetBool("Attack", true);

        yield return new WaitForSeconds(.7f);
        StartCoroutine(Attack());

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Ground")
        {
            //     anim.SetBool("Attack", false);
        }

        if (target.tag == "Player")
        {
            //  Destroy(target.gameObject);
        }
    }

} // SpiderJumper
