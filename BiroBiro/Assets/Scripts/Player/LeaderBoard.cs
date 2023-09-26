using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    string key = "ee0df4ddc6e7f9001d540d57f7bf18d43ca51ab449f2df3006c84c0d2dcc2e4b";
    public List<TextMeshProUGUI> names = new();
    public List<TextMeshProUGUI> Points = new();

    public void AttLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(key, (msg)=>
        {
            for (int i = 0; i < names.Count; i++)
            {
                names[i].text = msg[i].Username;
                Points[i].text = msg[i].Score.ToString() ;
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
