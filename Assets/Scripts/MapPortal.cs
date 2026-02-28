using UnityEngine;

public class MapPortal : MonoBehaviour
{
    public int targetMap;
    public int targetEntryPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        MapNavigation.Instance.GoToMap(targetMap, targetEntryPoint);
    }
}
