using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(Player player){ 
        BinaryFormatter bi = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.Birola";
        FileStream file = new(path, FileMode.Create);

        PlayerData PD = new(player);
        bi.Serialize(file, PD);
        file.Close();
    }
    public static float load(){
        string path = Application.persistentDataPath + "/player.Birola";
        if (File.Exists(path)){
            BinaryFormatter bi = new();
            FileStream file = new(path, FileMode.Open);

            PlayerData PD = bi.Deserialize(file) as PlayerData;

            file.Close();
            return PD.points;
        }
        else{
            Debug.LogError($"path not found: {path}");
            return 0;
        }
    }
}
