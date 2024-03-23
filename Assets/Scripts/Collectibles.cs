using System;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField]
    CollectiblesCount collectiblesCount;

    ///<summary>
    /// Rotate the collectible around itself
    ///</summary>
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 100);
    }

    ///<summary>
    /// Check if the player has picked up the collectible
    ///</summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectiblesCount.IncreaseCollectiblesCount();
            Destroy(gameObject);
        }
    }
}
