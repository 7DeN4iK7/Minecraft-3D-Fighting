using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int healthRegen;
    
    
    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetRelative().SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        SteveOld steveOld = other.GetComponentInParent<SteveOld>();
        if (steveOld)
        {
            steveOld.Heal(healthRegen);
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}
