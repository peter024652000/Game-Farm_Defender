using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeIndicator : MonoBehaviour {

    [SerializeField] Image[] lifeStatus;
    [SerializeField] Sprite[] emptyLife;
    [SerializeField] int lifeLeft;

	// Use this for initialization
	void Start () {
        lifeLeft = lifeStatus.Length;
	}
	
	// Update is called once per frame
	public void  DelLife () {
        lifeStatus[lifeLeft - 1].sprite = emptyLife[0];
        lifeLeft--;

	}
}
