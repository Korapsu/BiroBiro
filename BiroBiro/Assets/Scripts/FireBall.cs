using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireBall : MonoBehaviour
{
    public bool Right;

    int Direction;
    [SerializeField] float speed;
    [SerializeField] float destroyTime;

    void Start()
    {
        GetComponent<SpriteRenderer>().flipX = Right;
        Direction = Right ? 1 : -1;
        StartCoroutine(DestroyThis());
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3.right * Direction), speed * Time.deltaTime);
    }
    IEnumerator DestroyThis() { 
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.instance.Death();
        }
    }
}
