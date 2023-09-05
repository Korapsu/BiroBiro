using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rig;
    [SerializeField] float speed;
    public float JumpForce;

    public static float points { get; private set; }

    float savedHeight; // a altura perdida ao reposicionar
    float actualHeight; //  a altura atual tirando o reposicionamento


    void Start(){
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float Mov = Input.GetAxis("horizontal") * speed;
        rig.velocity = new Vector2(Mov, rig.velocity.y);

        if (rig.velocity.y >= 0 && transform.position.y > actualHeight) {
            actualHeight = transform.position.y;
            points = savedHeight + actualHeight;
        }
    }
}
