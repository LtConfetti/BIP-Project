using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;
    public float lifeTime = 5f;
    public float speed = 5f;
    public float radius = 3f;
    private float elapsedTime = 0f;
    private Transform npcTransform;

    public void Initialize(Transform npc, float speed, float radius)
    {
        this.npcTransform = npc;
        this.speed = speed;
        this.radius = radius;
    }

    private void Start()
    {
        // Destroy the projectile after a certain time to prevent it from existing indefinitely
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (npcTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the projectile in a circular path around the NPC
        elapsedTime += Time.deltaTime;
        float angle = elapsedTime * speed;

        float x = npcTransform.position.x + Mathf.Cos(angle) * radius;
        float z = npcTransform.position.z + Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, npcTransform.position.y, z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCombat player = collision.gameObject.GetComponent<PlayerCombat>();
            if (player != null)
            {
                player.TakeDamage((int)damage);
            }

            // Destroy the projectile upon collision
            Destroy(gameObject);
        }
    }
}