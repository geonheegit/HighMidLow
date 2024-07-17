using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_manager : MonoBehaviour
{
    public bool playMet = false; // note_dropper.cs
    private int stackedBeatCount;
    private int stacked4BeatCount;
    private int stacked8BeatCount;
    private int currentBeatCount;
    private int current8BeatCount;
    public int current16BeatCount; // note_dropper.cs
    public int measure; // note_dropper.cs

    private note_dropper note_dropper;

    [SerializeField] Text stackedBeatCountText;
    [SerializeField] Text currentBeatCountText;
    [SerializeField] Text current8BeatCountText;
    [SerializeField] Text current16BeatCountText;
    [SerializeField] Text measureCountText;
    [SerializeField] GameObject metronomePrefab;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float bpm;
    [SerializeField] float offsetSeconds;


    public double dspSongTime;
    public double songPosition;
    public double secPerBeat;
    public double initialDsptime;

    void Start()
    {
        note_dropper = gameObject.GetComponent<note_dropper>();

        stackedBeatCount = 0;
        stacked4BeatCount = 0;
        stacked8BeatCount = 0;
        currentBeatCount = 0;
        current8BeatCount = 0;
        current16BeatCount = 0;
        measure = 0;

        secPerBeat = 60f / bpm;
        initialDsptime = 0f;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)){
            playMet = true;
            stackedBeatCount = 0;
            stacked4BeatCount = 0;
            stacked8BeatCount = 0;
            currentBeatCount = 0;
            current8BeatCount = 0;
            current16BeatCount = 0;
            measure = 0;

            // new logic
            dspSongTime = (float)AudioSettings.dspTime;
            initialDsptime = (float)(AudioSettings.dspTime - dspSongTime);
            
            audioSource.Play();

            StartCoroutine(note_dropper.DropNotes());
        }
        else if (Input.GetKeyDown(KeyCode.N)){
            playMet = false;
            audioSource.Stop();
            audioSource.time = 0;
        }

        if (playMet){
            if (songPosition >= secPerBeat * stackedBeatCount / 4 + initialDsptime){ // 1/16 박자

                stackedBeatCount ++;
                current16BeatCount ++;

            }
            if (songPosition >= secPerBeat * stacked8BeatCount / 2 + initialDsptime){ // 1/8 박자
                stacked8BeatCount ++;
                current8BeatCount ++;
                
            }
            if (songPosition >= secPerBeat * stacked4BeatCount + initialDsptime){ // 1/4 박자
                stacked4BeatCount ++;
                currentBeatCount ++;

                Instantiate(metronomePrefab);
                
            } 

            songPosition = (float)(AudioSettings.dspTime - dspSongTime - offsetSeconds);
        }

        if(currentBeatCount == 5){
            currentBeatCount = 1;
            measure ++;
        }
        if (current8BeatCount == 9){
            current8BeatCount = 1;
        }
        if (current16BeatCount == 17){
            current16BeatCount = 1;
        }

        

        stackedBeatCountText.text = "Total Beat: " + stackedBeatCount;
        currentBeatCountText.text = "Current Beat (1/4) : " + currentBeatCount;
        current8BeatCountText.text = "Current Beat (1/8) : " + current8BeatCount;
        current16BeatCountText.text = "Current Beat (1/16) : " + current16BeatCount;
        measureCountText.text = "Measures: " + measure;
    }
}
