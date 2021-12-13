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

        if(Input.GetKeyDown(KeyCode.CapsLock)) // choose octave
            firstOctave = !firstOctave;


        if (firstOctave)
        {
            if(Input.GetKeyDown(KeyCode.S))
                piano.Play(Piano.Note.C);
            if(Input.GetKeyDown(KeyCode.E))
                piano.Play(Piano.Note.Cs);
            if(Input.GetKeyDown(KeyCode.D))
                piano.Play(Piano.Note.D);
            if(Input.GetKeyDown(KeyCode.R))
                piano.Play(Piano.Note.Ds);
            if(Input.GetKeyDown(KeyCode.F))
                piano.Play(Piano.Note.E);
            if(Input.GetKeyDown(KeyCode.J))
                piano.Play(Piano.Note.F);
            if(Input.GetKeyDown(KeyCode.I))
                piano.Play(Piano.Note.Fs);
            if(Input.GetKeyDown(KeyCode.K))
                piano.Play(Piano.Note.G);
            if(Input.GetKeyDown(KeyCode.O))
                piano.Play(Piano.Note.Gs);
            if(Input.GetKeyDown(KeyCode.L))
                piano.Play(Piano.Note.A);
            if(Input.GetKeyDown(KeyCode.P))
                piano.Play(Piano.Note.As);
            if(Input.GetKeyDown(KeyCode.Semicolon))
                piano.Play(Piano.Note.B);

            if(Input.GetKeyUp(KeyCode.S))
                piano.Stop(Piano.Note.C);
            if(Input.GetKeyUp(KeyCode.E))
                piano.Stop(Piano.Note.Cs);
            if(Input.GetKeyUp(KeyCode.D))
                piano.Stop(Piano.Note.D);
            if(Input.GetKeyUp(KeyCode.R))
                piano.Stop(Piano.Note.Ds);
            if(Input.GetKeyUp(KeyCode.F))
                piano.Stop(Piano.Note.E);
            if(Input.GetKeyUp(KeyCode.J))
                piano.Stop(Piano.Note.F);
            if(Input.GetKeyUp(KeyCode.I))
                piano.Stop(Piano.Note.Fs);
            if(Input.GetKeyUp(KeyCode.K))
                piano.Stop(Piano.Note.G);
            if(Input.GetKeyUp(KeyCode.O))
                piano.Stop(Piano.Note.Gs);
            if(Input.GetKeyUp(KeyCode.L))
                piano.Stop(Piano.Note.A);
            if(Input.GetKeyUp(KeyCode.P))
                piano.Stop(Piano.Note.As);
            if(Input.GetKeyUp(KeyCode.Semicolon))
                piano.Stop(Piano.Note.B);
        }
        else
        {
            
            if(Input.GetKeyDown(KeyCode.S))
                piano.Play(Piano.Note.C2);
            if(Input.GetKeyDown(KeyCode.E))
                piano.Play(Piano.Note.Cs2);
            if(Input.GetKeyDown(KeyCode.D))
                piano.Play(Piano.Note.D2);
            if(Input.GetKeyDown(KeyCode.R))
                piano.Play(Piano.Note.Ds2);
            if(Input.GetKeyDown(KeyCode.F))
                piano.Play(Piano.Note.E2);
            if(Input.GetKeyDown(KeyCode.J))
                piano.Play(Piano.Note.F2);
            if(Input.GetKeyDown(KeyCode.I))
                piano.Play(Piano.Note.Fs2);
            if(Input.GetKeyDown(KeyCode.K))
                piano.Play(Piano.Note.G2);
            if(Input.GetKeyDown(KeyCode.O))
                piano.Play(Piano.Note.Gs2);
            if(Input.GetKeyDown(KeyCode.L))
                piano.Play(Piano.Note.A2);
            if(Input.GetKeyDown(KeyCode.P))
                piano.Play(Piano.Note.As2);
            if(Input.GetKeyDown(KeyCode.Semicolon))
                piano.Play(Piano.Note.B2);

            if(Input.GetKeyDown(KeyCode.S))
                piano.Stop(Piano.Note.C2);
            if(Input.GetKeyDown(KeyCode.E))
                piano.Stop(Piano.Note.Cs2);
            if(Input.GetKeyDown(KeyCode.D))
                piano.Stop(Piano.Note.D2);
            if(Input.GetKeyDown(KeyCode.R))
                piano.Stop(Piano.Note.Ds2);
            if(Input.GetKeyDown(KeyCode.F))
                piano.Stop(Piano.Note.E2);
            if(Input.GetKeyDown(KeyCode.J))
                piano.Stop(Piano.Note.F2);
            if(Input.GetKeyDown(KeyCode.I))
                piano.Stop(Piano.Note.Fs2);
            if(Input.GetKeyDown(KeyCode.K))
                piano.Stop(Piano.Note.G2);
            if(Input.GetKeyDown(KeyCode.O))
                piano.Stop(Piano.Note.Gs2);
            if(Input.GetKeyDown(KeyCode.L))
                piano.Stop(Piano.Note.A2);
            if(Input.GetKeyDown(KeyCode.P))
                piano.Stop(Piano.Note.As2);
            if(Input.GetKeyDown(KeyCode.Semicolon))
                piano.Stop(Piano.Note.B2);
        }


        
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
