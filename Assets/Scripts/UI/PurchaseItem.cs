using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItem : MonoBehaviour
{
    public int gemScale = 1;
    public string itemName = "Pokeball";
    public DialogBoxController dialogBoxController;
    public void PointerDown() {
        if (!Internals.dialogOpened) {
            Internals.dialogOpened = true;
            dialogBoxController.gemScale = gemScale;
            dialogBoxController.iconImage.gameObject.GetComponent<Image>().sprite = dialogBoxController.iconSprites[0];
            dialogBoxController.btnEnabled = new bool[] { true, true };
            dialogBoxController.gameObject.SetActive(true);
            dialogBoxController.titleBarText = "Confirm Purchase";
            dialogBoxController.mainText = "Would you like to buy this item?";
            dialogBoxController.subText = itemName;
            dialogBoxController.btn1Text = "Sure";
            dialogBoxController.btn2Text = "Nope";
            dialogBoxController.setText();
            dialogBoxController.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+70, this.transform.position.z);
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
