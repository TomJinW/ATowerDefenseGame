using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreViewCloseButtonController : MonoBehaviour
{

    public GameObject storeView;
    public GameObject titleScreen;
    public TitleScreenTypes titleScreenTypes = TitleScreenTypes.Title;
    public Image background;
    public void MouseDown()
    {
        if (!Internals.dialogOpened) {
            storeView.SetActive(false);
            titleScreen.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        background.gameObject.SetActive(titleScreenTypes == TitleScreenTypes.Pause);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
