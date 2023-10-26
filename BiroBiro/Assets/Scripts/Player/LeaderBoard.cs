using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    string key = "7cb71f368d516e5e9606863630f8363b338d88923e7c38a17546a95eb058b74f";
    public List<TextMeshProUGUI> names = new();
    public List<TextMeshProUGUI> Points = new();
    private void Start()
    {
        AttLeaderBoard();
    }

    public void AttLeaderBoard()
    {
        print("wa");
        LeaderboardCreator.GetLeaderboard(key, (msg)=>
        {
            int loopLenght = (msg.Length < names.Count)? msg.Length : names.Count;
            for (int i = 0; i < loopLenght; i++)
            {
                names[i].transform.parent.gameObject.SetActive(true);
                names[i].text = msg[i].Username;
                float score = msg[i].Score;

                if (score > Mathf.Pow(10, 12)) Points[i].text = $"{score / Mathf.Pow(10, 9):0.0}t";
                else if (score > Mathf.Pow(10, 9)) Points[i].text = $"{score / Mathf.Pow(10, 9):0.0}b";
                else if (score > Mathf.Pow(10, 6)) Points[i].text = $"{score / Mathf.Pow(10, 6):0.0}m";
                else if (score > Mathf.Pow(10, 3)) Points[i].text = $"{score / Mathf.Pow(10, 3):0.0}k";
                else Points[i].text = $"{score :0.0}";

            }
        });
    }
    public void NewPlace(string playerName, int score)
    {
        LeaderboardCreator.UploadNewEntry(key, playerName, score, (msg) =>
        {
            AttLeaderBoard();
        });
    }
}
