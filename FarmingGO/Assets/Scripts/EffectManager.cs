using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public ParticleSystem[] effects; // ����Ʈ�� ���� �迭
    public float minsec = 100f;
    public float maxsec = 150f;

    public LandManager landManager;

    void Start()
    {
        StartCoroutine(StartEffecting());
    }

    void EffectLogic()
    {
        int randomIndex = Random.Range(0, effects.Length); // ���� ����Ʈ ����

        // ������ ����Ʈ�� Ȱ��ȭ�մϴ�.
        effects[randomIndex].gameObject.SetActive(true);

        landManager.SetAllLandStatusToWatered();
        landManager.SetAllCropsToWilted();
        StartCoroutine(StopEffectAfterDelay(effects[randomIndex]));
    }

    IEnumerator StartEffecting()
    {
        while (true)
        {
            EffectLogic();
            float interval = Random.Range(minsec, maxsec);// ���� �ð� ���� ����
            Debug.Log("����Ʈ�߻�");
            yield return new WaitForSeconds(interval); 
        }
    }

    IEnumerator StopEffectAfterDelay(ParticleSystem effect)
    { 
        yield return new WaitForSeconds(3); // ����Ʈ ���� �ð���ŭ ���
        effect.gameObject.SetActive(false); // ���õ� ����Ʈ ��Ȱ��ȭ
    }
}
