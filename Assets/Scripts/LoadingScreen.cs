using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public int minTime = 4;

    void Start() {
        Invoke("loadThePlace", minTime);
    }

    void loadThePlace() {
        SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
    }
}
