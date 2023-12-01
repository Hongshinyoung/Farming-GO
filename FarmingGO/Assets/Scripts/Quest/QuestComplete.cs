using System.Collections;
using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;

public class QuestComplete : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Land land;
    public ItemData requireQuestItem1, requireQuestItem2; //1 = ����, 2 = �丶�侾��
    public GameObject requireQuestItem3; // 3 = �丶��(����)
    public ItemSlotData[] slots;
    public TextMeshProUGUI questComplete;
    public bool isComplete;
    private QuestLoader loader;
    private int currentQuestIndex = 0;
    public GameObject questManager;
    public EndingManager endingManager;

    private void Start()
    {
        loader = GetComponent<QuestLoader>();

    }
    private void Update()
    {
        Debug.Log(PlayerStats.Money);
        FirstQuest();
        SecondQuest();
        ThirdQuest();
        LastQuest();
    }

    // ����Ʈ Ŭ���� �� Ŭ���� ���� 1�ʰ� ǥ��
    IEnumerator ActiveQuestComplete()
    {
        questComplete.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        questComplete.gameObject.SetActive(false);
        loader.quest_ING.gameObject.SetActive(false);
        loader.btn_Yes.gameObject.SetActive(true);
        loader.btn_No.gameObject.SetActive(true);
        
        
        loader.MoveToNextQuest(); // ���� ����Ʈ�� �̵�
    }

    void FirstQuest()
    {
        //ù ��° ����Ʈ ����

        if (!isComplete && inventoryManager != null)
        {
            if (inventoryManager.GetEquippedSlot(InventorySlot.InventoryType.Tool).itemData == requireQuestItem1)
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
                Reward();
                currentQuestIndex++;
            }
        }
    }

    void SecondQuest() //�������� �丶�� ������ �����غ�����
    {
        //�� ��° ����Ʈ ����
        if (inventoryManager != null && currentQuestIndex == 1)
        {
            if (inventoryManager.HasItem("�丶�侾��"))
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
                Reward();
                currentQuestIndex++;
            }
        }
    }
    void ThirdQuest()    //��縦 ���� �丶�並 ��Ȯ�� ������
    {
        //�� ��° ����Ʈ ����
        if (inventoryManager != null && currentQuestIndex == 2)
        {
            if (inventoryManager.HasItems("Tomato"))
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
                Reward();
                currentQuestIndex++;
            }

        }
    }
    void LastQuest()    //100,000G�� ��ƺ�����
    {
        //������ ����Ʈ ����
        if (PlayerStats.Money >= 100000)
        {
            isComplete = true;
            StartCoroutine(ActiveQuestComplete());
            //Ending();
            SceneManager.LoadScene(2);
        }
    }



    private void Reward()
    {
        int rewardMoney = 5000; //ù ����
        rewardMoney += 12500; // ����Ʈ ����� ���� ���� ���� 8000���� ����
        PlayerStats.Earn(rewardMoney);
    }

    //private void Ending()
    //{
    //    Debug.Log("��������");
    //    endingManager = GetComponent<EndingManager>();
    //    endingManager.MuteSoundPlayVideo();

    //    StopCoroutine(ActiveQuestComplete());
    //    questManager.SetActive(false);
    //}
}
