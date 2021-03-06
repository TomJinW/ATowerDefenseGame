using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTowerManager : MonoBehaviour
{

    public BtnTowerController[] towerControllers;
    [SerializeField] ConstructionManager constructionManager;

    public void setActive(int index) {
        
        foreach (BtnTowerController tower in towerControllers) {
            tower.setIndicator(false);
        }
        towerControllers[index].setIndicator(true);
        constructionManager.buildPrefabIndex = index;
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
