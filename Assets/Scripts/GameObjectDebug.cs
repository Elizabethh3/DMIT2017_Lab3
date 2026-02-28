using UnityEngine;
using UnityEngine.Events;

public class GameObjectDebug : MonoBehaviour
{
    public UnityEvent OnStarted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnStarted?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
