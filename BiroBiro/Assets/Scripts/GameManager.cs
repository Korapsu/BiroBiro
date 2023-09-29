using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] GameObject[] PlatsPrefabs;
    [SerializeField] GameObject FireBall;

    [SerializeField] float resetHeith;

    [SerializeField] List<GameObject> ActivePlats = new();

    [Header("platSpawn")]
    [SerializeField] int firstPlatSpawn;
    [SerializeField] int platToSpawn;

    [Header("Lava")]
    [SerializeField] float HeightDif;
    [SerializeField] float lavaSpeed;
    [SerializeField] float lavaMultiplier;

    [Header("Ranges")]
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 yRange;

    float Height;


    private void Start()
    {
        for (int i = 0; i < firstPlatSpawn; i++) spawn();

        StartCoroutine(ResetPosition());
    }
    private void Update()
    {
        lavaMultiplier = Player.instance.actualHeight - transform.position.y < HeightDif? 1: 1 + Player.points / 50;
        float actualSpeed = lavaSpeed * lavaMultiplier;

        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up, actualSpeed * Time.deltaTime);
    }
    #region collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        createPlat(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        createPlat(collision.gameObject);
    }

    void createPlat(GameObject collision)
    {
        if (collision.transform.CompareTag("wall")) return;

        else if (collision.CompareTag("Player")) {
            Player.instance.Death();
        }
        if (Player.points > 2200 && Random.Range(0, 3) == 1) spawnFireBall();
        for (int i = 0; i < platToSpawn; i++) spawn();
        ActivePlats.Remove(collision);
        Destroy(collision);
    }
    #endregion

    #region plat
    void spawnFireBall()
    {
        bool right = Random.Range(0, 2) == 1;
        float side = right ? xRange.x - 2: xRange.y + 2;

        Vector3 pos = new(side , Player.instance.transform.position.y + (Random.Range(-yRange.x, yRange.y) * 2));

        GameObject ball = Instantiate(FireBall, pos, Quaternion.identity);
        ball.GetComponent<FireBall>().Right = right;
    }
    void spawn() {
        int randomInt, prefabLength = Player.points > 1200 ? PlatsPrefabs.Length : PlatsPrefabs.Length - 1;

        while (true) { 

            randomInt = Random.Range(0, prefabLength);
            if (PlatsPrefabs[randomInt].CompareTag("patPlat") && 
                ActivePlats[^1].CompareTag("patPlat")) continue; 
            break;
        }
        Vector2 pos = newPos();

        GameObject plat = Instantiate(PlatsPrefabs[randomInt], pos, Quaternion.identity);
        ActivePlats.Add(plat);
    }
    Vector2 newPos() {
        Height += Random.Range(yRange.x, yRange.y);
        return new(Random.Range(xRange.x, xRange.y), Height);
    }
    #endregion
    IEnumerator ResetPosition() {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Player playerComponent = player.GetComponent<Player>();

        while (true) {
            yield return new WaitForSeconds(5);
            if (player.position.y > resetHeith) {
                transform.position -= Vector3.up * resetHeith;

                player.position -= Vector3.up * resetHeith;
                Camera.main.transform.position -= Vector3.up * resetHeith;

                int _apSize = ActivePlats.Count;
                for (int i = 0; i < _apSize; i++)
                {
                    if (ActivePlats[i] != null) ActivePlats[i].transform.position -= Vector3.up * resetHeith;
                }  
                Height -= resetHeith;
                playerComponent.SaveHeight();
            }
        }
    }
}
