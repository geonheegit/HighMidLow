using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note_dropper : MonoBehaviour
{
    public TextAsset TXTFile; //텍스트파일 가져오기
    public int start_measure;
    public int start_beat;

    void Start()
    {
        int i = 0;
        string[] lines = TXTFile.text.Split('\n');
        
        foreach (string line in lines){
            if (i == 0){ // notedata 첫번째 줄은 start_measure, start_beat를 담고 있음.
                string[] words = line.Split(" ");

                int j = 0;
                foreach (string word in words){
                    if (j == 0){
                        start_measure = int.Parse(word.Split("_")[1]);
                    }
                    else if (j == 1){
                        start_beat = int.Parse(word.Split("_")[1]);
                    }

                    j++;
                }
            }

            // 2번째 줄부터
            

            i++;
        }
    }


    void Update()
    {

    }
}
