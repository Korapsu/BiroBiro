using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackImages : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] Image BackImage;

    [SerializeField] Sprite[] images;

    void LateUpdate()
    {
        pointText.text = $"{Player.points:000}";

        if (Player.points % 1000 == 0) {
            int point = (int)Player.points / 1000;
            BackImage.sprite = point < images.Length? images[point] : images[^1];
        }
    }
}
