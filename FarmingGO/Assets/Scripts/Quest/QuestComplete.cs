using System.Collections;
using System;
using TMPro;
using UnityEngine;
using System.Data;
using System.Linq.Expressions;

public class QuestComplete : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Land land;
    public ItemData requireQuestItem1, requireQuestItem2; //1 = 괭이, 2 = 토마토씨앗
    public GameObject requireQuestItem3; // 3 = 토마토(열매)
    public ItemSlotData[] slots;
    public TextMeshProUGUI questComplete;
    public bool isComplete;
    private QuestLoader loader;
    private int currentQuestIndex = 0;

    private void Start()
    {
        loader = GetComponent<QuestLoader>();
    }
    private void Update()
    {
        FirstQuest();
        SecondQuest();
        ThirdQuest();
        LastQuest();
    }

    // 퀘스트 클리어 시 클리어 문구 1초간 표시
    IEnumerator ActiveQuestComplete()
    {
        questComplete.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        questComplete.gameObject.SetActive(false);
        loader.quest_ING.gameObject.SetActive(false);
        loader.btn_Yes.gameObject.SetActive(true);
        loader.btn_No.gameObject.SetActive(true);
        
        
        loader.MoveToNextQuest(); // 다음 퀘스트로 이동
    }

    void FirstQuest()
    {
        //첫 번째 퀘스트 로직

        if (!isComplete && inventoryManager != null)
        {
            if (inventoryManager.GetEquippedSlot(InventorySlot.InventoryType.Tool).itemData == requireQuestItem1)
            {
                Debug.Log("첫 번째 퀘스트 성공");
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
                Reward();
                currentQuestIndex++;
            }
        }
    }
   
    void SecondQuest() //상점에서 토마토 씨앗을 구매해보세요
    {
        //두 번째 퀘스트 로직
        if (inventoryManager != null && currentQuestIndex == 1)
        {
            if (inventoryManager.HasItem("토마토씨앗"))
            {
                Debug.Log("두번째 퀘스트 성공");
                isComplete = true;
                StartCoroutine (ActiveQuestComplete());
                Reward();
                currentQuestIndex++;
            }
        }
    }
    void ThirdQuest()    //농사를 지어 토마토를 수확해 보세요
    {
        //세 번째 퀘스트 로직
        if (inventoryManager != null && currentQuestIndex == 2)
        {
            if(inventoryManager.HasItem("Tomato_04"))
            {
                Debug.Log("세번째 퀘스트 성공");
                isComplete = true;
                StartCoroutine (ActiveQuestComplete());
                Reward();
                currentQuestIndex++;
            }

        }
    }
    void LastQuest()    //100,000G를 모아보세요
    {
        //마지막 퀘스트 로직
        if (PlayerStats.Money >= 1000000 && currentQuestIndex == 3)
        {
            Debug.Log("마지막 퀘스트 성공");
            isComplete = true;
            StartCoroutine (ActiveQuestComplete());
            Ending();
        }
    }



    private void Reward()
    {
        int rewardMoney = 5000; //첫 보상
        rewardMoney += 68000; // 퀘스트 진행될 수록 높은 보상 8000원씩 증가
        PlayerStats.Earn(rewardMoney);
    }

    private void Ending()
    {
        EndingManager ending = new EndingManager();
        ending.MuteSoundPlayVideo();
    }
}
