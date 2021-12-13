using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Text octaveText;
    public enum InputMode
    {
        Realistic, Direct
    }

    public InputMode mode = InputMode.Realistic;

    Piano piano;
    public List<KeyCode> keys;
    bool firstOctave = true;
    
    void ProcessKeyPress()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            piano.CycleOctave();
            octaveText.text = "Current Octave: " + piano.GetOctave().ToString();
        }


        // if(Input.GetKeyDown(KeyCode.S)) 

        if(Input.GetKeyDown(KeyCode.CapsLock))
        {
            piano.ResetKeysPos();
            firstOctave = !firstOctave;
        }   
        
        if(Input.GetKeyDown(KeyCode.S))
            piano.Play(firstOctave? Piano.Note.C : Piano.Note.C2);
        if(Input.GetKeyDown(KeyCode.E))
            piano.Play(firstOctave? Piano.Note.Cs : Piano.Note.Cs2);
        if(Input.GetKeyDown(KeyCode.D))
            piano.Play(firstOctave? Piano.Note.D : Piano.Note.D2);
        if(Input.GetKeyDown(KeyCode.R))
            piano.Play(firstOctave? Piano.Note.Ds : Piano.Note.Ds2);
        if(Input.GetKeyDown(KeyCode.F))
            piano.Play(firstOctave? Piano.Note.E : Piano.Note.E2);
        if(Input.GetKeyDown(KeyCode.J))
            piano.Play(firstOctave? Piano.Note.F : Piano.Note.F2);
        if(Input.GetKeyDown(KeyCode.I))
            piano.Play(firstOctave? Piano.Note.Fs : Piano.Note.Fs2);
        if(Input.GetKeyDown(KeyCode.K))
            piano.Play(firstOctave? Piano.Note.G : Piano.Note.G2);
        if(Input.GetKeyDown(KeyCode.O))
            piano.Play(firstOctave? Piano.Note.Gs : Piano.Note.Gs2);
        if(Input.GetKeyDown(KeyCode.L))
            piano.Play(firstOctave? Piano.Note.A : Piano.Note.A2);
        if(Input.GetKeyDown(KeyCode.P))
            piano.Play(firstOctave? Piano.Note.As : Piano.Note.As2);
        if(Input.GetKeyDown(KeyCode.Semicolon))
            piano.Play(firstOctave? Piano.Note.B : Piano.Note.B2);

        if(Input.GetKeyUp(KeyCode.S))
            piano.Stop(firstOctave? Piano.Note.C : Piano.Note.C2);
        if(Input.GetKeyUp(KeyCode.E))
            piano.Stop(firstOctave? Piano.Note.Cs : Piano.Note.Cs2);
        if(Input.GetKeyUp(KeyCode.D))
            piano.Stop(firstOctave? Piano.Note.D : Piano.Note.D2);
        if(Input.GetKeyUp(KeyCode.R))
            piano.Stop(firstOctave? Piano.Note.Ds : Piano.Note.Ds2);
        if(Input.GetKeyUp(KeyCode.F))
            piano.Stop(firstOctave? Piano.Note.E : Piano.Note.E2);
        if(Input.GetKeyUp(KeyCode.J))
            piano.Stop(firstOctave? Piano.Note.F : Piano.Note.F2);
        if(Input.GetKeyUp(KeyCode.I))
            piano.Stop(firstOctave? Piano.Note.Fs : Piano.Note.Fs2);
        if(Input.GetKeyUp(KeyCode.K))
            piano.Stop(firstOctave? Piano.Note.G : Piano.Note.G2);
        if(Input.GetKeyUp(KeyCode.O))
            piano.Stop(firstOctave? Piano.Note.Gs : Piano.Note.Gs2);
        if(Input.GetKeyUp(KeyCode.L))
            piano.Stop(firstOctave? Piano.Note.A : Piano.Note.A2);
        if(Input.GetKeyUp(KeyCode.P))
            piano.Stop(firstOctave? Piano.Note.As : Piano.Note.As2);
        if(Input.GetKeyUp(KeyCode.Semicolon))
            piano.Stop(firstOctave? Piano.Note.B : Piano.Note.B2);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessKeyPress();
    }

    void Awake()
    {
        piano = FindObjectOfType<Piano>();
        octaveText.text = "Current Octave: " + piano.GetOctave().ToString();
        Debug.Assert(piano != null);
    }
}
