using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;


    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position); //on crée la zone sur la quelle on detectera qu'il touchera le sol
        float horizontalmovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; //tant qu'on appuis sur les fleches directionnel

        if (Input.GetButtonDown("Jump")&& isGrounded) //si on appuis sur espace et que le perso touche le sol alors..
        {
            isJumping = true;
        }
        //on déplace le joueur
        MovePlayer(horizontalmovement);
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
    }
}
