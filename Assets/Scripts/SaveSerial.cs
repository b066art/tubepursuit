using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSerial : MonoBehaviour
{
    public static SaveSerial Instance;

    private Dictionary<int, int> levels = new Dictionary<int, int>() { [0] = 0, [1] = 0, [2] = 0, [3] = 0, [4] = 0, [5] = 0, [6] = 0, [7] = 0, [8] = 0, [9] = 0, [10] = 0, [11] = 0, [12] = 0, [13] = 0, [14] = 0, [15] = 0, [16] = 0, [17] = 0, [18] = 0, [19] = 0, [20] = 0, [21] = 0, [22] = 0, [23] = 0, [24] = 0, [25] = 0, [26] = 0, [27] = 0, [28] = 0, [29] = 0, [30] = 0, [31] = 0, [32] = 0, [33] = 0, [34] = 0, [35] = 0, [36] = 0, [37] = 0, [38] = 0, [39] = 0, [40] = 0, [41] = 0, [42] = 0, [43] = 0, [44] = 0, [45] = 0, [46] = 0, [47] = 0, [48] = 0, [49] = 0, [50] = 0, [51] = 0, [52] = 0, [53] = 0, [54] = 0, [55] = 0, [56] = 0, [57] = 0, [58] = 0, [59] = 0, [60] = 0, [61] = 0, [62] = 0, [63] = 0 };
    private int playerLevel = 1;

    private void Awake() { Instance = this; }

    private void Start() {
        EventManager.LevelStartEvent.AddListener(SaveGame);
        LoadGame();
    }

    public int GetPlayerLevel() { return playerLevel; }

    public void SaveGame() {
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat"); 
        SaveData data = new SaveData();
        data.levelsSaved = CurrentLevel.Instance.GetLevels();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame() {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            levels = data.levelsSaved;
        }

        for (int i = 0; i < levels.Count; i++) {
            if (levels[i] == 0) {
                playerLevel = i + 1;
                break;
            }
        }

        CurrentLevel.Instance.SetLevel(playerLevel);
        CurrentLevel.Instance.SetLevels(levels);
    }

    private void OnDestroy() { SaveGame(); }
}

[Serializable]
public class SaveData
{
    public Dictionary<int, int> levelsSaved = new Dictionary<int, int>();
}



