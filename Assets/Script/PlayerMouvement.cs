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

    public Transform groundCheckLeft;
    public Transform groundCheckRight;


    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalmovement;

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position); //on crée la zone sur la quelle on detectera qu'il touchera le sol
        horizontalmovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; //tant qu'on appuis sur les fleches directionnel

        //on déplace le joueur
        MovePlayer(horizontalmovement);

    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) //si on appuis sur espace et que le perso touche le sol alors..
        {
            isJumping = true;
        }
        float CharacterVelocity = Mathf.Abs(rb.velocity.x); //renvoyer une valeur positive pour le déplacement même à gauche (calcule la valeur absolu)
        animator.SetFloat("Speed", CharacterVelocity);
    }

    void MovePlayer(float _horizontalmovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalmovement, rb.velocity.y); //on lui crée un mouvement horizontal
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
}
