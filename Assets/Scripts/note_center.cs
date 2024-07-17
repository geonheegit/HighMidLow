using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note_center : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.name == "JudgeLine"){
            audioSource.Play();
        }
    }
}
