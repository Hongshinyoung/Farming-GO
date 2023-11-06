using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class QuestLoader : MonoBehaviour
{
    public List<QuestData> quests = new List<QuestData>();
    public TextMeshProUGUI questText;
    public Button yes, no;

    [System.Serializable]
    public class QuestData
    {
        public string name;
        public string description;
        public int reward;
        public bool completed;
    }

    [System.Serializable]
    public class QuestDataList
    {
        public List<QuestData> quests;
    }

    void Start()
    {
        string filePath = Application.dataPath + "/Scripts/Quest/QuestList/QuestData.json";

        if (File.Exists(filePath))
        {
            // ���Ͽ��� JSON ������ �б�
            string jsonText = File.ReadAllText(filePath);

            // JSON �����͸� QuestDataList ��ü�� �Ľ�
            QuestDataList questDataList = JsonUtility.FromJson<QuestDataList>(jsonText);


            // ù ��° ����Ʈ ������ TextMeshProUGUI�� ǥ��
            if (questDataList.quests.Count > 0)
            {
                
                questText.text = questDataList.quests[0].description;
                yes.gameObject.SetActive(true);
                no.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("����Ʈ ����� ��� �ֽ��ϴ�.");
            }
        }
        else
        {
            Debug.LogError("����Ʈ �����Ͱ� �����ϴ�.");
        }
    }
}
