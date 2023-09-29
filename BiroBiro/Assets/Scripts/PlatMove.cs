using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMove : PlatBase
{
    float height;
    [SerializeField]bool right = true;
    [SerializeField] float speed;
    [SerializeField] float DirectionMax;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        speed -= Random.Range(-0.5f, 0.5f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float target = right ? -DirectionMax : DirectionMax;

        transform.position = Vector3.Lerp(transform.position, new Vector3(target, transform.position.y), speed * Time.deltaTime);

        if (Mathf.Abs(target - transform.position.x) < 0.1f) right = !right;
        spriteRenderer.flipX = !right;
    }
}
