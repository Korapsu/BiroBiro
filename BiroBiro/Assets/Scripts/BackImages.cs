using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackImages : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] Image BackImage;

    [SerializeField] int toDivide;

    [SerializeField] Sprite[] images;

    void LateUpdate()
    {
        pointText.text = $"{Player.points:000}";

        int Divided = Mathf.FloorToInt(Player.points / toDivide);

        BackImage.sprite = Divided < images.Length? images[Divided] : images[^1];
    }
}
