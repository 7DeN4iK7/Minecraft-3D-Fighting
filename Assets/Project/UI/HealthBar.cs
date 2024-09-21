using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private List<Image> heartImages;
    [SerializeField] private List<Image> heartImagesFill;
    
    [Range(0, 1)] [SerializeField] private float heartShakeBorder;
    
    public void SetHealth(float t)
    {
        float heartPart = 1 / (float)heartImages.Count;
        for (int i = heartImages.Count; i > 0; i--)
        {
            float heartProgress = 1 - Mathf.Clamp(i - t / heartPart, 0, 1);
            heartImagesFill[i - 1].fillAmount = heartProgress;
            
            heartImages[i - 1].transform.DOKill();
            heartImages[i - 1].transform.localPosition = new Vector3(heartImages[i - 1].transform.localPosition.x, 30 * (1 - t) * (1 -heartProgress) * (i % 2 == 0 ? 1 : -1) / 2, 0);
            heartImages[i - 1].transform.DOLocalMoveY(30 * (1 - t) * (1 -heartProgress) * (i % 2 == 0 ? -1 : 1), Mathf.Clamp(0.5f * t, 0.1f, 0.5f)).SetLoops(-1, LoopType.Yoyo);
            if (heartProgress > 0)
                break;
        }
    }

    public void Reset()
    {
        foreach (var image in heartImages)
        {
            image.transform.DOKill();
            image.transform.localPosition = new Vector3(image.transform.localPosition.x, 0, 0);
        }

        foreach (var image in heartImagesFill)
        {
            image.fillAmount = 1;
        }
    }
    
    
}
