using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAi : MonoBehaviour
{
    public Transform playerTransform;
    NavMeshAgent agent;

    [SerializeField]
    private float baseSpeed = 7f; // Vitesse de déplacement de base de l'agent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed; // Définir la vitesse de l'agent sur la vitesse de base
    }

    void Update()
    {
        agent.destination = playerTransform.position;

        // Calculer la direction vers le joueur
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0f; // Ignorer la composante Y pour éviter d'incliner l'agent

        // Calculer la rotation pour faire face au joueur (avec un décalage de 90 degrés)
        Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0);

        // Appliquer la rotation à l'agent
        agent.transform.rotation = targetRotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'agent entre en collision avec le joueur
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has been caught");
        }
    }

    // Fonction pour augmenter la vitesse de l'agent
    public void IncreaseSpeed(float speedIncreaseAmount)
    {
        agent.speed += speedIncreaseAmount; // Augmenter la vitesse de l'agent de la quantité spécifiée
    }
}
