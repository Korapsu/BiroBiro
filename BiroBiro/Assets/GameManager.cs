using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] Plats;
    float Height;

    [Header("ToDestroy")]
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 yRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int randomInt = Random.Range(1, 7);
        if (collision.CompareTag("SpecialPlat") || randomInt >= 4){
            spawn();
            Destroy(collision.gameObject);
        }
        else collision.transform.position = newPos();
    }

    void spawn() {
        int randomInt = Random.Range(0, Plats.Length);
        Vector2 pos = newPos();

        Instantiate(Plats[randomInt], pos, Quaternion.identity);
    }

    Vector2 newPos() {
        Height += Random.Range(yRange.x, yRange.y);
        return new(Random.Range(xRange.x, xRange.y), Height);
    }
}
