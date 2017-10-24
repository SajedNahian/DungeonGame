using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private int hp;

    //[SerializeField]
    private Transform target;//set target from inspector instead of looking in Update
    [SerializeField]
    private float speed;
    int x;
    int y;
    private bool gettingKnockbacked = false;
    Renderer renderer;



    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        renderer = GetComponent<Renderer>();
    }

    //void Update()
    //{

    //    //rotate to look at the player
    //    transform.LookAt(target.position);
    //    transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
    //    //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);


    //    //move towards the player
    //    if (Vector3.Distance(transform.position, target.position) > 1f)
    //    {//move if distance from target is greater than 1
    //        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    //    }

    //}

    private void FixedUpdate()
    {
        enemyMovement();
        enemyCheckDeath();
    }

    void enemyCheckDeath ()
    {
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void enemyMovement ()
    {
        if (!gettingKnockbacked)
        {
            if ((target.position.x - transform.position.x) < -.2)
            {
                x = -1;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if ((target.position.x - transform.position.x) > .2)
            {
                x = 1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                x = 0;
            }

            if ((target.position.y - transform.position.y) < -.2)
            {
                y = -1;
            }
            else if ((target.position.y - transform.position.y) > .2)
            {
                y = 1;
            }
            else
            {
                y = 0;
            }
            transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            hp -= 1;
            renderer.material.color = Color.red;
            StartCoroutine(returnColor(.5f));
            gettingKnockbacked = true;
            StartCoroutine(KnockBack(0.2f, Player.pos - transform.position));
        } else if (collision.tag == "Player")
        {
            //print("HUMAN!!");
            collision.GetComponent<Player>().hitPlayer(.2f, transform.position - Player.pos);
        }
    }

    IEnumerator KnockBack (float knockDur, Vector3 knockBackDir)
    {
        float timer = 0;
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            print("adding force");
            GetComponent<Rigidbody2D>().AddForce(new Vector3(knockBackDir.x * -20, knockBackDir.y * -20, 0));
        }
        StartCoroutine(returnKnockback(.5f));
        //renderer.material.color = Color.white;
        yield return 0;
    }

    IEnumerator returnColor(float time)
    {
        yield return new WaitForSeconds(time);

        renderer.material.color = Color.white;
    }

    IEnumerator returnKnockback(float time)
    {
        yield return new WaitForSeconds(time);

        gettingKnockbacked = false;
    }
}
