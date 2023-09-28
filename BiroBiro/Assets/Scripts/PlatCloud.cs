using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatCloud : PlatBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Rigidbody2D _player) && _player.velocity.y > 0)
        {
            Jump(collision.gameObject);
        }
    }
}
