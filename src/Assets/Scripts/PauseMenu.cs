using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public AudioSource audioSource;
    public void BactToMainMenu()
    {
        // SceneManager.LoadScene(SceneManage.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }   


    public void PauseGame ()
    {
        Time.timeScale = 0;
        audioSource.Pause();
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
        audioSource.UnPause();
    }


}

// Resources :
// pause : https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/