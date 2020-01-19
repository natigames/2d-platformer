using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; // to focus on the player
    public Transform farBackground, middleBackground; // for Parallax effect

    public float minHeight, maxHeight;// look at scene to determine offscreens
     
    //private float lastXPos;
    private Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        //lastXPos = transform.position.x;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // follow target on X, clamped on Y, same on Z)
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y,minHeight,maxHeight), transform.position.z);

        // calculate the amount to move camera layer accordingly
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        // make water move at "same" speed as player/camera (follow on X)
        farBackground.position +=new Vector3(amountToMove.x, amountToMove.y, 0f);

        // make Bushes move at half the speed
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;

        //update position
        lastPos = transform.position;

    }
}
