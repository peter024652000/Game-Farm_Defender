using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject EnemyProjectilePrefab;
    [SerializeField] GameObject ExplosionVFX;
    [SerializeField] float projectileSpeed = 3f;
    [SerializeField] GameObject[] Parts;
    [SerializeField] Sprite[] PartsSprite;
    [SerializeField] AudioClip enemySound;

    //Cache Reference
    Rigidbody2D myRigidBody2D;
    SpriteRenderer mySpriteRenderer;
    EnemyNumIndicator enemyNumIndicator;
    AudioSource enemyAudiosource;

    // Use this for initialization
    void Start () {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyNumIndicator = FindObjectOfType<EnemyNumIndicator>();
        enemyAudiosource = GetComponent<AudioSource>();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            if (!EnemyProjectilePrefab) { return; }
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(EnemyProjectilePrefab, new Vector2 (transform.position.x,transform.position.y-0.4f), Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) // not for all collision; want a gameobject.(which will be the bullet with DamageDealer.cs)
    {
        if(tag=="Parts")
        {
            if(other.tag!="Projectile"&&other.tag!="Player"&&other.tag!="Ghost")
            {
                Bounce(other.tag);
            }

        }

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>(); // store the gameobject which is bumped to enemy (usually it is bullet)in the damageDealer.
        if (!damageDealer) { return; }
        StartCoroutine(ProcessHit(damageDealer));

    }


    private IEnumerator ProcessHit(DamageDealer damageDealer)
    {
        enemyAudiosource.PlayOneShot(enemySound, 1.5f);
        health -= damageDealer.GetDamage();
        if (damageDealer.tag == "PlayerProjectile")
        {
            damageDealer.Hit();
        }

        if (health <= 0)
        {

            Die();

        }

        for (int time = 0;time < 1; time++)
        {
            mySpriteRenderer.color = new Color(1, 1, 1, 0.3f);
            yield return new WaitForSeconds(0.05f);
            mySpriteRenderer.color = new Color(1, 1, 1, 1f);
            yield return new WaitForSeconds(0.05f);
        }



    }

    private void Die()
    {

        AudioSource.PlayClipAtPoint(enemySound,Camera.main.transform.position,0.2f);//not good enough to play the sound if not in 2D
        GameObject explosion = Instantiate(ExplosionVFX, transform.position, transform.rotation);
        Destroy(explosion, 0.4f);
        Destroy(gameObject);
        if (enemyNumIndicator == true)//for enemy on start scene
        {
            enemyNumIndicator.EnemyLeftCount(tag);
        }
        ExplosionEffect();
    }

    private void ExplosionEffect()
    {
        if (Parts.Length==0) { return; }

        if (tag == "Splitus")
        {
            GameObject splitusUp = Instantiate(Parts[0], new Vector2(transform.position.x, transform.position.y + 0.5f), transform.rotation);
            GameObject splitusLeft = Instantiate(Parts[1], new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), transform.rotation);
            GameObject splitusRight = Instantiate(Parts[2], new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f), transform.rotation);
            splitusUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            splitusLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, -projectileSpeed);
            splitusRight.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, -projectileSpeed);
        }
        if (tag == "Spikus")
        {
            GameObject spikusCenterDown = Instantiate(Parts[0], new Vector2(transform.position.x, transform.position.y-0.2f), transform.rotation);
            GameObject spikusLeftDown = Instantiate(Parts[1], new Vector2(transform.position.x - 0.2f, transform.position.y - 0.1f), transform.rotation);
            GameObject spikusRightDown = Instantiate(Parts[2], new Vector2(transform.position.x + 0.2f, transform.position.y - 0.1f), transform.rotation);
            GameObject spikusCenterUp = Instantiate(Parts[3], new Vector2(transform.position.x , transform.position.y + 0.2f), transform.rotation);
            GameObject spikusLeft = Instantiate(Parts[4], new Vector2(transform.position.x + -0.2f, transform.position.y ), transform.rotation);
            GameObject spikusRight = Instantiate(Parts[5], new Vector2(transform.position.x + 0.2f, transform.position.y ), transform.rotation);
            GameObject spikusLeftUp = Instantiate(Parts[6], new Vector2(transform.position.x + -0.2f, transform.position.y + 0.1f), transform.rotation);
            GameObject spikusRightUp = Instantiate(Parts[7], new Vector2(transform.position.x + 0.2f, transform.position.y + 0.1f), transform.rotation);

            spikusCenterDown.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
            spikusLeftDown.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed/2, -projectileSpeed);
            spikusRightDown.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed/2, -projectileSpeed);
            spikusCenterUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, +projectileSpeed);
            spikusLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0);
            spikusRight.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
            spikusLeftUp.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed/2, projectileSpeed);
            spikusRightUp.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed/2, projectileSpeed);
        }
    }

    private void Bounce(string walltag)
    {

        if (myRigidBody2D.velocity == new Vector2(0, projectileSpeed))
        {
            mySpriteRenderer.sprite = PartsSprite[1];
            myRigidBody2D.velocity = new Vector2(0, -projectileSpeed);
        }
        else if (myRigidBody2D.velocity == new Vector2(0, -projectileSpeed))
        {
            mySpriteRenderer.sprite = PartsSprite[0];
            myRigidBody2D.velocity = new Vector2(0, projectileSpeed);
        }
        else if (myRigidBody2D.velocity == new Vector2(-projectileSpeed, -projectileSpeed))
        {
            if (walltag == "DownWall")
            {
                mySpriteRenderer.sprite = PartsSprite[1];
                myRigidBody2D.velocity = new Vector2(-projectileSpeed, projectileSpeed);
            }
            else
            {
                mySpriteRenderer.sprite = PartsSprite[3];
                myRigidBody2D.velocity = new Vector2(projectileSpeed, -projectileSpeed);
            }

        }
        else if (myRigidBody2D.velocity == new Vector2(projectileSpeed, -projectileSpeed))
        {
            if (walltag == "DownWall")
            {
                mySpriteRenderer.sprite = PartsSprite[0];
                myRigidBody2D.velocity = new Vector2(projectileSpeed, projectileSpeed);
            }
            else
            {
                mySpriteRenderer.sprite = PartsSprite[2];
                myRigidBody2D.velocity = new Vector2(-projectileSpeed, -projectileSpeed);
            }

        }
        else if (myRigidBody2D.velocity == new Vector2(-projectileSpeed, projectileSpeed))
        {
            if (walltag == "UpWall")
            {
                mySpriteRenderer.sprite = PartsSprite[2];
                myRigidBody2D.velocity = new Vector2(-projectileSpeed, -projectileSpeed);
            }
            else
            {
                mySpriteRenderer.sprite = PartsSprite[0];
                myRigidBody2D.velocity = new Vector2(projectileSpeed, projectileSpeed);
            }

        }
        else if (myRigidBody2D.velocity == new Vector2(projectileSpeed, projectileSpeed))
        {
            if (walltag == "UpWall")
            {
                mySpriteRenderer.sprite = PartsSprite[3];
                myRigidBody2D.velocity = new Vector2(projectileSpeed, -projectileSpeed);
            }
            else
            {
                mySpriteRenderer.sprite = PartsSprite[1];
                myRigidBody2D.velocity = new Vector2(-projectileSpeed, projectileSpeed);
            }

        }
    }
}

    
