using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    [Tooltip("Place in order of \n C, C#, D, D#, E, F, F#, G, G#, A, A#, B")]

    public Key[] keys;

    public int numKeys;

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
        foreach(Key k in keys)
        {
            k.HighLightOctave(octave);
        }
    }

    // Start is called before the first frame update
    public void Play(char note)
    {
        int index = char.ToLower(note) - 'c';
        keys[index].Play(octave, audioSource);
    }

    public void Play(Note note)
    {
        keys[(int)note].Play(octave, audioSource);
    }

    public void Stop(Note note)
    {
        keys[(int)note].Stop(octave);
    }

    void Awake()
    {
        keys = new Key[24];
        for(int i = 0; i < numKeys; i++)
        {
            keys[i] = transform.GetChild(i).GetComponent<Key>();
                        keys[i].gameObject.GetComponent<SceneNode>().NodeOrigin.x = i;
            int m = i / 8;
            AudioClip Hi = Resources.Load<AudioClip>("piano-mp3/" + keys[i].name + (3 + m * 3));
            AudioClip Mid = Resources.Load<AudioClip>("piano-mp3/" + keys[i].name + (2 + m * 3));
            AudioClip Lo = Resources.Load<AudioClip>("piano-mp3/" + keys[i].name + (1 + m * 3));
            keys[i].SetAudioClip(Hi, Mid, Lo);
        }
    }
}
