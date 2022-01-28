using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private Animator anim;

    private string selectedCharacterDataName = "Selected Character";

    private string victoryAnim = "Victory";

    int selectedCharacter;

    private GameObject playerObject;
    public Transform PlayerStartPosition;

    public GameObject[] characters;

    void Awake()
    {
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);
        playerObject = Instantiate(characters[selectedCharacter],PlayerStartPosition.position, characters[selectedCharacter].transform.rotation);

        anim = playerObject.GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool(victoryAnim, true);
    }
}
