using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    [System.Serializable]
    public struct KeyNotes
    {
        public Key key;
        public AudioClip Lo, Mid, Hi;
    }

    [Tooltip("Place in order of \n C, C#, D, D#, E, F, F#, G, G#, A, A#, B")]
    public KeyNotes[] keyNotes;

    public enum Octave
    {
        Low, Mid, High
    }
    Octave octave = Octave.Low;

    public enum Note
    {
        C, Cs, D, Ds, E, F, Fs, G, Gs, A, As, B
    }

    public AudioSource audioSource;

    public Octave GetOctave()
    {   
        return octave;
    }

    public void CycleOctave()
    {
        if(octave == Octave.High)
        {
            octave = Octave.Low;
        }
        else
        {
            octave++;
        }
        foreach(KeyNotes k in keyNotes)
        {
            k.key.HighLightOctave(octave);
        }
    }

    // Start is called before the first frame update
    public void Play(char note)
    {
        int index = char.ToLower(note) - 'c';
        keyNotes[index].key.Play(octave, audioSource);
    }

    public void Play(Note note)
    {
        keyNotes[(int)note].key.Play(octave, audioSource);
    }

    void Awake()
    {
        foreach(KeyNotes k in keyNotes)
        {
            k.key.SetAudioClip(k.Hi, k.Mid, k.Lo);
        }
    }
}
