using System.Collections;
using System.Collections.Generic;
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
        
        wa(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        wa(collision.gameObject);
    }

    void wa(GameObject collision)
    {
        if (collision.transform.CompareTag("wall")) return;

        else if (collision.CompareTag("Player")) {
            if (Player.points > highestScore) SaveSystem.Save(collision.GetComponent<Player>());
            SceneManager.LoadScene("jogo"); 
        }

        print(collision.name);

        int randomInt = Random.Range(1, 7);
        if (collision.CompareTag("SpecialPlat") || randomInt > 4){
            for (int i = 0; i < 3; i++) spawn();
        }
        else collision.transform.position = newPos();
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
            yield return new WaitForSeconds(15);
            if (player.position.y > resetHeith) {
                transform.position -= Vector3.up * resetHeith;
                player.position -= Vector3.up * resetHeith;

                foreach (GameObject t in ActivePlats)
                    if (t != null) t.transform.position -= Vector3.up * resetHeith;
                    else ActivePlats.Remove(t);
                Height -= resetHeith;
                playerComponent.SaveHeight();
            }
        }
    }
}
