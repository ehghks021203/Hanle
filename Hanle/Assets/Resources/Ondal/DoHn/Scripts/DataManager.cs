using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data {
    public bool [] isUnlock = new bool[17];
}

public class DataManager : MonoBehaviour
{
    static GameObject container;
#region SINGLETON
    static DataManager instance;
    public static DataManager Instance {
        get {
            if (!instance) {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }
#endregion
    string path;
    public Data data = new Data();

#region DATA LOAD
    public void Load() {
        path = Path.Combine(Application.persistentDataPath, "data.json");
        if (File.Exists(path)) {
            string fromJsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<Data>(fromJsonData);
        }
        else {
            for (int i = 0; i < data.isUnlock.Length; i++)
                data.isUnlock[i] = false;
            Save();
        }
    }
#endregion

#region DATA SAVE
    public void Save() {
        path = Path.Combine(Application.persistentDataPath, "data.json");
        string toJsonData = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, toJsonData);
    }
#endregion

#region DATA CHANGE
    public void Unlock(int id) {
        data.isUnlock[id] = true;
        Save();
    }
#endregion
}