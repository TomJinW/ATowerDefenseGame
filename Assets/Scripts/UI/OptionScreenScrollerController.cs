using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScreenScrollerController : MonoBehaviour
{
    public OptionScreenOptions screenOptions = OptionScreenOptions.Music;

    public OptionScreenOptionController optionScreenOptionController;

    private bool isDragging = false;
    private Vector3 initMousePosition;
    private Vector3 initObjectPosition;

    public void MouseEnter()
    {
        optionScreenOptionController.currentSelectedOption = screenOptions;
        optionScreenOptionController.setOption();
    }

    public void MouseDown()
    {
        
    }

    public void onStartDragging() {
        initMousePosition = Input.mousePosition;
        initObjectPosition = transform.localPosition;
        isDragging = true;
    }

    public void onEndDragging() {
        isDragging = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging) {

            float newX = initObjectPosition.x + (Input.mousePosition.x - initMousePosition.x) * (Screen.width / 1440.0f);
            //transform.Rotate(new Vector3(0, 0, scaler * -3.14f));
            float xLength = Constants.volumeSliderXRange.y - Constants.volumeSliderXRange.x;
            transform.localPosition = new Vector2(newX.Bounds(Constants.volumeSliderXRange.x, Constants.volumeSliderXRange.y), initObjectPosition.y);
            switch (screenOptions) {
                case OptionScreenOptions.Music:
                    Settings.music = (transform.localPosition.x - Constants.volumeSliderXRange.x) / xLength * 100.0f;
                    Debug.Log(Settings.music);
                    break;
                case OptionScreenOptions.SFX:
                    Settings.SFX = (transform.localPosition.x - Constants.volumeSliderXRange.x) / xLength * 100.0f;
                    Debug.Log(Settings.SFX);
                    break;
            }
        }
    }
}
