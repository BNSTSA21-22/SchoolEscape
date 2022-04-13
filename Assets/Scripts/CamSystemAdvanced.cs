using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSystemAdvanced : MonoBehaviour
{
    private GameObject Player;
    private GameObject Enemy;
    private Vector3 tempPosition;

    public string tagName = "Player";
    public string enemyTagName = "Enemy";

    private Vector3 _cam;

    public float CamMoveSpeed = 5f;
    public int SwapDistance = 8;

    [SerializeField]
    private float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {
        _cam = transform.position;

        Player = GameObject.FindWithTag(tagName);
        Enemy = GameObject.FindWithTag(enemyTagName);
    }

    private float dist;

    //update stuff

    void LateUpdate() {
        dist = Vector3.Distance(Player.transform.position, Enemy.transform.position);

        if(dist < 13) {
            MidPointSys();
        } else {
            OldSys();
        }
    }

    //For closer combat, find the midpoint.

    void MidPointSys() {
        tempPosition.x = (Player.transform.position.x + Enemy.transform.position.x)/2;

        if(tempPosition.x > maxX) {
            tempPosition.x = maxX;
        } else if(tempPosition.x < minX) {
            tempPosition.x = minX;
        }

        tempPosition.z = -10;

        transform.position = Vector3.Lerp(transform.position, tempPosition, CamMoveSpeed * Time.deltaTime);
    }

    //For further distances, focus on the player.

    void OldSys() {
        tempPosition = transform.position;
        tempPosition.x = Player.transform.position.x;

        float x = tempPosition.x;
        float y = tempPosition.y;
        float z = tempPosition.z;

        if(x < minX) {
            x = minX;
        } else if(x > maxX) {
            x = maxX;
        }

        _cam = new Vector3(x, y, z);

        transform.position = Vector3.Lerp(transform.position, _cam, CamMoveSpeed * Time.deltaTime);
    }
}
