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

    private int currentQuestIndex = 0; // 현재 진행 중인 퀘스트의 인덱스

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
            // 파일에서 JSON 데이터 읽기
            string jsonText = File.ReadAllText(filePath);

            // JSON 데이터를 QuestDataList 객체로 파싱
            QuestDataList questDataList = JsonUtility.FromJson<QuestDataList>(jsonText);

            // 퀘스트 리스트 업데이트
            quests = questDataList.quests;

            // 첫 번째 퀘스트 설명을 TextMeshProUGUI에 표시
            if (quests.Count > 0)
            {
                questText.text = quests[currentQuestIndex].description;
                btn_Yes.gameObject.SetActive(true);
                btn_No.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("퀘스트 목록이 비어 있습니다.");
            }
        }
    }

    // 완료된 퀘스트를 다음으로 이동
    public void MoveToNextQuest()
    {
        if (currentQuestIndex < currentQuestIndex + 1)
        {
            currentQuestIndex++;
            questText.text = quests[currentQuestIndex].description;
            Debug.Log("로더" + currentQuestIndex +"퀘스트 카운트" + quests.Count + quests[currentQuestIndex].description);

            //if(questComplete != null)
            //{
            //    questComplete.isComplete = false;
            //}
        }
        else
        {
            Debug.Log("모든 퀘스트를 완료했습니다.");
        }

    }
}
