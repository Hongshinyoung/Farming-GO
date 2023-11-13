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
        //첫 번째 퀘스트 로직

        if (!isComplete && inventoryManager != null)
        {
            if (inventoryManager.GetEquippedSlot(InventorySlot.InventoryType.Tool).itemData == questTool)
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
            }
        }


        //두 번째 퀘스트 로직
        if (!isComplete)
        {
            if (CompareTag("Land") && land.landStatus == Land.LandStatus.Farmland)
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
            }
        }
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

        // 다음 퀘스트로 이동
        if (loader != null)
        {
            loader.MoveToNextQuest();
        }
    }
}
