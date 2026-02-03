using UnityEngine;

public class BoxCutter : MonoBehaviour
{
    [Header("Ayarlar")]
    public Sprite[] stages; // Kırılma aşaması resimleri
    public GameObject authorizedAvatar; // Kırabilecek karakter (Sürükle-Bırak)

    private int currentHit = 0;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Başlangıç kontrolü
        if (stages.Length > 0) spriteRenderer.sprite = stages[0];
        if (authorizedAvatar == null) Debug.LogError(gameObject.name + " üzerindeki authorizedAvatar boş! Lütfen karakteri sürükle.");
    }

    // DURUM 1: Eğer kutu "Is Trigger" işaretliyse çalışır
    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleImpact(other.gameObject);
    }

    // DURUM 2: Eğer kutu "Is Trigger" işaretli DEĞİLSE (katıysa) çalışır
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleImpact(collision.gameObject);
    }

    // Ortak kontrol fonksiyonu
    private void HandleImpact(GameObject incomingObject)
    {
        // Gelen obje bizim avatarımız mı yoksa onun bir parçası mı?
        if (incomingObject == authorizedAvatar || incomingObject.transform.IsChildOf(authorizedAvatar.transform))
        {
            TakeHit();
        }
        else
        {
            Debug.Log("Kutuya vuran obje yetkili değil: " + incomingObject.name);
        }
    }

    void TakeHit()
    {
        currentHit++;
        Debug.Log("Vuruş başarılı! Kalan can: " + (stages.Length - currentHit));

        if (currentHit < stages.Length)
        {
            spriteRenderer.sprite = stages[currentHit];
        }
        else
        {
            BreakBox();
        }
    }

    void BreakBox()
    {
        Debug.Log("KUTU PARÇALANDI!");
        Destroy(gameObject);
    }
}