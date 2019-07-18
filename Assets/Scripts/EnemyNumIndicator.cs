using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyNumIndicator : MonoBehaviour {

    //configuration parameters
    [SerializeField] WaveManager waveManager;
    public int normusLeft;
    public int tinusLeft;
    public int spikusLeft;
    public int splitusLeft;
    [SerializeField] TextMeshProUGUI[] enemyText;
    [SerializeField] Image[] enemyEmptyIndicator;
    [SerializeField] Sprite[] enemyIndicator;

    // Use this for initialization
    void Start () {
        waveManager = FindObjectOfType<WaveManager>();
        normusLeft = waveManager.waveConfigs[waveManager.waveNum].GetNormusNum();
        tinusLeft = waveManager.waveConfigs[waveManager.waveNum].GetTinusNum();
        spikusLeft = waveManager.waveConfigs[waveManager.waveNum].GetSpikusNum();
        splitusLeft = waveManager.waveConfigs[waveManager.waveNum].GetSplitusNum();

        enemyText[0].text = normusLeft.ToString();
        enemyText[1].text = tinusLeft.ToString();
        enemyText[2].text = spikusLeft.ToString();
        enemyText[3].text = splitusLeft.ToString();
    }

    void Update()
    {
        EnemyIndicatorSetActive();

        if(waveManager.waveNum != waveManager.finalWave)
        {
            EnemyLeftRenew();
        }

    }

    public void EnemyLeftCount(string enemytag)
    {

        if(enemytag=="Normus"&&normusLeft>=1){
            normusLeft--;
            enemyText[0].text = normusLeft.ToString();
        }

        else if (enemytag == "Tinus" && tinusLeft >= 1){
            tinusLeft--;
            enemyText[1].text = tinusLeft.ToString();
        }
        else if (enemytag == "Spikus" && spikusLeft >= 1){
            spikusLeft--;
            enemyText[2].text = spikusLeft.ToString();
        }
        else if (enemytag == "Splitus" && splitusLeft >= 1){
            splitusLeft--;
            enemyText[3].text = splitusLeft.ToString();
        }
        else{

        }
    }

    public void EnemyLeftRenew()
    {

        waveManager.NextWave();
        if (normusLeft == 0 && tinusLeft == 0 && spikusLeft == 0 && splitusLeft == 0 && waveManager.waveNum < waveManager.finalWave)
        {
            normusLeft = waveManager.waveConfigs[waveManager.waveNum].GetNormusNum();
            tinusLeft = waveManager.waveConfigs[waveManager.waveNum].GetTinusNum();
            spikusLeft = waveManager.waveConfigs[waveManager.waveNum].GetSpikusNum();
            splitusLeft = waveManager.waveConfigs[waveManager.waveNum].GetSplitusNum();

            enemyText[0].text = normusLeft.ToString();
            enemyText[1].text = tinusLeft.ToString();
            enemyText[2].text = spikusLeft.ToString();
            enemyText[3].text = splitusLeft.ToString();
        }
        else
        {

        }   
    }
	
    public void EnemyIndicatorSetActive()
    {
        if (waveManager.waveNum >= 0)
        {
           enemyEmptyIndicator[0].sprite = enemyIndicator[0];
        }

        if (waveManager.waveNum >=1)
        {
            enemyEmptyIndicator[1].sprite = enemyIndicator[1];
        }

        if (waveManager.waveNum >= 3)
        {
            enemyEmptyIndicator[2].sprite = enemyIndicator[2];
        }

        if (waveManager.waveNum >= 5)
        {
            enemyEmptyIndicator[3].sprite = enemyIndicator[3];
        }


    }

}
