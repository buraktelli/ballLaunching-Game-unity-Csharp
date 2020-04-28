using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static void Save(string path, PlayerData data)
    {
        using (var fs = new FileStream(path, FileMode.OpenOrCreate)) 
        { 
            var formatter = new BinaryFormatter();
            formatter.Serialize(fs, data); 
        }
    }
    public static PlayerData Load(string path) 
    { 
        using (var fs = new FileStream(path, FileMode.Open)) 
        { 
            var formatter = new BinaryFormatter();
            PlayerData g = (PlayerData)formatter.Deserialize(fs);
            return g;
        } 
    }
}
