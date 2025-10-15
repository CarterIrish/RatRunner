using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    /// <summary>
    /// takes all the data and turns it into a binary file
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="day"></param>
    public static void SaveGameData(Inventory inventory, int day)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game-data.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(inventory, day);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// decodes the binary file and returns a GameData class
    /// </summary>
    /// <returns></returns>
    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/game-data.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void DeleteGameData()
    {
        string path = Application.persistentDataPath + "/game-data.data";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.LogWarning("No save file found to delete.");
        }
    }
}
