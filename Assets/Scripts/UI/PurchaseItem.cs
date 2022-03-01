using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItem : MonoBehaviour
{
    public string itemName = "Pokeball";
    public DialogBoxController dialogBoxController;
    public void PointerDown() {
        if (!Internals.dialogOpened) {
            Internals.dialogOpened = true;
            dialogBoxController.iconImage.gameObject.GetComponent<Image>().sprite = dialogBoxController.iconSprites[0];
            dialogBoxController.btnEnabled = new bool[] { true, true };
            dialogBoxController.gameObject.SetActive(true);
            dialogBoxController.titleBarText = "Confirm Purchase";
            dialogBoxController.mainText = "Would you like to buy this item?";
            dialogBoxController.subText = itemName;
            dialogBoxController.btn1Text = "Sure";
            dialogBoxController.btn2Text = "Nope";
            dialogBoxController.setText();
            dialogBoxController.transform.position = this.transform.position;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
