using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public int minTime = 4;

    public Image enemy;
    public Sprite[] spritelist;

    private int newind = 1;

    void Start() {
        switch(LoadingData.sceneToLoad) {
            case "Character Selection":
                newind = 0;
                break;

            case "CafeteriaCutscene":
                newind = 0;
                break;
        }

        Debug.Log("Index: " + newind);

        enemy.sprite = spritelist[newind];

        Invoke("loadThePlace", minTime);
    }

    void loadThePlace() {
        SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
    }
}
