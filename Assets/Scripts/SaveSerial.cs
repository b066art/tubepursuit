using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSerial : MonoBehaviour
{
    public static SaveSerial Instance;

    [SerializeField] private int playerLevel = 1;

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
        data.playerLevelSaved = CurrentLevel.Instance.GetLevel();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame() {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            playerLevel = data.playerLevelSaved;
        }

        CurrentLevel.Instance.SetLevel(playerLevel);
    }

    private void OnDestroy() { SaveGame(); }
}

[Serializable]
public class SaveData
{
    public int playerLevelSaved;
}



