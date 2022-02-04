using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPosition;

    private Vector3 _cam;

    public float CamMoveSpeed = 5f;
    
    public string tagName = "Player";
    // Start is called before the first frame update

    [SerializeField]
    private float minX, maxX;

    void Start()
    {
        _cam = transform.position;

        player = GameObject.FindWithTag(tagName).transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tempPosition = transform.position;
        tempPosition.x = player.position.x;

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
