using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static string PlayerName;
    [SerializeField] LeaderBoard lb;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TMP_InputField nameInput;
    private void Start()
    {
        string name = SaveSystem.loadName();
        if (name != "null") { 
            nameInput.gameObject.SetActive(false);
            nameText.gameObject.SetActive(true);
            nameText.text = name;
        }
    }
    public void newName()
    {
        int point = Mathf.FloorToInt(SaveSystem.loadPoints());

        if (nameInput.IsActive())
        {
            if (nameInput.text.Length > 3 && point > 0)
            {
                PlayerName = nameInput.text;

                lb.NewPlace(PlayerName, point);
                nameText.text = PlayerName;

                SaveSystem.Save(point);

                nameInput.gameObject.SetActive(false);
                nameText.gameObject.SetActive(true);
            }
        }
        else
        {
            lb.NewPlace(PlayerName, point);
        }
    }
}
