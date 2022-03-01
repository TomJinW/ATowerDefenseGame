using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonController : MonoBehaviour
{

    public Canvas titlescreen;
    public Canvas optionscreen;
    public OptionScreenOptionController optionScreenOptionController;

    public TitleScreenTypes type = TitleScreenTypes.Title;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MouseDown()
    {
        titlescreen.gameObject.SetActive(true);
        optionscreen.gameObject.SetActive(false);

    }

    public void MouseEnter()
    {
        optionScreenOptionController.currentSelectedOption = OptionScreenOptions.OK;
        optionScreenOptionController.setOption();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
