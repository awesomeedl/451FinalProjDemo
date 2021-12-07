using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(GetComponent<UnityEngine.UI.Dropdown>().value - 1);
    }
}
