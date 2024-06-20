using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackHitbox;
    public int playerHealth = 3;
    public KeyCode attackKey = KeyCode.Space;
    public float attackRange = 1.0f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attackKey)) 
        {
            Attack();
        }
    }
    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackHitbox.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<EnemyBehaviour>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackHitbox == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackHitbox.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Took damage");
        if (playerHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Out of lives");
    }
}