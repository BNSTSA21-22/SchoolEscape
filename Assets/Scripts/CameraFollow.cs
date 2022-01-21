using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPosition;
    // Start is called before the first frame update

    [SerializeField]
    private float minX, maxX;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tempPosition = transform.position;
        tempPosition.x = player.position.x;
        if (tempPosition.x < minX)
        { tempPosition.x = minX; }
        else if (tempPosition.x > maxX) { tempPosition.x = maxX; }
        transform.position = tempPosition;
    }
}
