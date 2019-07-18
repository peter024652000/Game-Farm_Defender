using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    //configuration parameters

    [Header("Ghost")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float padding = 0.35f;
    Player player;

    float xMin, xMax;
    //float yMin, yMax;


    // Use this for initialization
    void Start()
    {
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        //var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);  //for not only x movement
        transform.position = new Vector2(newXpos, transform.position.y);

    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        //yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        //yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

}
