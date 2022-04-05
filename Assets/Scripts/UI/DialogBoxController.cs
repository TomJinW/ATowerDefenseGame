using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogBoxController : MonoBehaviour
{
    public int gemScale = 1;

    public string titleBarText = "";
    public string mainText = "";
    public string subText = "";
    public string btn1Text = "";
    public string btn2Text = "";
    public bool[] btnEnabled = { true, true };

    public Image iconImage;
    public Sprite[] iconSprites;
    public Button btn1;
    public Button btn2;

    private bool isDragging = false;
    private Vector3 initMousePosition;
    private Vector3 initObjectPosition;

    public void OnStartDragging()
    {
        Debug.Log("Start Dragging");
        initMousePosition = Input.mousePosition;
        initObjectPosition = transform.position;
        isDragging = true;
    }

    public void OnEndDragging()
    {
        isDragging = false;
    }

    public void setText()
    {
        TMP_Text[] texts = gameObject.transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text textbox in texts)
        {

            switch (textbox.tag)
            {
                case "TitleBar":
                    textbox.text = titleBarText;
                    break;
                case "MainText":
                    textbox.text = mainText;
                    break;
                case "SubText":
                    textbox.text = subText;
                    break;
                case "Btn1":
                    
                    textbox.text = btn1Text;
                    break;
                case "Btn2":
                    
                    textbox.text = btn2Text;
                    break;
            }
        }

        btn1.gameObject.SetActive(btnEnabled[0]);
        btn2.gameObject.SetActive(btnEnabled[1]);

    }

    public void btn1Clicked() {
        Internals.gems += 5 * gemScale;
        iconImage.gameObject.GetComponent<Image>().sprite = iconSprites[1];
        titleBarText = "Success";
        mainText = "You have successfully purchased this item.";
        subText = "";
        btn1Text = "Sure";
        btn2Text = "OK";
        btnEnabled = new bool[] { false, true };
        setText();

    }
    public void btn2Clicked()
    {
        btnEnabled = new bool[] { true, true };
        setText();
        gameObject.SetActive(false);
        Internals.dialogOpened = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Button[] buttons = gameObject.transform.GetComponentsInChildren<Button>();
        //foreach (Button button in buttons)
        //{
        //    switch (button.tag)
        //    {
        //        case "Btn1":
        //            btn1 = button;
        //            break;
        //        case "Btn2":
        //            btn2 = button;
        //            break;
        //    }
        //}
        setText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            float newX = initObjectPosition.x + (Input.mousePosition.x - initMousePosition.x);
            float newY = initObjectPosition.y + (Input.mousePosition.y - initMousePosition.y);
            float scale = 100 * (Screen.width / 1024.0f);
            transform.position = new Vector2(newX.Bounds(scale, Screen.width - scale), newY.Bounds(scale, Screen.height - scale));
        }
    }
}
