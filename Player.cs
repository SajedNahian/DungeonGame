using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public static Vector3 pos;
    private bool gettingKnockbacked;
    Renderer renderer;
    // Use this for initialization
    void Awake () {
        renderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        pos = transform.position;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float h1 = h * speed * Time.deltaTime;
        float v1 = v * speed * Time.deltaTime;
        if (Mathf.Abs(h) == 1 && Mathf.Abs(v) == 1)
        {
            h1 *= Mathf.Sqrt(.5f);
            v1 *= Mathf.Sqrt(.5f);
        }
        if (h1 < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            RotateAround.speed = Mathf.Abs(RotateAround.speed);
        } else if (h1 > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            RotateAround.speed = -Mathf.Abs(RotateAround.speed);
        }
        Vector3 temp = transform.position;
        temp.x += h1;
        temp.y += v1;
        transform.position = temp;
    }

    public void hitPlayer(float knockDur, Vector3 knockBackDir)
    {
        renderer.material.color = Color.red;
        StartCoroutine(returnColor(.5f));
        gettingKnockbacked = true;
        StartCoroutine(KnockBack(0.2f, knockBackDir));
    }

    private IEnumerator KnockBack(float knockDur, Vector3 knockBackDir)
    {
        float timer = 0;
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            print("adding force");
            GetComponent<Rigidbody2D>().AddForce(new Vector3(knockBackDir.x * -10, knockBackDir.y * -10, 0));
            //GetComponent<Rigidbody2D>().AddForce(new Vector3(100, 100, 0));
        }
        //StartCoroutine(returnKnockback(.5f));
        //renderer.material.color = Color.white;
        yield return 0;
    }

    IEnumerator returnColor(float time)
    {
        yield return new WaitForSeconds(time);

        renderer.material.color = Color.white;
    }
}
