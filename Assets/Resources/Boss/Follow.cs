using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform player;
    public float followRadius = 10.0f; // Radius within which the enemy will follow the player

    // Attacking
    public float attackRange = 2.0f; // Range within which the enemy can attack

    // Projectile prefabs
    public GameObject firstRingProjectilePrefab;
    public float firstRingProjectileSpeed = 5f;
    public float firstRingProjectileRadius = 3f;
    public float firstRingTimeBetweenAttacks = 1.0f;

    public GameObject secondRingProjectilePrefab;
    public float secondRingProjectileSpeed = 3f;
    public float secondRingProjectileRadius = 5f;
    public float secondRingTimeBetweenAttacks = 2.0f;

    // States
    public bool playerInSightRange, playerInAttackRange;

    // Health
    public int health = 3;

    // Attack flags
    private bool firstRingAlreadyAttacked;
    private bool secondRingAlreadyAttacked;

    // Initial rotation
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check for sight and attack range
        playerInSightRange = distanceToPlayer <= followRadius;
        playerInAttackRange = distanceToPlayer <= attackRange;

        // NPC behavior based on the distance to the player
        if (playerInSightRange && !playerInAttackRange) FollowPlayer();
        if (playerInAttackRange) AttackPlayer();
    }

    private void FollowPlayer()
    {
        // Get the current position
        Vector3 currentPosition = transform.position;

        // Get the player position but keep the current y value
        Vector3 targetPosition = new Vector3(player.position.x, currentPosition.y, player.position.z);

        // Move towards the player position in the x and z axes only
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

        // Spawn projectiles if not already attacking
        if (!firstRingAlreadyAttacked)
        {
            SpawnProjectileRing(firstRingProjectilePrefab, firstRingProjectileSpeed, firstRingProjectileRadius);
            firstRingAlreadyAttacked = true;
            Invoke(nameof(ResetFirstRingAttack), firstRingTimeBetweenAttacks);
        }

        if (!secondRingAlreadyAttacked)
        {
            SpawnProjectileRing(secondRingProjectilePrefab, secondRingProjectileSpeed, secondRingProjectileRadius);
            secondRingAlreadyAttacked = true;
            Invoke(nameof(ResetSecondRingAttack), secondRingTimeBetweenAttacks);
        }

        // Maintain initial rotation
        transform.rotation = initialRotation;
    }

    private void AttackPlayer()
    {
        // Spawn projectiles if not already attacking
        if (!firstRingAlreadyAttacked)
        {
            SpawnProjectileRing(firstRingProjectilePrefab, firstRingProjectileSpeed, firstRingProjectileRadius);
            firstRingAlreadyAttacked = true;
            Invoke(nameof(ResetFirstRingAttack), firstRingTimeBetweenAttacks);
        }

        if (!secondRingAlreadyAttacked)
        {
            SpawnProjectileRing(secondRingProjectilePrefab, secondRingProjectileSpeed, secondRingProjectileRadius);
            secondRingAlreadyAttacked = true;
            Invoke(nameof(ResetSecondRingAttack), secondRingTimeBetweenAttacks);
        }

        // Maintain initial rotation
        transform.rotation = initialRotation;
    }

    private void SpawnProjectileRing(GameObject projectilePrefab, float projectileSpeed, float projectileRadius)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(transform, projectileSpeed, projectileRadius);
    }

    private void ResetFirstRingAttack()
    {
        firstRingAlreadyAttacked = false;
    }

    private void ResetSecondRingAttack()
    {
        secondRingAlreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}
