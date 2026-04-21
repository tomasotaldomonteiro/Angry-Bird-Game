using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BirdCollectible : MonoBehaviour
{
    [SerializeField] private BirdAbilityType unlockBirdType = BirdAbilityType.Bomb;
    [SerializeField] private GameObject collectParticlePrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Birds bird = other.GetComponent<Birds>();
        if (bird == null || !bird.IsLaunched)
        {
            return;
        }

        BirdInventory.Unlock(unlockBirdType);

        if (collectParticlePrefab != null)
        {
            Instantiate(collectParticlePrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}

