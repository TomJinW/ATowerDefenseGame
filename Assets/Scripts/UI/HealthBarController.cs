using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public TMP_Text healthBarText;
    public Image healthImage;
    public Sprite[] healthBarSprites;
    public Base gameBase;

    private float tmpHealth = 1.0f;

    public void setHealthBar(float health) {
        healthBarText.text = ((int)(health * 100.0f)).ToString() + " / 100";
        healthImage.transform.localScale = new Vector3(health, 1.0f, 1.0f);

        if (health >= 0.5f)
        {
            healthImage.sprite = healthBarSprites[0];
        }
        else if (health >= 0.2f && health < 0.5f)
        {
            healthImage.sprite = healthBarSprites[1];
        }
        else {
            healthImage.sprite = healthBarSprites[2];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameBase != null) {
            setHealthBar((gameBase.health / 100.0f).Bounds(0.0f,1.0f));
        }
        //tmpHealth -= 0.001f;
        //if (tmpHealth <= 0) tmpHealth = 1.0f;
        //setHealthBar(tmpHealth);
    }
}
