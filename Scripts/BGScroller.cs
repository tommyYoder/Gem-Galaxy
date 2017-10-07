using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float scrollSpeed;
    public float tileSizeY;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;                                         // Looks for starting position of game object.
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);      // Moves game object in the Y direction and then will reset the position to the new world space position.
        transform.position = startPosition + Vector3.up * newPosition;
    }
}
