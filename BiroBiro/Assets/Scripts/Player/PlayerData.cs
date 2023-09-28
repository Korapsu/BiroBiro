using System;

[Serializable]
public class PlayerData
{
    public float points;
    public string name = "null";

    public PlayerData(float point) {
        points = point;
        name = SaveData.PlayerName == null? "null" : SaveData.PlayerName;
    }
}
