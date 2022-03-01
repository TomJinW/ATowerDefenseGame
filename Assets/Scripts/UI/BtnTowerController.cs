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
        Debug.Log(index);
        towerManager.setActive(this.index);
    }
   
    // Start is called before the first frame update
    void Start()
    {
        if (index == 0)
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
