using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackHitbox;
    public int playerHealth = 3;
    public KeyCode attackKey = KeyCode.Space;
    public float attackRange = 1.0f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;

    public Image[] healthImages;
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
        UpdateHealthUI();
        if (playerHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Out of lives");
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < playerHealth)
            {
                healthImages[i].enabled = true;
            }
            else
            {
                healthImages[i].enabled = false;
            }
        }
    }
}
