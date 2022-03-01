using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTower : SingleTargetTower
{
    [Header("Test Tower Fields")]
    [SerializeField] private float damage;

    protected override void ActOnTarget(GameObject target)
    {
        Debug.Log($"{name} shoots {target.name} for {damage} damage!");
        ShootVisuals(target);
    }

    private void ShootVisuals(GameObject target)
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.localScale = Vector3.one * 0.25f;
        bullet.transform.position = transform.position + Vector3.up * 0.75f;

        float progress = 0;
        float duration = 0.5f;
        Coroutilities.DoUntil(this,
            () =>
            {
                progress += Time.deltaTime / duration;
                bullet.transform.position = Vector3.Lerp(transform.position + Vector3.up * 0.75f, target.transform.position, progress);
            },
            () => progress >= 1
        );
        Coroutilities.DoWhen(this, () => Destroy(bullet), () => progress >= 1);
    }
}