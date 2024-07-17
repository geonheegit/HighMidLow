using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_manager : MonoBehaviour
{
    private bool playMet = false;
    private double initial_time;
    private int stackedBeatCount;
    private int currentBeatCount;
    private int measure;
    [SerializeField] Text stackedBeatCountText;
    [SerializeField] Text currentBeatCountText;
    [SerializeField] Text measureCountText;
    [SerializeField] GameObject metronomePrefab;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float bpm;
    [SerializeField] float offsetBeats;
    [SerializeField] float offsetSeconds;
    void Start()
    {
        initial_time = Time.time;
        stackedBeatCount = 0;
        currentBeatCount = 0;
        measure = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)){
            playMet = true;
            audioSource.time = 60 / bpm * offsetBeats + offsetSeconds;
            stackedBeatCount = 0;
            currentBeatCount = 0;
            measure = 0;
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.N)){
            playMet = false;
            audioSource.Stop();
            audioSource.time = 0;
        }

        if (playMet){
            if (Time.time - initial_time > 60 / bpm){
                initial_time = Time.time;
                Instantiate(metronomePrefab);
                stackedBeatCount ++;
                currentBeatCount ++;
            }
        }

        if(currentBeatCount == 5){
            currentBeatCount = 1;
            measure ++;
        }


        stackedBeatCountText.text = "Total Beat: " + stackedBeatCount;
        currentBeatCountText.text = "Current Beat: " + currentBeatCount;
        measureCountText.text = "Measures: " + measure;
    }
}
