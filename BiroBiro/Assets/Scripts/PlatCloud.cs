using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatCloud : PlatBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Jump(collision.gameObject);
    }
    protected override void Jump(GameObject collision)
    {
        if (collision.TryGetComponent(out Rigidbody2D wa) && wa.velocity.y > 0)
        base.Jump(collision);
    }
}
