using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintBoxController : MonoBehaviour
{
    private string header = "Header";
    private string body = "body";
    private string footer = "footer";

    public TMP_Text headerText;
    public TMP_Text bodyText;
    public TMP_Text footerText;

    public void setText(string header, string body, string footer) {
        headerText.text = header;
        bodyText.text = body;
        footerText.text = footer;
        this.header = header;
        this.body = body;
        this.footer = footer;
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
