using UnityEngine;

public enum BirdAbilityType
{
    None,
    Bouncy,
    Heavy,
    Bomb,
    TripleShot
}

public class Birds : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] private BirdAbilityType abilityType = BirdAbilityType.None;
    [SerializeField] private float baseHitDamage = 20f;

    [Header("Bouncy Bird")]
    [SerializeField] private float bounceVelocityMultiplier = 1.15f;

    [Header("Heavy Bird")]
    [SerializeField] private float heavyMassMultiplier = 2.2f;
    [SerializeField] private float heavyDamageMultiplier = 2.0f;

    [Header("Bomb Bird")]
    [SerializeField] private float bombRadius = 3f;
    [SerializeField] private float bombDamage = 75f;
    [SerializeField] private float bombForce = 30f;
    [SerializeField] private GameObject explosionParticlePrefab;

    [Header("Triple Bird")]
    [SerializeField] private GameObject tripleBirdPrefab;
    [SerializeField] private float tripleSpreadAngle = 12f;
    [SerializeField] private float tripleMinimumSpawnSpeed = 8f;
    [SerializeField] private float tripleVelocityEpsilon = 0.5f;

    [Header("Activation")]
    [SerializeField] private KeyCode activateAbilityKey = KeyCode.E;

    private Rigidbody2D rb;
    private Collider2D birdCollider;
    private SpriteRenderer spriteRenderer;
    private bool isLaunched;
    private bool abilityUsed;
    private float defaultMass;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        birdCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true; 

        if (rb != null)
        {
            defaultMass = rb.mass;
        }
    }

    private void Update()
    {
        if (!isLaunched || abilityUsed)
        {
            return;
        }

        if (Input.GetKeyDown(activateAbilityKey))
        {
            if (abilityType == BirdAbilityType.Bomb)
            {
                Explode();
            }
            else if (abilityType == BirdAbilityType.TripleShot)
            {
                SpawnExtraBirds();
                abilityUsed = true;
            }
        }
    }

    public void OnLaunched()
    {
        isLaunched = true;
        abilityUsed = false;

        if (rb != null)
        {
            rb.simulated = true;
            rb.mass = (abilityType == BirdAbilityType.Heavy) ? defaultMass * heavyMassMultiplier : defaultMass;
        }
    }

    public void ResetBird()
    {
        isLaunched = false;
        abilityUsed = false;

        if (birdCollider != null)
        {
            birdCollider.enabled = true;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        if (rb != null)
        {
            rb.simulated = true;
            rb.mass = defaultMass;
        }
    }

    public void MarkAsTripleSpawn()
    {
        if (abilityType == BirdAbilityType.TripleShot)
        {
            abilityType = BirdAbilityType.None;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isLaunched)
        {
            return;
        }

        BuildingHealth building = collision.collider.GetComponent<BuildingHealth>();
        if (building != null)
        {
            float damage = baseHitDamage;
            if (abilityType == BirdAbilityType.Heavy)
            {
                damage *= heavyDamageMultiplier;
            }

            building.ApplyDamage(damage);
        }

        if (abilityType == BirdAbilityType.Bouncy && collision.contactCount > 0 && rb != null)
        {
            Vector2 normal = collision.GetContact(0).normal;
            Vector2 bounceVelocity = Vector2.Reflect(rb.linearVelocity, normal) * bounceVelocityMultiplier;
            rb.linearVelocity = bounceVelocity;
        }
    }

    private void SpawnExtraBirds()
    {
        if (tripleBirdPrefab == null || rb == null)
        {
            return;
        }

        SpawnSideBird(-tripleSpreadAngle);
        SpawnSideBird(tripleSpreadAngle);
    }

    private void SpawnSideBird(float angleOffset)
    {
        GameObject newBird = Instantiate(tripleBirdPrefab, transform.position, transform.rotation);
        Birds birdScript = newBird.GetComponent<Birds>();
        Rigidbody2D newBirdRb = newBird.GetComponent<Rigidbody2D>();

        if (newBirdRb != null && rb != null)
        {
            Vector2 baseVelocity = rb.linearVelocity;
            
            if (baseVelocity.magnitude < tripleMinimumSpawnSpeed)
            {
                baseVelocity = Vector2.right * tripleMinimumSpawnSpeed;
            }

            Vector2 launchVelocity = Quaternion.Euler(0f, 0f, angleOffset) * baseVelocity;
            newBirdRb.bodyType = RigidbodyType2D.Dynamic;
            newBirdRb.gravityScale = rb.gravityScale;
            newBirdRb.simulated = true;
            newBirdRb.linearVelocity = launchVelocity;
        }

        if (birdScript != null)
        {
            birdScript.MarkAsTripleSpawn();
            birdScript.OnLaunched();
        }
    }

    private void Explode()
    {
        abilityUsed = true;
        isLaunched = false;

        if (explosionParticlePrefab != null)
        {
            Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, bombRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit == birdCollider)
            {
                continue;
            }

            BuildingHealth building = hit.GetComponent<BuildingHealth>();
            if (building != null)
            {
                building.ApplyDamage(bombDamage);
            }

            Rigidbody2D hitRb = hit.attachedRigidbody;
            if (hitRb != null)
            {
                Vector2 direction = (hitRb.worldCenterOfMass - rb.worldCenterOfMass).normalized;
                hitRb.AddForce(direction * bombForce, ForceMode2D.Impulse);
            }
        }

        if (birdCollider != null)
        {
            birdCollider.enabled = false;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (abilityType != BirdAbilityType.Bomb)
        {
            return;
        }

        Gizmos.color = new Color(1f, 0.3f, 0.2f, 0.4f);
        Gizmos.DrawSphere(transform.position, bombRadius);
    }
}
