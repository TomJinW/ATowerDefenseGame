using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBase : MonoBehaviour
{
    public int damage;
    public bool isBase = true;

    public void DamageOther()
    {
        if(isBase)
        {
            Debug.Log("Hit base");
            GameObject baseTower = GameObject.Find("darkcastle");
            baseTower.GetComponent<Base>().OnHit(damage);
        }
    }
}
