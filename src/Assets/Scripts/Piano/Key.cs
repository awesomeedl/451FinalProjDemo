using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Hi, Mid, Lo;

    NodePrimitive HiNode, MidNode, LoNode;

    public AudioClip HiNote, MidNote, LoNote;

    public bool isBlackKey;

    Color originalColor;
    Color selectColor;

    public GameObject noteEffect;

    public void SetAudioClip(AudioClip hi, AudioClip mid, AudioClip lo)
    {
        HiNote = hi;
        MidNote = mid;
        LoNote = lo;
    }

    public void Play(Piano.Octave octave, AudioSource audioSource)
    {
        switch(octave)
        {
            case Piano.Octave.Low:
                OnKeyDown(Lo); 
                audioSource.PlayOneShot(LoNote);
                break;
            case Piano.Octave.Mid:
                OnKeyDown(Mid); 
                audioSource.PlayOneShot(MidNote);
                break;
            case Piano.Octave.High:
                OnKeyDown(Hi); 
                audioSource.PlayOneShot(HiNote);
                break;
        }
    }

    public void Stop(Piano.Octave octave)
    {
        switch(octave)
        {
            case Piano.Octave.Low:
                OnKeyUp(Lo); 
                break;
            case Piano.Octave.Mid:
                OnKeyUp(Mid); 
                break;
            case Piano.Octave.High:
                OnKeyUp(Hi); 
                break;
        }
    }

    public void HighLightOctave(Piano.Octave octave)
    {
        switch(octave)
        {
            case Piano.Octave.Low:
                LoNode.MyColor = originalColor;
                MidNode.MyColor = originalColor;
                HiNode.MyColor = originalColor;
                break;
            case Piano.Octave.Mid:
                LoNode.MyColor = Color.grey;
                MidNode.MyColor = originalColor;
                HiNode.MyColor = originalColor;
                break;
            case Piano.Octave.High:
                LoNode.MyColor = Color.grey;
                MidNode.MyColor = Color.grey;
                HiNode.MyColor = originalColor;
                break;
        }
    }

    public void OnKeyDown(GameObject keyJoint)
    {
        noteEffect.SetActive(true);
        keyJoint.transform.rotation = Quaternion.AngleAxis(30f, Vector3.left);
        foreach(var g in keyJoint.GetComponentsInChildren<NodePrimitive>())
        {
            g.MyColor = selectColor;
        }
    }

    public void OnKeyUp(GameObject keyJoint)
    {
        noteEffect.SetActive(false);
        keyJoint.transform.rotation = Quaternion.identity;
        foreach(var g in keyJoint.GetComponentsInChildren<NodePrimitive>())
        {
            g.MyColor = originalColor;
        }
    }

    // IEnumerator KeyDepress(GameObject keyJoint)
    // {
    //     Quaternion original = keyJoint.transform.rotation;
    //     Quaternion target = Quaternion.AngleAxis(30f, Vector3.right);
    //     float timer = 0f;
    //     float animateTime = 0.05f;

    //     while (timer < animateTime)
    //     {
    //         keyJoint.transform.rotation = Quaternion.Lerp(original, target, easeOut(timer / animateTime));
    //         timer += Time.deltaTime;
    //         yield return null;
    //     }
        
    //     target = original;
    //     original = keyJoint.transform.rotation;

    //     timer = 0f; 

    //     while (timer < animateTime)
    //     {
    //         keyJoint.transform.rotation = Quaternion.Lerp(original, target, easeIn(timer / animateTime));
    //         timer += Time.deltaTime;
    //         yield return null;
    //     }

    //     keyJoint.transform.rotation = target;
    // }

    void Awake()
    {
        selectColor = new Color(0, 144, 255);
        originalColor = isBlackKey ? Color.black : Color.white;
        LoNode = Lo.GetComponent<SceneNode>().PrimitiveList[0];
        MidNode = Mid.GetComponent<SceneNode>().PrimitiveList[0];
        HiNode = Hi.GetComponent<SceneNode>().PrimitiveList[0];
    }


    float easeOut(float f)
    {
        return 1f - Mathf.Pow(1f - f, 3);
    }

    float easeIn(float f)
    {
        return f * f * f;
    }
}
