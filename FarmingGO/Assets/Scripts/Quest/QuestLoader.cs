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
            // 파일에서 JSON 데이터 읽기
            string jsonText = File.ReadAllText(filePath);

            // JSON 데이터를 QuestDataList 객체로 파싱
            QuestDataList questDataList = JsonUtility.FromJson<QuestDataList>(jsonText);


            // 첫 번째 퀘스트 설명을 TextMeshProUGUI에 표시
            if (questDataList.quests.Count > 0)
            {
                
                questText.text = questDataList.quests[0].description;
                yes.gameObject.SetActive(true);
                no.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("퀘스트 목록이 비어 있습니다.");
            }
        }
        else
        {
            Debug.LogError("퀘스트 데이터가 없습니다.");
        }
    }
}
