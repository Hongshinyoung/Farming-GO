using System.Collections;
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

    // ����Ʈ Ŭ���� �� Ŭ���� ���� 1�ʰ� ǥ��
    IEnumerator ActiveQuestComplete()
    {
        questComplete.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        questComplete.gameObject.SetActive(false);
        loader.quest_ING.gameObject.SetActive(false);
        loader.btn_Yes.gameObject.SetActive(true);
        loader.btn_No.gameObject.SetActive(true);

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
            if (inventoryManager.GetEquippedSlot(InventorySlot.InventoryType.Tool).itemData == questTool)
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
            }
        }
    }

    void SecondQuest() 
    {
        //�� ��° ����Ʈ ����
        if (!isComplete && land != null)
        {
            // Land �������� LandStatus�� Farmland�� �ٲ�� ����Ʈ ����
            if (land.landStatus == Land.LandStatus.Farmland)
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
            }
        }
        else Debug.Log("land is null");
    }
}
