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
    public GameObject questloader;

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
        string filePath = Path.Combine(Application.streamingAssetsPath, "QuestData.json");

        // ���Ͽ��� JSON ������ �б�
        string jsonText = "";

        // ������ ����Ǿ� �ִٸ� WWW�� ����Ͽ� ������ �о�� ��.
#if UNITY_ANDROID && !UNITY_EDITOR
        using (WWW reader = new WWW(filePath))
        {
            while (!reader.isDone) { }
            jsonText = reader.text;
        }
#else
        jsonText = File.ReadAllText(filePath);
#endif

        if (!string.IsNullOrEmpty(jsonText))
        {
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
        if (currentQuestIndex < quests.Count - 1)
        {
            currentQuestIndex++;
            questText.text = quests[currentQuestIndex].description;

            //if(questComplete != null)
            //{
            //    questComplete.isComplete = false;
            //}
        }
        else
        {
            Debug.Log("��� ����Ʈ�� �Ϸ��߽��ϴ�.");
            questloader.SetActive(false);
        }
    }
}