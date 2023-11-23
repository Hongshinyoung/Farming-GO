using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class QuestLoader : MonoBehaviour
{
    public List<QuestData> quests = new List<QuestData>();
    public TextMeshProUGUI questText;
    public Button btn_Yes, btn_No;
    private QuestComplete questComplete;
    public Text quest_ING;

    private int currentQuestIndex = 0; // ���� ���� ���� ����Ʈ�� �ε���

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

            // ����Ʈ ����Ʈ ������Ʈ
            quests = questDataList.quests;

            // ù ��° ����Ʈ ������ TextMeshProUGUI�� ǥ��
            if (quests.Count > 0)
            {
                questText.text = quests[currentQuestIndex].description;
                btn_Yes.gameObject.SetActive(true);
                btn_No.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("����Ʈ ����� ��� �ֽ��ϴ�.");
            }
        }
    }

    // �Ϸ�� ����Ʈ�� �������� �̵�
    public void MoveToNextQuest()
    {
        if (currentQuestIndex < currentQuestIndex + 1)
        {
            currentQuestIndex++;
            questText.text = quests[currentQuestIndex].description;
            Debug.Log("�δ�" + currentQuestIndex +"����Ʈ ī��Ʈ" + quests.Count + quests[currentQuestIndex].description);

            //if(questComplete != null)
            //{
            //    questComplete.isComplete = false;
            //}
        }
        else
        {
            Debug.Log("��� ����Ʈ�� �Ϸ��߽��ϴ�.");
        }

    }
}
