using System;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField]
    CollectiblesCount collectiblesCount;
    void Update()
    {
        // Faire tourner le collectible sur lui-même
        transform.Rotate(Vector3.up * Time.deltaTime * 100);
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifier si le joueur a ramassé le collectible
        if (other.CompareTag("Player"))
        {
            // Déclencher l'événement OnCollected
            // Collectibles.OnCollected?.Invoke();
            collectiblesCount.IncreaseCollectiblesCount();
            Debug.Log("Collectible ramassé !");
            // Détruire le collectible
            Destroy(gameObject);
        }
    }
}
