using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class TitleScreenOptionController : MonoBehaviour
{

    public TitleScreenTypes titleScreenType = TitleScreenTypes.Title;

    public TitleScreenOptions currentSelectedOption = TitleScreenOptions.Start;

    public Canvas titleScreenCanvas;
    public Canvas optionScreenCanvas;
    public Canvas storeViewCanvas;

    int timeCount = 60;

    public Image background;

    public void setOption()
    {
        //AudioSource audioData = GetComponent<AudioSource>();
        //audioData.volume = Settings.SFX / 100.0f;
        //audioData.Play(0);
        this.transform.localPosition = Constants.titleScreenOptionPositions[(int)currentSelectedOption];
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
        //AudioSource audioData = GetComponent<AudioSource>();
        //audioData.volume = Settings.SFX / 100.0f;
        //audioData.Play(0);
        switch (currentSelectedOption)
        {
            case TitleScreenOptions.Start:
                if (titleScreenType == TitleScreenTypes.Pause)
                {
                    Time.timeScale = 1;
                    titleScreenCanvas.gameObject.SetActive(false);
                }
                else
                {
                    Time.timeScale = 1;
                    Internals.zombieHPScaler = 1.0f;
                    SceneManager.LoadScene("SeansScene");
                }

                break;
            case TitleScreenOptions.Load:
                titleScreenCanvas.gameObject.SetActive(false);
                storeViewCanvas.gameObject.SetActive(true);
                break;
            case TitleScreenOptions.Option:
                titleScreenCanvas.gameObject.SetActive(false);
                optionScreenCanvas.gameObject.SetActive(true);
                break;
            case TitleScreenOptions.Quit:
                if (titleScreenType == TitleScreenTypes.Pause)
                {
                    Time.timeScale = 1;
                    Internals.winTime = 0;
                    SceneManager.LoadScene("TitleScreen");
                }
                else
                {
                    Application.Quit();
                }
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        background.gameObject.SetActive(titleScreenType == TitleScreenTypes.Pause);
        Application.targetFrameRate = 120;
        this.transform.localPosition = Constants.titleScreenOptionPositions[(int)currentSelectedOption];
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown("d"))
        {
            setNextOption();
        }
        if (Input.GetKeyDown("a"))
        {
            setPreviousOption();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            setNextOption();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            setPreviousOption();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            processSelection();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            processSelection();
        }
        //if (Input.GetKeyDown(KeyCode.Escape)) {
        //    if (titleScreenType == TitleScreenTypes.Pause)
        //    {
        //        Time.timeScale = 1;
        //        titleScreenCanvas.gameObject.SetActive(false);
        //    }
        //}
    }
}
