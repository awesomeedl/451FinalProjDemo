using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;
using System.Globalization;


public class GetMidiInstructions : MonoBehaviour
{
    public Transform topBar;
    public Transform bottomBar;
    public List<Transform> fallingCubes;
    public List<List<(int, double, double)>> instrument_notes_lst;
    float speed = 3f;
    public AudioSource audioSource;
    public AudioClip music;


    // Start is called before the first frame update
    void Start()
    {
        // Read Txt File
        string path = "Assets/Resources/all_star_midi_data.txt";
        instrument_notes_lst = new List<List<(int, double, double)>>();
        instrument_notes_lst = readTextFile(path);

        // Color Bars
        topBar.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        bottomBar.GetComponent<Renderer>().material.SetColor("_Color", Color.black);

        // Falling Cubes
        int instrument = 0;
        fallingCubes = new List<Transform>();

        float timeOffset = getTimeOffset(instrument);
        Debug.Log("timeOffset : "+timeOffset);
        StartCoroutine(playMusicAtTime(timeOffset));


        // for (int i=0; i<12; i=i+2){
        //     // StartCoroutine(makeCubeAtTime(i, 24+12+i)); //
        //     float seconds = 1;
        //     StartCoroutine(makeCubeAtTime(i, 24+12, seconds, 1));
        //     StartCoroutine(makeCubeAtTime(i+1, 24+12, seconds, 2));
        //     StartCoroutine(makeCubeAtTime(i+1, 24+12+1, seconds, 2));
        //     StartCoroutine(makeCubeAtTime(i, 24+12+2, seconds, 1));

        // }

        putCubesToMusic(instrument);


    }

    // Update is called once per frame
    void Update()
    {
        fallingCubeMovement();
    }


    void fallingCubeMovement()
    {
        //float speed = 2f;
        for (int i = 0; i < fallingCubes.Count; i++)
        {
            // Destroy
            float cubeTop = fallingCubes[i].position.y + (fallingCubes[i].localScale.y/2f);
            if (fallingCubes[i].gameObject != null && cubeTop < bottomBar.position.y)
            {
                Destroy(fallingCubes[i].gameObject);
                fallingCubes.Remove(fallingCubes[i]);
            }
            // Move
            else
            {
                fallingCubes[i].Translate(-Vector3.up * Time.deltaTime * speed);
            }
        }
    }

    IEnumerator makeCubeAtTime(double time, int note, float dur, int color_num)
    {
        yield return new WaitForSeconds((float)time);
        makeCubeAtPos(note, dur, color_num);
    }

