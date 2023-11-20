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

        path = Application.dataPath + "/Scripts/Save"; // ���� ���
    }

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    // �κ��丮 ������ ����
    public void SaveInventoryData()
    {
        SaveData saveData = inventoryManager.GetSaveData(); // InventoryManager�κ��� ������ ȹ��
        string data = JsonUtility.ToJson(saveData);
        File.WriteAllText(Path.Combine(path, fileName), data);
        
        Debug.Log("����Ϸ�");
    }

    // �κ��丮 ������ �ҷ�����
    public void LoadInventoryData()
    {
        string filePath = Path.Combine(path, fileName);
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(data);
            inventoryManager.LoadSaveData(saveData); // �����͸� InventoryManager�� �����Ͽ� �ε�
            Debug.Log("�ε�Ϸ�");
        }
        else
        {
            Debug.Log("����� ������ �����ϴ�.");
        }
    }
    // �κ��丮 ������ �ʱ�ȭ
    public void ClearInventoryData()
    {
        saveData = new SaveData();
    }
}
