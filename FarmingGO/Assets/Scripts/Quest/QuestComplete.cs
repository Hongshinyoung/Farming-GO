using System.Collections;
using System;
using TMPro;
using UnityEngine;

public class QuestComplete : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Land land;
    public ItemData questTool;
    public TextMeshProUGUI questComplete;

    public bool isComplete;

    private QuestLoader loader;

    private void Start()
    {
        loader = GetComponentInParent<QuestLoader>();
        land = GetComponent<Land>();

    }
    private void Update()
    {
        FirstQuest();
        SecondQuest();
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
        Reward();


        // 다음 퀘스트로 이동
        if (loader != null)
        {
            loader.MoveToNextQuest();
        }
    }

    void FirstQuest()
    {
        //첫 번째 퀘스트 로직

        if (!isComplete && inventoryManager != null)
        {
            if (inventoryManager.GetEquippedSlot(InventorySlot.InventoryType.Tool).itemData == questTool)
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
            }
        }
    }

    void SecondQuest()
    {
        //두 번째 퀘스트 로직
        if (!isComplete && land != null)
        {
            // Land 프리팹의 LandStatus가 Farmland로 바뀌면 퀘스트 성공
            if (land.landStatus == Land.LandStatus.Farmland)
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
            }
        }
        //else Debug.Log("land is null");
    }

    private void Reward()
    {
        int rewardMoney = 10000; //첫 보상
        rewardMoney += 10000; // 퀘스트 진행될 수록 높은 보상 10000원씩 증가
        PlayerStats.Earn(rewardMoney);
    }
}
