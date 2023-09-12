using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player instance;

    Rigidbody2D rig;
    Inputs inputs;

    // mov
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    bool Fliped = false;

    // aparencia
    [SerializeField] Sprite[] Pimages;
    SpriteRenderer spriteRenderer;

    public float points { get; private set; }

    float savedHeight; // a altura perdida ao reposicionar
    public float actualHeight { get; private set; } //  a altura atual tirando o reposicionamento

    private void Awake()
    {
        if (instance == null)instance = this;
        else Destroy(this.gameObject);
    }
    void Start(){
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (verticalSpeed > 0){
            spriteRenderer.sprite = Pimages[1]; 
            if (verticalSpeed > jumpSpeed) verticalSpeed = jumpSpeed;
        }
        else spriteRenderer.sprite = Pimages[0];

        float Mov = inputs.Mov.Horizontal.ReadValue<float>() * speed;

        if (Mov != 0) Fliped = Mov < 0;

        spriteRenderer.flipX = Fliped;
        rig.velocity = new Vector2(Mov, verticalSpeed);
    }
    public void SaveHeight() {
        savedHeight += actualHeight;
        actualHeight = 0;
    }
}
