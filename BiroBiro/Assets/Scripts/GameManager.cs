using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] GameObject[] PlatsPrefabs;
    [SerializeField] int firstPlatSpawn;

    [Header("Lava")]
    [SerializeField] float HeightDif;
    [SerializeField] float lavaSpeed;

    List<GameObject> ActivePlats = new();

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
        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up, lavaSpeed * Time.deltaTime);
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
        else if (collision.CompareTag("Player")) SceneManager.LoadScene("jogo");

        print(collision.name);

        int randomInt = Random.Range(1, 7);
        if (collision.CompareTag("SpecialPlat") || randomInt > 4){
            for (int i = 0; i < 3; i++) spawn();
            Destroy(collision.gameObject);
        }
        else collision.transform.position = newPos();
    }
    #endregion

    #region plat
    void spawn() {
        int randomInt = Random.Range(0, PlatsPrefabs.Length);
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
            if (player.position.y > 2000) {
                transform.position -= Vector3.up * 2000;
                player.position -= Vector3.up * 2000;

                foreach (GameObject t in ActivePlats)
                    if (t != null) t.transform.position -= Vector3.up * 2000;
                    else ActivePlats.Remove(t);
                Height -= 2000;
                playerComponent.SaveHeight();
            }
        }
    }
}
