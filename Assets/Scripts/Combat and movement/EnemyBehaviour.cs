using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public QuestManager questManager;
    public int maxHealth = 2;
    private int currentHealth;
    public int damage = 1;
    public float movementSpeed = 1;
    private bool PlayerInRange = false;
    public bool PlayerInBox = false;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
        questManager.EnemyKilled();
    }
    private void FixedUpdate()
    {
        //Moving towards the player if player is in the enemy area box and enemy is not touching the player
        if(PlayerInBox && !PlayerInRange)
        {
            transform.position = Vector3.MoveTowards(
                 transform.position,
                 new Vector3(player.position.x,transform.position.y,player.position.z),
                 movementSpeed * Time.deltaTime);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCombat>().TakeDamage(damage);
            
            PlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}
