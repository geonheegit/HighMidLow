using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metronome_prefab : MonoBehaviour
{
    private Vector3 spawnPos;
    void Start()
    {
        spawnPos = GameObject.Find("MetronomePrefabSpawn").transform.position;

        transform.position = spawnPos;

        StartCoroutine(DestroySelfSec(0.1f));
    }

    IEnumerator DestroySelfSec(float time){
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
