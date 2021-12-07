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

    void ProcessKeyPress()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            piano.CycleOctave();
            octaveText.text = "Current Octave: " + piano.GetOctave().ToString();
        }

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
