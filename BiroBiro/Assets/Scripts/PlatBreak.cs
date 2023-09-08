using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatBreak : PlatBase
{
    [SerializeField] float timeToBreak;
    protected override void Jump(GameObject collision)
    {
        base.Jump(collision);
        StartCoroutine(breakSelf());
    }

    IEnumerator breakSelf() {
        yield return new WaitForSeconds(timeToBreak);
        if (gameObject) Destroy(gameObject);
    }
}
