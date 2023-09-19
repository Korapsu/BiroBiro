using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatBase : MonoBehaviour
{
    [SerializeField] float increaseValue;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Jump(collision.gameObject);
    }
    protected virtual void Jump(GameObject collision) { 
        if (collision.TryGetComponent(out Rigidbody2D _player)) { 
            _player.AddForce(Vector2.up * increaseValue, ForceMode2D.Impulse);
        }
    }
}
