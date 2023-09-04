using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatBase : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        inpulse(collision.gameObject);
    }

    protected virtual void inpulse(GameObject obj) {
        obj.TryGetComponent(out Player _player);
        if (_player) _player.rig.AddForce(Vector2.up * _player.JumpForce);
    }
}
