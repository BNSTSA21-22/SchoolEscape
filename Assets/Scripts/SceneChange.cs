using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int delay = 5;
    public string nextLevel;

    void Start()
    {
        StartCoroutine(LoadLevel());  
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(nextLevel);
    }
}
