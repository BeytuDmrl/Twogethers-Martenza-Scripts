using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    public DoorController door; // Kapıyı buraya sürükle
    public KeyCode interactKey = KeyCode.E;

    private bool isPressed = false;
    private bool isPlayerInside = false;
    private GameObject playerInZone;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Buton başlangıçta basılmadığı için kırmızı yapıyoruz
        spriteRenderer.color = Color.red; 
    }

    void Update()
    {
        if (isPlayerInside && !isPressed)
        {
            AvatarController controller = Object.FindFirstObjectByType<AvatarController>();

            if (controller != null && IsCurrentPlayer(controller) && Input.GetKeyDown(interactKey))
            {
                PressButton();
            }
        }
    }

    bool IsCurrentPlayer(AvatarController controller)
    {
        return playerInZone == controller.redAvatar || playerInZone == controller.whiteAvatar;
    }

    void PressButton()
    {
        isPressed = true;
        door.AddButton();
        // Butona basılınca default (beyaz) rengine döner
        spriteRenderer.color = Color.white; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            playerInZone = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            if (isPressed)
            {
                isPressed = false;
                door.RemoveButton();
                // Oyuncu butondan ayrılınca tekrar kırmızıya döner
                spriteRenderer.color = Color.red;
            }
        }
    }
}