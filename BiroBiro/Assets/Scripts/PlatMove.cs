using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMove : PlatBase
{
    float height;
    [SerializeField]bool right = true;
    [SerializeField] Vector3 Target;

    void Update()
    {
        if (right)
        {
            Target = new Vector3(2.5f, transform.position.y);
            transform.position = Vector3.Lerp(transform.position, Target, 2 * Time.deltaTime);
        }
        else
        {
            Target = new Vector3(-2.5f, transform.position.y);
            transform.position = Vector3.Lerp(transform.position, Target, 2 * Time.deltaTime);

        }

        
        if (transform.position.x == Target.x) right = !right;
    }
}
