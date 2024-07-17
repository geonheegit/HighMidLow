using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note_dropper : MonoBehaviour
{
    public TextAsset TXTFile; //텍스트파일 가져오기
    public int start_measure;
    public int start_beat; // 1/16 note
    private game_manager gm;
    private GameObject notesBox;

    [SerializeField] GameObject dropPos1;
    [SerializeField] GameObject dropPos2;
    [SerializeField] GameObject dropPos3;
    [SerializeField] GameObject dropPos4;
    [SerializeField] GameObject highNote;
    [SerializeField] GameObject middleNote;
    [SerializeField] GameObject lowNote;
    [SerializeField] GameObject judgeLine;
    [SerializeField] GameObject dropLine;
    public float scroll_speed;

    public IEnumerator DropNotes()
    {
        gm = GameObject.FindWithTag("gm").GetComponent<game_manager>();
        notesBox = GameObject.FindWithTag("notesBox");

        int i = 0;
        string[] lines = TXTFile.text.Split('\n');

        float distance = dropLine.transform.position.y - judgeLine.transform.position.y;
        
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
            else{
                if (line[0] != 'J'){
                    // 2번째 줄부터
                    // Format: [Line#, NoteCategory(H/M/L), Measure, Beat(1/16)]
                    string[] words = line.Split(" ");

                    int lineNum = System.Convert.ToInt32(words[0]);
                    string noteCate = words[1];
                    int measure = System.Convert.ToInt32(words[2]);
                    int beat = System.Convert.ToInt32(words[3]);

                    // distance, beatDiff, scroll_speed
                    // fixedupdate: 0.02sec / cycle
                    // transform.position.y - (note_Dropper.scroll_speed / 100)
                    // speed = [note_Dropper.scroll_speed / 100 * 50] (unit/sec)
                    // dropline to judgeline estimated time = distance / speed
                    // unit/beat = unit/sec * sec/beat: speed * gm.secPerBeat -> 1/4박자마다 이동하는 거리
                    // speed * gm.secPerBeat / 4 -> 1/16박자마다 이동하는 거리
                    // beatDiff * speed * gm.secPerBeat / 4 -> beatDiff만큼 이동하는 거리
                    // start_measure * 16 + start_beat동안 이동하는 거리: (start_measure * 16 + start_beat) * speed * gm.secPerBeat / 4
                    // + 상수 조절

                    // TODO: 1/32 박자 인식

                    
                    float speed = scroll_speed / 100 * 50;
                    float estimatedTime = distance / speed;

                    float beatDiff = (measure * 16 + beat) - (start_measure * 16 + start_beat); // time | 
                    float adjusted_yPos = (beatDiff * speed * (float)gm.secPerBeat / 4) + ((start_measure * 16 + start_beat) * speed * (float)gm.secPerBeat / 4) - 10;
                    Debug.Log(adjusted_yPos);

                    Vector3 adjusted_dropPos1 = new Vector3(dropPos1.transform.position.x, dropPos1.transform.position.y + adjusted_yPos, 0);
                    Vector3 adjusted_dropPos2 = new Vector3(dropPos2.transform.position.x, dropPos2.transform.position.y + adjusted_yPos, 0);
                    Vector3 adjusted_dropPos3 = new Vector3(dropPos3.transform.position.x, dropPos3.transform.position.y + adjusted_yPos, 0);
                    Vector3 adjusted_dropPos4 = new Vector3(dropPos4.transform.position.x, dropPos4.transform.position.y + adjusted_yPos, 0);

                    // Debug.Log(words[0]);
                    // Debug.Log(words[2]);
                    // Debug.Log(words[3]);

                    if (gm.playMet){
                        if (noteCate == "H"){
                            GameObject note = Instantiate(highNote, notesBox.transform);

                            if (lineNum == 1){
                                note.transform.position = adjusted_dropPos1;
                            }
                            else if (lineNum == 2){
                                note.transform.position = adjusted_dropPos2;
                            }
                            else if (lineNum == 3){
                                note.transform.position = adjusted_dropPos3;
                            }
                            else if (lineNum == 4){
                                note.transform.position = adjusted_dropPos4;
                            }
                            
                        }
                        else if(noteCate == "M"){
                            GameObject note = Instantiate(middleNote, notesBox.transform);

                            if (lineNum == 1){
                                note.transform.position = adjusted_dropPos1;
                            }
                            else if (lineNum == 2){
                                note.transform.position = adjusted_dropPos2;
                            }
                            else if (lineNum == 3){
                                note.transform.position = adjusted_dropPos3;
                            }
                            else if (lineNum == 4){
                                note.transform.position = adjusted_dropPos4;
                            }
                        }
                        else if (noteCate == "L"){
                            GameObject note = Instantiate(lowNote, notesBox.transform);

                            if (lineNum == 1){
                                note.transform.position = adjusted_dropPos1;
                            }
                            else if (lineNum == 2){
                                note.transform.position = adjusted_dropPos2;
                            }
                            else if (lineNum == 3){
                                note.transform.position = adjusted_dropPos3;
                            }
                            else if (lineNum == 4){
                                note.transform.position = adjusted_dropPos4;
                            }
                        }
                    }
                }

            }

            i++;
        }
        yield return null;
    }

    void Update()
    {

    }
}
