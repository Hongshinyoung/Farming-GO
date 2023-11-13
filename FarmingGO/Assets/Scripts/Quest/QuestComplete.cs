using System.Collections;
using TMPro;
using UnityEngine;

public class QuestComplete : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemData questTool;
    public TextMeshProUGUI questComplete;

    public bool isComplete;

    private QuestLoader loader;
    private Land land;

    private void Start()
    {
        loader = GetComponentInParent<QuestLoader>();
        loader.btn_Yes.onClick.AddListener(Update);
    }

    private void Update()
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


        //�� ��° ����Ʈ ����
        if (!isComplete)
        {
            if (CompareTag("Land") && land.landStatus == Land.LandStatus.Farmland)
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
            }
        }
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
}