    void makeCubeAtPos(int note, float dur, int color_num){
        // Plan : across bar, a note cube can fall in one of the 12 keys. 
        // I can subtract 21 from a note, and then do modulo 12 to find remainder. 21-21 % 12 = 0, 22-21 % 12 = 

        float barLength = topBar.localScale.x - 1f;
        float min = 21f;
        float max = 108f;

        float posInRange = ((((float)note-24f) % 12f) / 11f) /*-(1f/24f)*/ ; // /12 // ((float)note-min)/(max-min); //  note 30 : 9/87
        int octave = ((note-24) / 12); // determine color, 
        // Debug.Log(note);
        int baseOctave = 2;

        float cubeScale = 1f/(12) * barLength;
        float cubeHeight = dur*speed ; //note dur*speed  //fallingCube.transform.localScale.y/4f;


        Vector3 leftMostPos = topBar.position - (barLength/2f)*(topBar.right);
        Vector3 posOnBar = (posInRange*barLength*topBar.right);
        Vector3 spawnHeightOffset = topBar.up*cubeHeight/2f;
        Vector3 spawnPos = leftMostPos + posOnBar + spawnHeightOffset;
        
        GameObject fallingCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fallingCube.transform.SetParent (transform);

        if (octave == baseOctave)
        {
            fallingCube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else if (octave == baseOctave+1)
        {
            fallingCube.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
        else if (octave == baseOctave+2)
        {
            fallingCube.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }


        // if (color_num == 1)
        // {
        //     fallingCube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        // }
        // else if (color_num == 2)
        // {
        //     fallingCube.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        // }
        
        fallingCube.transform.localScale = new Vector3(cubeScale, cubeHeight, cubeScale);
        fallingCube.transform.position =  spawnPos;

        fallingCubes.Add(fallingCube.transform); //
    }

    void putCubesToMusic(int instrument_num) // readTextFile() needs to be called before this
    {
        for (int j = 0; j < instrument_notes_lst[instrument_num].Count; j++)
        {
            int note = (instrument_notes_lst[instrument_num][j].Item1);
            double start = (instrument_notes_lst[instrument_num][j].Item2);
            double end = (instrument_notes_lst[instrument_num][j].Item3);
            float dur = (float)end-(float)start;

            // Debug.Log("start : " + start);

            StartCoroutine(makeCubeAtTime(start, note, dur, 1)); // time (seconds), note
        }
    }


    float getTimeOffset(int instrument_num) // readTextFile() needs to be called before this
    {
        double start = instrument_notes_lst[instrument_num][0].Item2; // first note
        float startTime = (float)start;
        float fallTime = (topBar.position.y - bottomBar.position.y) / speed; 

        Debug.Log("distance : " + (topBar.position.y - bottomBar.position.y));
        Debug.Log("speed : " + speed);
        Debug.Log("startTime : " + startTime);
        
        float timeOffset = fallTime; // + startTime/speed;

        return timeOffset;
    }

    IEnumerator playMusicAtTime(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("GOT HERE");
        audioSource.PlayOneShot(music);
    }



    List<List<(int, double, double)>> readTextFile(string file_path)
    {
        // Debuging Tips:
        // use format like this to check string cleaning Debug.Log("-" + variable + "-");

        // -- Initialize Variables
        double midi_duration = 0.0;
        List<int> instrument_prog_lst = new List<int>();
        var instrument_notes_lst = new List<List<(int, double, double)>>();
        int instrument_index = -1;
    
        // // -- Read/Clean file
        // StreamReader inp_stm = new StreamReader(file_path);

        // WEBGL Specific way of reading a file
        StringReader inp_stm = new StringReader(Resources.Load<TextAsset>("midi").ToString());

        //while(!inp_stm.EndOfStream)
        while(inp_stm.Peek() > 0)
        {
            string inp_ln = inp_stm.ReadLine( );
            
            // duration is always first
            if(inp_ln.Contains("duration:"))
            {
                string duration_prog = inp_ln.Replace("duration:", "");  
                duration_prog = duration_prog.Replace(" ", "");
                duration_prog = duration_prog.Replace("\r", "").Replace("\n", "").Replace("\t", "");
                
                midi_duration = Convert.ToDouble(duration_prog); // Convert.ToDouble | Double.Parse
            }
            else if(inp_ln.Contains("note      start        end")) 
            {
                continue;
            }
            else if(inp_ln.Contains("instrument:"))
            {
                string instrument_prog = inp_ln.Replace("instrument:", "");
                instrument_prog = instrument_prog.Replace(" ", "");
                instrument_prog_lst.Add(Int32.Parse(instrument_prog));
                
                instrument_index += 1;
                instrument_notes_lst.Add( new List<(int, double, double)> {} );
            }
            else
            {                
                char[] charSeparators = new char[] { ' ' };
                string[] split_line = inp_ln.Split(charSeparators, System.StringSplitOptions.RemoveEmptyEntries);
                
                (int, double, double) note_item = (Int32.Parse(split_line[0]), Double.Parse(split_line[1]), Double.Parse(split_line[2])); // note, start, end
                instrument_notes_lst[instrument_index].Add(note_item);
            }
        }
        inp_stm.Close( );  


        // -- Sort notes by start time
        for (int index = 0; index < instrument_notes_lst.Count; index++)
        {
            instrument_notes_lst[index].Sort((a, b) => a.Item2.CompareTo(b.Item2));
        }
        
        // // Testing
        // for (int i = 0; i < instrument_notes_lst.Count; i++)
        // {
        //     for (int j = 0; j < instrument_notes_lst[i].Count; j++)
        //     {
        //         Debug.Log(instrument_notes_lst[i][j].Item1); // DEBUG
        //         Debug.Log(instrument_notes_lst[i][j].Item2); // DEBUG
        //         Debug.Log(instrument_notes_lst[i][j].Item3); // DEBUG
        //     }
        // }
        // Debug.Log("instrument_prog_lst");
        // for (int k = 0; k < instrument_prog_lst.Count; k++)
        // {
        //     Debug.Log(instrument_prog_lst[k]); // DEBUG
        // }
        
        
        return instrument_notes_lst; // Possible vars to return : instrument_prog_lst, midi_duration
    }

}



// Resources : 
// (didn't impliment) Interfaces as generic types : https://stackoverflow.com/questions/4343336/a-list-of-multiple-data-types
// https://stackoverflow.com/questions/748062/return-multiple-values-to-a-method-caller
// (sort list of tuples) https://riptutorial.com/csharp/example/5033/comparing-and-sorting-tuples
// (read from txt file) https://gamedev.stackexchange.com/questions/85807/how-to-read-a-data-from-text-file-in-unity
// (string to num) https://docs.microsoft.com/en-us/dotnet/api/system.stringsplitoptions?view=net-6.0#System_StringSplitOptions_RemoveEmptyEntries
// (coroutines) https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity