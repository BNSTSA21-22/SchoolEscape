using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public int delay = 5;
    public string nextLevel;

    [SerializeField]
    private Slider healthBar;

    void Start()
    {
        if (healthBar != null) {
            healthBar.gameObject.SetActive(false);
        }

        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(nextLevel);
    }
}
