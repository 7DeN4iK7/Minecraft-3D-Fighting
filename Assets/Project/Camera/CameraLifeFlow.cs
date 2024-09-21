using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraLifeFlow : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    
    private CinemachineTrackedDolly _trackedDolly;

    private void Awake()
    {
        _trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void Start()
    {
        StartCoroutine(LifeRoutine());
    }
    
    public void SetPathProcess(float t)
    {
        _trackedDolly.m_PathPosition = t;
    }

    IEnumerator LifeRoutine()
    {
        float progress = 0;
        while (true)
        {
            while (progress < 1)
            {
                yield return new WaitForEndOfFrame();
                progress += cameraSpeed * Time.deltaTime;
                SetPathProcess(progress);
            }
            
            progress = 1;
            yield return new WaitForSeconds(2);
            
            while (progress > 0)
            {
                yield return new WaitForEndOfFrame();
                progress -= cameraSpeed * Time.deltaTime;
                SetPathProcess(progress);
            }

            progress = 0;
            yield return new WaitForSeconds(2);
        }
    }


}
