using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTowerManager : MonoBehaviour
{

    public BtnTowerController[] towerControllers;

    public void setActive(int index) {
        
        foreach (BtnTowerController tower in towerControllers) {
            tower.setIndicator(false);
        }
        towerControllers[index].setIndicator(true);
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
