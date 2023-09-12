using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackImages : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;

    [SerializeField] int toDivide;

    [SerializeField] Sprite[] images;

    float length;
    float startPos;

    [SerializeField] SpriteRenderer spriteRenderer;
    Transform Cum;

    float ParalaxEffect;

    void Start()
    {
        Cum = Camera.main.transform;

        startPos = transform.position.y;
        length = spriteRenderer.size.y;
    }

    private void Update()
    {
        float rePos = Cum.position.y * (1 - ParalaxEffect);
        float dist = Cum.position.y * ParalaxEffect;

        transform.position = new Vector3(transform.position.y, startPos + dist);

        if (rePos > startPos + length) startPos += length;
        else if (rePos < startPos - length) startPos -= length;
    }
    void LateUpdate()
    {
        pointText.text = $"{Player.instance.points:000}";

        int Divided = Mathf.FloorToInt(Player.instance.points / toDivide);

        spriteRenderer.sprite = Divided < images.Length? images[Divided] : images[^1];
    }
}
