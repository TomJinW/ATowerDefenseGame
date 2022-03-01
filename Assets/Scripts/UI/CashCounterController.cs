using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CashCounterController : MonoBehaviour
{
    public Image icon;
    public TMP_Text cashText;
    public System.Random rnd = new System.Random();
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager != null) {
            cashText.text = gameManager.money.ToString();
        }
        //cashText.text = rnd.Next(10000,65535).ToString();
    }
}
