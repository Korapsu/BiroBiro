using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] GameObject[] PlatsPrefabs;
    [SerializeField] int firstPlatSpawn;
    [SerializeField] float resetHeith;

    [SerializeField] List<GameObject> ActivePlats = new();

    [Header("Lava")]
    [SerializeField] float HeightDif;
    [SerializeField] float lavaSpeed;
    [SerializeField] float lavaMultiplier;

    [Header("Ranges")]
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 yRange;

    float Height;

    float highestScore;

    private void Start()
    {
        for (int i = 0; i < firstPlatSpawn; i++) spawn();

        StartCoroutine(ResetPosition());
        highestScore = SaveSystem.load();
    }
    private void Update()
    {
        lavaMultiplier = Player.instance.actualHeight - transform.position.y < HeightDif? 1: 2;
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
            Player _p = collision.GetComponent<Player>();
            if (Player.points > highestScore) SaveSystem.Save(_p);
            SceneManager.LoadScene("jogo"); 
        }

        for (int i = 0; i < 3; i++) spawn();
        ActivePlats.Remove(collision);
        Destroy(collision);
    }
    #endregion

    #region plat
    void spawn() {
        int randomInt;
        while (true) { 
            randomInt = Random.Range(0, PlatsPrefabs.Length);
            if (PlatsPrefabs[randomInt].CompareTag("patPlat") && 
                ActivePlats[^1].CompareTag("patPlat")) continue; //
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
