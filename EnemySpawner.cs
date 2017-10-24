using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private Transform tlSpawner, trSpawner, blSpawner, brSpawner;
    [SerializeField]
    private GameObject enemy1;
    float nextWaveTime;
    float waveCountdownTimer;
	// Use this for initialization
	void Start () {
        waveCountdownTimer = 1f;
        nextWaveTime = Time.time + waveCountdownTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextWaveTime)
        {
            nextWave();
        }
	}

    void nextWave ()
    {
        Vector3 tempLeft = transform.position;
        tempLeft.y = Random.Range(blSpawner.transform.position.y, tlSpawner.transform.position.y);
        tempLeft.x = blSpawner.transform.position.x;
        Instantiate(enemy1, tempLeft, transform.rotation);


        Vector3 tempRight = transform.position;
        tempRight.y = Random.Range(brSpawner.transform.position.y, trSpawner.transform.position.y);
        tempRight.x = brSpawner.transform.position.x;
        Instantiate(enemy1, tempRight, transform.rotation);

        nextWaveTime = Time.time + 5f;
    }
}
