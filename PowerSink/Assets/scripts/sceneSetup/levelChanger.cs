using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelChanger : MonoBehaviour
{
    public Animator animator;
    private string levelToLoad;
    
    //plays fade out animation and then loads level

    public void FadeToLevel (string LevelName)
    {
        levelToLoad = LevelName;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);

        if(levelToLoad == "mainMenu")
        {
            GameManager._instance.MainMenuLoad();
        }
        else
        {
            GameManager._instance.LevelLoad();
        }

        animator.SetTrigger("FadeIn");
    }


}
