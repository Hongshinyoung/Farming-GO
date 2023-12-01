using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public ParticleSystem[] effects; // 이펙트를 담을 배열
    public float minsec = 100f;
    public float maxsec = 150f;

    public LandManager landManager;

    void Start()
    {
        StartCoroutine(DelayedEffecting());
    }

    IEnumerator DelayedEffecting()
    {
        yield return new WaitForSeconds(60); // 1분 대기
        StartCoroutine(StartEffecting());
    }

    void EffectLogic()
    {
        int randomIndex = Random.Range(0, effects.Length); // 랜덤 이펙트 선택

        // 랜덤한 이펙트를 활성화합니다.
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
            float interval = Random.Range(minsec, maxsec);// 랜덤 시간 간격 설정
            Debug.Log("이펙트발생");
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator StopEffectAfterDelay(ParticleSystem effect)
    {
        yield return new WaitForSeconds(3); // 이펙트 지속 시간만큼 대기
        effect.gameObject.SetActive(false); // 선택된 이펙트 비활성화
    }
}
