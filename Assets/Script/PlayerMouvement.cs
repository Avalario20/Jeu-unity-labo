using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;
    private bool m_FacingRight = true;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisitonLayers;


    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalmovement;

    public static PlayerMouvement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMouvement dans la sc�ne");
            return;
        }
        instance = this;
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,collisitonLayers); //on cr�e la zone sur la quelle on detectera qu'il touchera le sol
        horizontalmovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; //tant qu'on appuis sur les fleches directionnel

        //on d�place le joueur
        MovePlayer(horizontalmovement);

    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) //si on appuis sur espace et que le perso touche le sol alors..
        {
            isJumping = true;
        }
        float CharacterVelocity = Mathf.Abs(rb.velocity.x); //renvoyer une valeur positive pour le d�placement m�me � gauche (calcule la valeur absolu)
        animator.SetFloat("Speed", CharacterVelocity);
    }

    void MovePlayer(float _horizontalmovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalmovement, rb.velocity.y); //on lui cr�e un mouvement horizontal
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity,ref velocity, .05f); //on lui donne une vitesse

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce)); //on le fait sauter
            isJumping = false; //on indique qu'il ne saute plus
        }
        // Si le jouer va a droite mais qu'il ne regarde pas vers la droite...
        if (_horizontalmovement > 0 && !m_FacingRight)
        {
            // ...flipper le jouer
            Flip();
        }
        // ou Si le jouer va a gauche mais qu'il regarde vers la droite...
        else if (_horizontalmovement < 0 && m_FacingRight)
        {
            // ...flipper le jouer
            Flip();
        }

    }
    private void Flip()
    {
        // Changer le sens du joueur
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    //on affiche la zone de detection du sol
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    public bool canAttack()
    {
        return horizontalmovement == 0 && isGrounded;
    }
}
