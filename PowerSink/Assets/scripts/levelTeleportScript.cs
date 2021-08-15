using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTeleportScript : MonoBehaviour
{
    public string levelToLoad;
    public bool isLevelLoading = false;

    public void OnTriggerEnter(Collider col)
    {
        if (LayerMask.NameToLayer("Player") == col.gameObject.layer)
        {
            LevelLoad();
        }
    }

    public void LevelLoad()
    {
        if (levelToLoad == "levelOne" && !isLevelLoading)
        {
            GameManager._instance.LoadLevelOne();
            isLevelLoading = true;
        }
            
        if (levelToLoad == "levelTwo" && !isLevelLoading)
        {
            GameManager._instance.LoadLevelTwo();
            isLevelLoading = true;
        }
            
        if (levelToLoad == "levelThree" && !isLevelLoading)
        {
            GameManager._instance.LoadLevelThree();
            isLevelLoading = true;
        }

        if (levelToLoad == "levelFour" && !isLevelLoading)
        {
            GameManager._instance.LoadLevelFour();
            isLevelLoading = true;
        }

        if (levelToLoad == "levelFive" && !isLevelLoading)
        {
            GameManager._instance.LoadLevelFive();
            isLevelLoading = true;
        }

        if (levelToLoad == "levelSix" && !isLevelLoading)
        {
            GameManager._instance.LoadLevelSix();
            isLevelLoading = true;
        }
    }
}
