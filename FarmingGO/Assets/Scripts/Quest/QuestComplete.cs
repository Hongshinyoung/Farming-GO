using System.Collections;
using System;
using TMPro;
using UnityEngine;
using System.Data;

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

    private void Start()
    {

    }
    private void Update()
    {
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
        Reward();

        // ���� ����Ʈ�� �̵�
        if (loader != null)
        {
            loader.MoveToNextQuest();
        }
    }

    void FirstQuest()
    {
        //ù ��° ����Ʈ ����

        if (!isComplete && inventoryManager != null)
        {
            if (inventoryManager.GetEquippedSlot(InventorySlot.InventoryType.Tool).itemData == requireQuestItem1)
            {
                Debug.Log("ù ��° ����Ʈ ����");
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
            }
        }
    }
   
    void SecondQuest() //�������� �丶�� ������ �����غ�����
    {
        //�� ��° ����Ʈ ����
        if (!isComplete && inventoryManager != null)
        {
            if(inventoryManager.HasItem("�丶�侾��"))
            {
                Debug.Log("�ι�° ����Ʈ ����");
                isComplete = true;
                StartCoroutine (ActiveQuestComplete());
            }
        }
    }
    void ThirdQuest()    //��縦 ���� �丶�並 ��Ȯ�� ������
    {
        //�� ��° ����Ʈ ����
        if (!isComplete && inventoryManager != null)
        {
            if(requireQuestItem3.name == "Tomato_04")
            {
                Debug.Log("����° ����Ʈ ����");
                isComplete = true;
                StartCoroutine (ActiveQuestComplete());
            }

        }
    }
    void LastQuest()    //100,000G�� ��ƺ�����
    {
        //������ ����Ʈ ����
        if (!isComplete && PlayerStats.Money >= 1000000)
        {
            Debug.Log("������ ����Ʈ ����");
            isComplete = true;
            StartCoroutine (ActiveQuestComplete());
        }
    }



    private void Reward()
    {
        int rewardMoney = 5000; //ù ����
        rewardMoney += 8000; // ����Ʈ ����� ���� ���� ���� 8000���� ����
        PlayerStats.Earn(rewardMoney);
    }
}
