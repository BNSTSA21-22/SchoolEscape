using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelection : MonoBehaviour
{
    //this is the variable stuff
    public GameObject[] playerObjects;
    public int selectedCharacter = 0;

    public string gameScene = "Cafeteria";

    private string selectedCharacterDataName = "Selected Character";

    //some cool voids
    private void HideAllCharacters()
    {
        foreach (GameObject g in playerObjects)
        {
            g.SetActive(false);
        }
    }

    public void ChangeChar(int num)
    {
        playerObjects[selectedCharacter].SetActive(false);
        selectedCharacter = selectedCharacter + num;
        if (selectedCharacter < 0)
        {
            selectedCharacter = playerObjects.Length-1;
        }

        if (selectedCharacter >= playerObjects.Length)
        {
            selectedCharacter = 0;
        }

        playerObjects[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
        SceneManager.LoadScene(gameScene);
    }

    //startup sequence
    void Start()
    {

        HideAllCharacters();

        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);

        playerObjects[selectedCharacter].SetActive(true);
    }

    void Update()
    {
        bool RightDown = Input.GetKeyDown(KeyCode.RightArrow);
        bool LeftDown = Input.GetKeyDown(KeyCode.LeftArrow);
        bool SpaceDown = Input.GetKeyDown(KeyCode.Space);

        if (RightDown)
        {
            ChangeChar(1);
        } else if (LeftDown)
        {
            ChangeChar(-1);
        } else if (SpaceDown)
        {
            StartGame();
        }
    }
}