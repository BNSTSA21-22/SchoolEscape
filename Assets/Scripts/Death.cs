using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private Animator anim;

    private string selectedCharacterDataName = "Selected Character";

    private string deathAnim = "Dead";

    public string menuScene = "Character Selection";

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
        anim.SetBool(deathAnim, true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadingData.sceneToLoad = menuScene;
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}
