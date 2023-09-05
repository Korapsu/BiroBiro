using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackImages : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] Image BackImage;

    [SerializeField] Image[] images;

    void LateUpdate()
    {
        pointText.text = Player.points.ToString();
        if (Player.points % 1000 == 0) {
            int point = (int)Player.points / 1000;
            BackImage = point < images.Length? images[point] : images[^1];
        }
    }
}
