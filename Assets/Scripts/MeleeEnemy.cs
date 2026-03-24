using UnityEngine;

public class MeleeEnemy : Enemy
{
    Player player;
    [SerializeField] GameObject containerPrefab;
    public override void Attack()
    {
        //if player is in vicinity - will attack once per few seconds
        if (attackRange.CircleOverlapCheck())
        {
            player = FindAnyObjectByType<Player>();
            player.RemoveHealth();
        }
    }

    public override void Die()
    {
        //assign player inventory and container screen when spawned in
        Instantiate(containerPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
