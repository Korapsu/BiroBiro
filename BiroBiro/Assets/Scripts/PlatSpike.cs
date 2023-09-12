using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatSpike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();

        if (collision.gameObject.transform.position.y > transform.position.y && rig.velocity.y <= 0)
            SceneManager.LoadScene("jogo");
    }
}
