using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBoss : MonoBehaviour
{

    public int bossHealth = 10;
    private int currentBossHealth = 1;
    public int bossDamage = 1;
    void Start()
    {
        currentBossHealth = bossHealth;
    }

    // Update is called once per frame
    public void TakeFromPlayerDamage(int damage)
    {
        currentBossHealth -= damage;
        if (currentBossHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }


}
