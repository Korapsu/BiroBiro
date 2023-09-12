using System;

[Serializable]
public class PlayerData
{
    public float points;

    public PlayerData(Player player) {
        points = player.points;
    }
}
