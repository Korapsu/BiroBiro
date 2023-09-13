using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackImages : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;

    [Header("Paralax")]
    [SerializeField] RawImage img;
    [SerializeField] float _y;

    int lastImage;

    [Header("Image")]
    [SerializeField] int toDivide;
    [SerializeField] Texture[] images;

    private void Start()
    {
        StartCoroutine(pogged());
    }
    private void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(0, _y) * Time.deltaTime, img.uvRect.size);
        pointText.text = $"{Player.points:000}";
    }
    IEnumerator pogged()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            print("pogged");
            int Divided = Mathf.FloorToInt(Player.points / toDivide);

            if (lastImage < Divided) { 
                img.uvRect = new Rect(Vector2.zero, img.uvRect.size);
                img.texture = Divided < images.Length ? images[Divided] : images[^1];

                lastImage = Divided;
                toDivide += 25;
            }
        }
    }
}
