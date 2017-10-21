using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public static Vector3 pos;
	// Use this for initialization
	void Start () {
		
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
}
