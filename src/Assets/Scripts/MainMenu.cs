using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // public string SongName;
    // List<string> selected_paths;
    // void Start()
    // {
        // selected_paths = new List<string>();
        // selected_paths.Add("midi_0"); // 0
    // }
    public void PlayGame()
    {
        // SceneManager.LoadScene(SceneManage.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }   


    // public void SetLevel0()
    // {
    //     GameObject gObject = GameObject.Find("FallingCubes"); 
    //     GetMidiInstructions midi_inst = gObject.GetComponenet<Transform>();
    //     ///string selected_path = "Assets/Resources/all_star_midi_data.txt";
    //     midi_inst.path = selected_paths[0];
    // }
}
