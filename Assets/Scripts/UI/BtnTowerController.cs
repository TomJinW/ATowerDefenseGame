using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BtnTowerController : MonoBehaviour
{
    public TMP_Text discriptionText;
    public Image indicator;

    public HintBoxController hintbox;

    public string header = "Header";
    public string body = "body";
    public string footer = "footer";

    public string priceTag = "$100";
    public int index = 0;
    public BtnTowerManager towerManager;

    public int buttonType = 0;
    private Base gameBase;
    private GameManager gameManager;

    public void setIndicator(bool enabled) {
        indicator.gameObject.SetActive(enabled);
    }

    public void onPointerEnter() {
        hintbox.gameObject.SetActive(true);

        Vector3 buttonPosition = this.transform.position;
        hintbox.transform.position = new Vector3(buttonPosition.x, buttonPosition.y + 170.0f * (Screen.width / 1024.0f), buttonPosition.z);
        hintbox.setText(header, body, footer);
    }

    public void onPointerExit() {
        hintbox.gameObject.SetActive(false);
    }

    public void onPointerClicked() {
        if (buttonType == 0)
        {
            towerManager.setActive(this.index);
        }
        else if (buttonType == 1)
        {
            if (Internals.gems >= 5) {
                Internals.gems -= 5;
                gameBase.health = (gameBase.health + 50.0f).Bounds(0, 100);
            }
        }
        else if (buttonType == 2)
        {
            if (Internals.gems >= 5)
            {
                Internals.gems -= 5;
                gameManager.money += 500;
            }
        }
        else {

        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        gameBase = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (index == 0 && buttonType == 0)
        {
            setIndicator(true);
        }
        else {
            setIndicator(false);
        }
        discriptionText.text = priceTag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
