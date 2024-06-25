using System.Collections.Generic;
using UnityEngine;

public class EnemyBox : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                    enemy.GetComponent<EnemyBehaviour>().PlayerInBox = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                    enemy.GetComponent<EnemyBehaviour>().PlayerInBox = false;
            }
        }
    }

}
