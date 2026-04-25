using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private bool destroyOnZeroHealth = true;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (damage <= 0f)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0f && destroyOnZeroHealth)
        {
            ScoreManager.Instance?.AddBuildingDestroyed();
            Destroy(gameObject);
        }
    }
    }


