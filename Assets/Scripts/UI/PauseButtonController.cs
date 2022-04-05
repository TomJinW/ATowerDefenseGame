using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    public Canvas mainMenuCanvas;

    public void MouseOnClick() {
        Time.timeScale = 0;
        mainMenuCanvas.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale != 0) {
                Time.timeScale = 0;
                mainMenuCanvas.gameObject.SetActive(true);
            }
        }
    }


}
