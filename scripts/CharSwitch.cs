using UnityEngine;

public class AvatarController : MonoBehaviour
{
    [Header("Avatar Referansları")]
    public GameObject redAvatar;
    public GameObject whiteAvatar;

    [Header("Kontrol Ayarları")]
    public KeyCode switchKey = KeyCode.LeftAlt;
    public Color redActiveColor = Color.red;
    public Color redInactiveColor = new Color(0.6f, 0, 0);
    public Color whiteActiveColor = Color.white;
    public Color whiteInactiveColor = new Color(0.7f, 0.7f, 0.7f);

    private Movement redMovement;
    private Movement whiteMovement;
    private SpriteRenderer redSprite;
    private SpriteRenderer whiteSprite;
    private bool isRedControlled = true;

    void Start()
    {
        redMovement = redAvatar.GetComponent<Movement>();
        whiteMovement = whiteAvatar.GetComponent<Movement>();

        redSprite = redAvatar.GetComponent<SpriteRenderer>();
        whiteSprite = whiteAvatar.GetComponent<SpriteRenderer>();

        SetControl(isRedControlled);

        Debug.Log("Sistem hazır! Başlangıçta KIRMIZI kontrol ediliyor.");
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchControl();
        }
    }

    void SwitchControl()
    {
        isRedControlled = !isRedControlled;
        SetControl(isRedControlled);
    }

    void SetControl(bool controlRed)
    {
        redMovement.SetControl(controlRed);
        whiteMovement.SetControl(!controlRed);
        UpdateColors();
    }

    void UpdateColors()
    {
        if (redSprite != null)
        {
            redSprite.color = isRedControlled ? redActiveColor : redInactiveColor;
        }

        if (whiteSprite != null)
        {
            whiteSprite.color = isRedControlled ? whiteInactiveColor : whiteActiveColor;
        }
    }
}