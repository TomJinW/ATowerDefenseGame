using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenButtonController : MonoBehaviour
{
    public TitleScreenOptionController titleScreenOptionController;
    public TitleScreenOptions titleScreenOption = TitleScreenOptions.Start;

    public void MouseEnter()
    {
        titleScreenOptionController.currentSelectedOption = titleScreenOption;
        titleScreenOptionController.setOption();
    }

    public void MouseDown()
    {
        titleScreenOptionController.currentSelectedOption = titleScreenOption;
        titleScreenOptionController.processSelection();
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
