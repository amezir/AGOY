using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAi : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private NavMeshAgent agent;

    [SerializeField]
    private float baseSpeed = 6f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
    }

    ///<summary>
    /// Update the agent's destination to follow the player and adjust its rotation to face the player
    ///</summary>
    void Update()
    {
        agent.destination = playerTransform.position;
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0);
        agent.transform.rotation = targetRotation;
    }

    ///<summary>
    /// Check if the enemy collides with the player and trigger the "Lose" scene
    ///</summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Lose");
            Cursor.visible = true;
        }
    }

    ///<summary>
    /// Increase the agent's speed by the specified amount
    ///</summary>
    public void IncreaseSpeed(float speedIncreaseAmount)
    {
        agent.speed += speedIncreaseAmount;
    }
}
