using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //configuration parameters


    [Header("Player")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float padding = 0.35f;
    [SerializeField] public int health = 300;
    LifeIndicator lifeIndicator;
    [SerializeField] AudioClip playerHittedSound;
    [SerializeField]GameObject ghost;
    bool invincible = false;
    SpriteRenderer mySpriteRenderer;
    AudioSource playerAudioSource;
    

    [Header("Projectile")]
    [SerializeField] GameObject playerProjectlePrefab;
    [SerializeField] float projectileSpeed = 6f;
    [SerializeField] float projectileFiringPeriod = 0.2f;
    [SerializeField] AudioClip bulletShoot;


    public Animator animator;
    Coroutine firingCoroutine;

    float xMin, xMax;
    //float yMin, yMax;

    // Use this for initialization
    void Start () {
        SetUpMoveBoundaries();
        lifeIndicator = FindObjectOfType<LifeIndicator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        playerAudioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}


    private void OnTriggerEnter2D(Collider2D other) // not for all collision; want a gameobject.(which will be the bullet with DamageDealer.cs)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>(); // store the gameobject which is bumped to enemy (usually it is bullet)in the damageDealer.
        if (!damageDealer) { return; }
        StartCoroutine(ProcessHit(damageDealer));

    }

    private IEnumerator ProcessHit(DamageDealer damageDealer)
    {

        if (tag=="Player")
        {
            if (damageDealer.tag == "EnemyProjectile")
            {
                damageDealer.Hit();
            }

            if (invincible == false)
            {
                playerAudioSource.PlayOneShot(playerHittedSound,0.8f);
                invincible = true;
                health -= damageDealer.GetDamage();
                lifeIndicator.DelLife();

                if (health <= 0)
                {
                    Destroy(gameObject);
                    Instantiate(ghost, new Vector2(transform.position.x, transform.position.y + 0.33f), transform.rotation);
                }

                for (int time = 0; time < 10; time++)
                {
                    mySpriteRenderer.color  = new Color (1,0,0,0.7f);
                    yield return new WaitForSeconds(0.05f);
                    mySpriteRenderer.color = new Color(1, 1, 1, 1f);
                    yield return new WaitForSeconds(0.05f);

                }

                invincible = false;

            }

        }

    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;


        if (tag=="Player")
        {
            if (Mathf.Abs(deltaX) > 0 || Mathf.Abs(deltaY) > 0)
            {
                animator.SetBool("IsMoving", true);
            }

            else
            {
                animator.SetBool("IsMoving", false);
            }
        }



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

    private void Fire()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinously());

        }

        if (Input.GetButtonUp("Fire1"))
        {

            StopCoroutine(firingCoroutine);
            animator.SetBool("IsShooting", false);

        }
    }

    IEnumerator FireContinously()

    {

        while (Input.GetButton("Fire1"))
        {
            playerAudioSource.PlayOneShot(bulletShoot, 0.3f);
            GameObject bullet = Instantiate(playerProjectlePrefab, new Vector2(transform.position.x-0.02f,transform.position.y+0.5f+0.2f) , Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            animator.SetBool("IsShooting", true);
            yield return new WaitForSeconds(projectileFiringPeriod);
           
        }

    }

}
