using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string[] toolSlotInfo;
    public string[] itemSlotInfo;
}

public class Inven_SaveManager : MonoBehaviour
{
    public static Inven_SaveManager instance;
    public SaveData saveData = new SaveData();
    public InventoryManager inventoryManager;

    public string path;
    string fileName = "InventorySave.json";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.dataPath + "/Scripts/Save"; // 저장 경로
    }

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    // 인벤토리 데이터 저장
    public void SaveInventoryData()
    {
        SaveData saveData = inventoryManager.GetSaveData(); // InventoryManager로부터 데이터 획득
        string data = JsonUtility.ToJson(saveData);
        File.WriteAllText(Path.Combine(path, fileName), data);
        
        Debug.Log("저장완료");
    }

    // 인벤토리 데이터 불러오기
    public void LoadInventoryData()
    {
        string filePath = Path.Combine(path, fileName);
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(data);
            inventoryManager.LoadSaveData(saveData); // 데이터를 InventoryManager에 전달하여 로드
            Debug.Log("로드완료");
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다.");
        }
    }
    // 인벤토리 데이터 초기화
    public void ClearInventoryData()
    {
        saveData = new SaveData();
    }
}
