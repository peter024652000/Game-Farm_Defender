using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicator : MonoBehaviour {

    [SerializeField] Image imgSpace;
    [SerializeField] Image imgLeft;
    [SerializeField] Image imgRight;
    [SerializeField] Sprite[] spriteSpace;
    [SerializeField] Sprite[] spriteLeft;
    [SerializeField] Sprite[] spriteRight;

    private void Update()
    {
        SpaceButtonSwitch();
        LeftButtonSwitch();
        RightButtonSwitch();

    }

    private void SpaceButtonSwitch()
    {
        if (Input.GetKeyDown("space"))
        {
            imgSpace.sprite = spriteSpace[1];
        }

        else if (Input.GetKeyUp("space"))
        {
            imgSpace.sprite = spriteSpace[0];
        }
    }

    private void LeftButtonSwitch()
    {
        if (Input.GetKeyDown("left"))
        {
            imgLeft.sprite = spriteLeft[1];
        }

        else if (Input.GetKeyUp("left"))
        {
            imgLeft.sprite = spriteLeft[0];
        }
    }

    private void RightButtonSwitch()
    {
        if (Input.GetKeyDown("right"))
        {
            imgRight.sprite = spriteRight[1];
        }

        else if (Input.GetKeyUp("right"))
        {
            imgRight.sprite = spriteRight[0];
        }
    }
}
