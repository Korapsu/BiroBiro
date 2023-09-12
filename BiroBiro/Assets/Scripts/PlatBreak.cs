using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatBreak : PlatBase
{
    [SerializeField] float timeToBreak;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected override void Jump(GameObject collision)
    {
        base.Jump(collision);
        StartCoroutine(breakSelf());
    }

    IEnumerator breakSelf() {
        animator.SetTrigger("Ative");
        yield return new WaitForSeconds(timeToBreak);
        if (gameObject) Destroy(gameObject);
    }
}
