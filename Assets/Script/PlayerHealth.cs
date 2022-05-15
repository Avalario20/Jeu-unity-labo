using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTimeAfterHit = 3f;
    public float invinsibilityFlashDelay = 0.2f;
    public bool isInvinsible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GiveHealth(20);
        }
    }
    public void TakeDamage(int damage)
    {
        if (!isInvinsible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvinsible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }
    }
    public void Die()
    {
        PlayerMouvement.instance.enabled = false;
        PlayerMouvement.instance.animator.SetTrigger("Death");
        PlayerMouvement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMouvement.instance.playerCollider.enabled = false;
    }
    void GiveHealth(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);
    }
    //effet d'invincibilité quand le joueur prend des dégats
    public IEnumerator InvicibilityFlash()
    {
        while (isInvinsible)
        {
            graphics.color = new Color(1f, 1f,1f,0f);
            yield return new WaitForSeconds(invinsibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invinsibilityFlashDelay);

        }
    }
    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvinsible = false;
    }
}
