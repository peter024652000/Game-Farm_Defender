using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShredder : MonoBehaviour {


    SceneLoader sceneloader;

    private void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if(collision.tag== "EnemyProjectile" || collision.tag == "PlayerProjectile")
        {
            Destroy(collision.gameObject);
        }

        else if ((collision.tag =="Player"|| collision.tag =="Ghost")&& tag == "LeftWall")
        {
            StartCoroutine(sceneloader.QuitGame());
        }

        else if ((collision.tag == "Player" || collision.tag == "Ghost") && tag == "RightWall")
        {
            StartCoroutine(sceneloader.LoadGameScene());
        }

    }

}
