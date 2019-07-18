using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveIndicator : MonoBehaviour {

    //configuraton parameter
    WaveManager waveManager;
    Player player;
    EnemyNumIndicator enemyNumIndicator;
    [SerializeField] GameObject waveUI;
    [SerializeField] TextMeshProUGUI waveTextUI;
    [SerializeField] TextMeshProUGUI waveNumUI;
    [SerializeField] GameObject signAgain;
    [SerializeField] GameObject signLeave;
    [SerializeField] GameObject playerMenu;
    [SerializeField] GameObject youWin;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip soundToNextWave;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioSource bgm;
    AudioSource waveAudio;

    bool win = false;
    bool die = false;


    // Use this for initialization
    IEnumerator Start () {
       
        waveManager = FindObjectOfType<WaveManager>();
        player = FindObjectOfType<Player>();
        enemyNumIndicator = FindObjectOfType<EnemyNumIndicator>();
        waveAudio = GetComponent<AudioSource>();

        waveNumUI.text = (waveManager.waveNum + 1).ToString();
        waveUI.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        waveUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(WaveNumRenew());
        Win();
    }


    private IEnumerator WaveNumRenew()
    {
        if (enemyNumIndicator.normusLeft == 0 && enemyNumIndicator.tinusLeft == 0 && enemyNumIndicator.spikusLeft == 0 && enemyNumIndicator.splitusLeft == 0 && waveManager.waveNum+1 <waveManager.finalWave)
        {
            waveAudio.PlayOneShot(soundToNextWave, 0.8f);
            yield return new WaitForSeconds(0.25f);

            if ((waveManager.waveNum+1) == waveManager.finalWave) //final wave
            
            {
                waveTextUI.text = "Final";
                waveNumUI.text = "Wave";
                waveUI.SetActive(true);
                yield return new WaitForSeconds(1.0f);
                waveUI.SetActive(false);
            }


            else if (waveManager.waveNum < waveManager.finalWave)//every wave
            {
                waveNumUI.text = (waveManager.waveNum + 1).ToString();
                waveUI.SetActive(true);
                yield return new WaitForSeconds(1.0f);
                waveUI.SetActive(false);
            }
              
        }

        else if (player.health <= 0)
        {
           
            if (die ==false)
            {

                bgm.Stop();
                waveAudio.PlayOneShot(gameOverSound,0.1f);
                waveTextUI.text = "Game";
                waveNumUI.text = "Over";
                waveUI.SetActive(true);
                Instantiate(signAgain, new Vector2(signAgain.transform.position.x, signAgain.transform.position.y), transform.rotation);
                Instantiate(signLeave, new Vector2(signLeave.transform.position.x, signLeave.transform.position.y), transform.rotation);
                die = true;
            }
           
        }

    }


    private void Win()  
        
    {
        if (enemyNumIndicator.normusLeft == 0 && enemyNumIndicator.tinusLeft == 0 && enemyNumIndicator.spikusLeft == 0 && enemyNumIndicator.splitusLeft == 0 && waveManager.waveNum == waveManager.finalWave)
        {
            DestroyAllEnemy();//Clean spawned Enemies (e.g.Parts from Splitus)
            if (win == false)

            {

                bgm.Stop();
                waveAudio.PlayOneShot(winSound, 0.2f);
                Destroy(player.gameObject);
                Instantiate(playerMenu, new Vector2(player.transform.position.x, player.transform.position.y), transform.rotation);
                Instantiate(youWin, new Vector2(youWin.transform.position.x, youWin.transform.position.y), transform.rotation);
                Instantiate(signAgain, new Vector2(signAgain.transform.position.x, signAgain.transform.position.y), transform.rotation);
                Instantiate(signLeave, new Vector2(signLeave.transform.position.x, signLeave.transform.position.y), transform.rotation);
                win = true;
            }
        }
        
    }

    private void DestroyAllEnemy()
    {

        if (GameObject.FindWithTag("Normus")==true)
        {
            GameObject explosionNormus = Instantiate(explosionVFX, new Vector2(GameObject.FindWithTag("Normus").transform.position.x, GameObject.FindWithTag("Normus").transform.position.y), transform.rotation);
            Destroy(explosionNormus, 0.4f);
            Destroy(GameObject.FindWithTag("Normus"));
        }

        if (GameObject.FindWithTag("Tinus") == true)
        {
            GameObject explosionTinus = Instantiate(explosionVFX, new Vector2(GameObject.FindWithTag("Tinus").transform.position.x, GameObject.FindWithTag("Tinus").transform.position.y), transform.rotation);
            Destroy(explosionTinus, 0.4f);
            Destroy(GameObject.FindWithTag("Tinus"));
        }

        if (GameObject.FindWithTag("Spikus") == true)
        {
            GameObject explosionSpikus = Instantiate(explosionVFX, new Vector2(GameObject.FindWithTag("Spikus").transform.position.x, GameObject.FindWithTag("Spikus").transform.position.y), transform.rotation);
            Destroy(explosionSpikus, 0.4f);
            Destroy(GameObject.FindWithTag("Spikus"));
        }

        if (GameObject.FindWithTag("Splitus") == true)
        {
            GameObject explosionSplitus = Instantiate(explosionVFX, new Vector2(GameObject.FindWithTag("Splitus").transform.position.x, GameObject.FindWithTag("Splitus").transform.position.y), transform.rotation);
            Destroy(explosionSplitus, 0.4f);
            Destroy(GameObject.FindWithTag("Splitus"));
        }

        if (GameObject.FindWithTag("Parts") == true)
        {
            GameObject explosionParts = Instantiate(explosionVFX, new Vector2(GameObject.FindWithTag("Parts").transform.position.x, GameObject.FindWithTag("Parts").transform.position.y), transform.rotation);
            Destroy(explosionParts, 0.4f);
            Destroy(GameObject.FindWithTag("Parts"));
        }

        if (GameObject.FindWithTag("EnemyProjectile") == true)
        {
            GameObject explosionProjectile = Instantiate(explosionVFX, new Vector2(GameObject.FindWithTag("EnemyProjectile").transform.position.x, GameObject.FindWithTag("EnemyProjectile").transform.position.y), transform.rotation);
            Destroy(explosionProjectile, 0.4f);
            Destroy(GameObject.FindWithTag("EnemyProjectile"));
        }

    }

}
