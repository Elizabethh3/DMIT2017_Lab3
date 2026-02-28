using UnityEngine;

public class MeleeEnemy : Enemy
{
    Player player;
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
        Destroy(gameObject);
    }
}
