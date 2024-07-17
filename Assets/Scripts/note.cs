using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note : MonoBehaviour
{
    private note_dropper note_Dropper;

    void Start()
    {
        note_Dropper = GameObject.FindWithTag("gm").GetComponent<note_dropper>();
    }


    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (note_Dropper.scroll_speed / 100), 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "JudgeLine"){
            
        }
    }
}
