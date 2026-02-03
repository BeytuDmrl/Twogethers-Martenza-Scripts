using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float speed = 8f;
    public float jumpForce = 16f;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Referanslar")]
    [SerializeField] private Transform groundCheck;
    
    [Header("Ses Ayarları")]
    public AudioSource audioSource; // Buraya AudioSource bileşenini sürükle
    public AudioClip jumpSound;     // Buraya zıplama ses dosyasını sürükle

    private Rigidbody2D rb;
    private Animator anim; 
    private float horizontalInput;
    private bool isFacingRight = true;
    private bool isControlled = false; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        // Eğer AudioSource component'i bu objenin üzerindeyse otomatik bulalım
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isControlled) 
        {
            ResetAnimations();
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Zıplama Kontrolü
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }

        Flip();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        if (!isControlled) return;

        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }

    void UpdateAnimations()
    {
        bool walking = horizontalInput != 0;
        anim.SetBool("isWalking", walking);
        anim.SetBool("isJumping", !IsGrounded());
    }

    void ResetAnimations()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isJumping", false);
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        // SES ÇALMA KISMI
        if (audioSource != null && jumpSound != null)
        {
            // Sesi her seferinde hafif farklı incelikte çalar (daha doğal duyulur)
            audioSource.pitch = Random.Range(0.9f, 1.1f); 
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void Flip()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    public void SetControl(bool control)
    {
        isControlled = control;
        if (!control) rb.linearVelocity = Vector2.zero;
    }
}