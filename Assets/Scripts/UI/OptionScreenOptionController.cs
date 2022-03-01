using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScreenOptionController : MonoBehaviour
{
    public TitleScreenTypes titleScreenTypes = TitleScreenTypes.Title;
    public OptionScreenOptions currentSelectedOption = OptionScreenOptions.OK;
    public Canvas titleScreenCanvas;
    public Canvas optionScreenCanvas;

    public GameObject musicBall;
    public GameObject sfxBall;

    public float distance = 50;

    public Image background;

    public void setVolume(float scaler = 1.0f) {
        float initValue = 0;
        GameObject ballObject = null;
        switch (currentSelectedOption) {
            case OptionScreenOptions.Music:
                initValue = Settings.music;
                ballObject = musicBall;
                break;
            case OptionScreenOptions.SFX:
                initValue = Settings.SFX;
                ballObject = sfxBall;
                break;
            case OptionScreenOptions.OK:
                return;
        }

        initValue += 1.0f * scaler;
        initValue = initValue.Bounds(0, 100);
        Vector3 originalPosition = ballObject.transform.localPosition;
        float xLength = Constants.volumeSliderXRange.y - Constants.volumeSliderXRange.x;
        Vector3 newPosition = new Vector3(Constants.volumeSliderXRange.x + initValue / 100.0f * xLength,originalPosition.y,0);
        ballObject.transform.Rotate(new Vector3(0, 0, scaler * -3.14f));
        ballObject.transform.localPosition = newPosition;
        setOption();
        switch (currentSelectedOption)
        {
            case OptionScreenOptions.Music:
                Settings.music = initValue;
                break;
            case OptionScreenOptions.SFX:
                Settings.SFX = initValue;
                break;
        }
    }

    public void setOption()
    {
        switch (currentSelectedOption)
        {
            case OptionScreenOptions.Music:
                {
                    Vector3 ballPosition = musicBall.transform.localPosition;
                    this.transform.localPosition = new Vector3(ballPosition.x - distance, ballPosition.y - 5, 0);
                }
                break;
            case OptionScreenOptions.SFX:
                {
                    Vector3 ballPosition = sfxBall.transform.localPosition;
                    this.transform.localPosition = new Vector3(ballPosition.x - distance, ballPosition.y - 5, 0);
                }

                break;
            case OptionScreenOptions.OK:
                this.transform.localPosition = Constants.optionScreenBackButtonPosition;
                break;
        }
    }
    public void setNextOption()
    {
        currentSelectedOption = currentSelectedOption.Next();
        setOption();
    }
    public void setPreviousOption()
    {
        currentSelectedOption = currentSelectedOption.Previous();
        setOption();
    }
    public void processSelection()
    {
        switch (currentSelectedOption)
        {
            case OptionScreenOptions.OK:
                optionScreenCanvas.gameObject.SetActive(false);
                titleScreenCanvas.gameObject.SetActive(true);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        background.gameObject.SetActive(titleScreenTypes == TitleScreenTypes.Pause);
        RectTransform rectTransform = (RectTransform)this.transform;
        Debug.Log(rectTransform.rect);
        setOption();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            setNextOption();
        }
        if (Input.GetKeyDown("w"))
        {
            setPreviousOption();
        }
        if (Input.GetKey("d"))
        {
            setVolume();
        }
        if (Input.GetKey("a"))
        {
            setVolume(-1.0f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            setNextOption();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            setPreviousOption();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            setVolume();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            setVolume(-1.0f);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            processSelection();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            processSelection();
        }
    }
}
