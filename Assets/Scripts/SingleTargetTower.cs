using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleTargetTower : MonoBehaviour
{
    [Header("Base Class Fields")]
    [SerializeField] [TagSelector] private string targetTag = "Enemy";
    [Tooltip("This tower will attempt to update its target every `retargetInterval` seconds.")]
    [SerializeField] private float retargetInterval = 0.5f;
    [Tooltip("This tower will act upon its current target every `actInterval` seconds.")]
    [SerializeField] private float actInterval = 0.25f;

    private HashSet<GameObject> targetsInRange;
    private GameObject currentTarget;

    private Coroutine updateTargetCorout;
    private Coroutine actOnTargetCorout;

    protected virtual void Start()
    {
        targetsInRange = new HashSet<GameObject>();
    }

    protected virtual void OnEnable()
    {
        //Do until repeatedly does a thing until the condition it's given is true; here, they will go on forever until
        //stopped in OnDisable.
        updateTargetCorout = Coroutilities.DoUntil(this, UpdateTarget, () => false, retargetInterval, true);
        actOnTargetCorout = Coroutilities.DoUntil(this, TryActOnTarget, () => false, actInterval, true);
    }

    protected virtual void OnDisable()
    {
        Coroutilities.TryStopCoroutine(this, ref updateTargetCorout);
        Coroutilities.TryStopCoroutine(this, ref actOnTargetCorout);
    }

    private void UpdateTarget()
    {
        //RemoveWhere removes all elements that meet a condition; in this case, if
        //target is null/pending destroy
        targetsInRange.RemoveWhere(target => !target);

        //Go through the targets in range (order is not guaranteed because hashsets aren't normally accessed like
        //this; we don't care about order), find the closest target among them, and make that the current target
        currentTarget = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject target in targetsInRange)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                currentTarget = target;
            }
        }
    }

    private void TryActOnTarget()
    {
        if (currentTarget)
        {
            ActOnTarget(currentTarget);
        }
    }

    protected abstract void ActOnTarget(GameObject target);

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            targetsInRange.Add(other.gameObject);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        targetsInRange.Remove(other.gameObject);
    }
}
