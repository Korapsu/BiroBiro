using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player instance;

    Rigidbody2D rig;
    Inputs inputs;

    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;

    public static float points { get; private set; }

    float savedHeight; // a altura perdida ao reposicionar
    public float actualHeight { get; private set; } //  a altura atual tirando o reposicionamento

    private void Awake()
    {
        if (instance == null)instance = this;
        else Destroy(this.gameObject);
    }
    void Start(){
        rig = GetComponent<Rigidbody2D>();
        inputs = new();
        inputs.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        pointSystem();

    }
    void pointSystem() { 
        if (rig.velocity.y >= 0 && transform.position.y > actualHeight) {
            actualHeight = transform.position.y;
            points = savedHeight + actualHeight;
        }
    }
    void Movement() { 
        float verticalSpeed = rig.velocity.y;
        if (rig.velocity.y > jumpSpeed) verticalSpeed = jumpSpeed;

        float Mov = inputs.Mov.Horizontal.ReadValue<float>() * speed;

        rig.velocity = new Vector2(Mov, verticalSpeed);

    }
    public void SaveHeight() {
        savedHeight += actualHeight;
        actualHeight = 0;
    }
}
