using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float MaxDiff;

    Transform player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player == null) return;
        Vector3 Target = new(0, player.position.y, -10);

        float dif = Target.y - transform.position.y;
        float realSpeed = dif > MaxDiff ? speed * 3 : speed;

        transform.position = Vector3.Lerp(transform.position, Target, realSpeed * Time.deltaTime);
    }
}
