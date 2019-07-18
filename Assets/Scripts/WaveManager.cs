using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    //Configuration parameter 
    [SerializeField] public List<WaveConfig> waveConfigs;
    [SerializeField] public int waveNum = 0;
    [SerializeField] public int finalWave = 8;
    EnemyNumIndicator enemyNumIndicator;


    // Use this for initialization
    IEnumerator Start()
    {
        enemyNumIndicator = FindObjectOfType<EnemyNumIndicator>();
        yield return new WaitForSeconds(1.5f);// Wave 1 presented correctly
        do
        {

            yield return StartCoroutine(SpawnAllPattern()); //prevent this from infinite loop.

        } while (waveConfigs[waveNum].GetLooping());

    }

    // Update is called once per frame
    private void Update()
    {
        winWave();
    }

    public void NextWave()
    {

        if (enemyNumIndicator.normusLeft==0 && enemyNumIndicator.tinusLeft==0 && enemyNumIndicator.spikusLeft == 0 && enemyNumIndicator.splitusLeft == 0 && waveNum<=finalWave-1)
        {
            waveNum++;
        }

    }


    private IEnumerator SpawnAllPattern()
    {
        int currentWaveNum = waveNum;

        for (int patternIndex = waveConfigs[waveNum].GetStartingPattern(); patternIndex < waveConfigs[waveNum].GetPatternConfig().Count; patternIndex++)
        {
            var currentPattern = waveConfigs[waveNum].GetPatternConfig()[patternIndex];      
            yield return StartCoroutine(SpawnAllEnemiesInPattern(currentPattern));

            if (currentWaveNum != waveNum) //start a wave from its 1st pattern
            {
                patternIndex = (waveConfigs[waveNum].GetStartingPattern())-1; //This will +1 when entering for loop
                currentWaveNum = waveNum;
            }
        }
    }
    private IEnumerator SpawnAllEnemiesInPattern(PatternConfig patternConfig)
    {
        
        for (int enemyCount = 0; enemyCount < patternConfig.GetNumberOfEnemies(); enemyCount++)
        {
            if (enemyCount==0)
            {
                yield return new WaitForSeconds(0.8f);
            }
            var newEnemy = Instantiate(patternConfig.GetEnemyPrefab(), patternConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(patternConfig); //Ask this enemy to have specific path(in this case is following currentWave).
            yield return new WaitForSeconds(patternConfig.GetTimeBetweenSpawns());

        }
        yield return new WaitForSeconds(patternConfig.GetTimeBetweenWaves());
    }

    private void winWave()
    {
        if (enemyNumIndicator.normusLeft == 0 && enemyNumIndicator.tinusLeft == 0 && enemyNumIndicator.spikusLeft == 0 && enemyNumIndicator.splitusLeft == 0 && waveNum == finalWave)
        {
            Destroy(gameObject);//stop waves spawning
        }
    }
    
}
