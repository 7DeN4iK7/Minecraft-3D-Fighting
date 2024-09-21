using System.Collections;
using UnityEngine;
public class Coroutines : MonoBehaviour
{
    private static Coroutines instance
    {  get
        {
            if(m_instance == null)
            {
                var go = new GameObject("[COROUTINE MANAGER]");
                m_instance = go.AddComponent<Coroutines>();
                DontDestroyOnLoad(go);
            }
            return m_instance;
        }
    }

    private static Coroutines m_instance;

    public static Coroutine StartRoutine(IEnumerator numerator)
    {
        return instance.StartCoroutine(numerator);
    }

    public static void StopRoutine(Coroutine coroutine) 
    {
        instance.StopCoroutine(coroutine);
    }

    public static bool TryStopRoutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            instance.StopCoroutine(coroutine);
            return true;
        }

        return false;
    }
}