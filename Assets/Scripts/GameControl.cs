using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public string menuScene = "Character Selection";
    private string selectedCharacterDataName = "Selected Character";
    int selectedCharacter;
    public GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName,0);
        //playerObject = Instantiate(characters[selectedCharacter],PlayerStartPosition.position,characters[selectedCharacter].transform.rotation);    
    }
}