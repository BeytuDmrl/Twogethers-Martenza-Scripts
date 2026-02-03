using UnityEngine;

public class HackSignal : MonoBehaviour
{
    public float speed = 400f;
    public RectTransform finishTarget; // Yeşil bitiş objesini buraya sürükle
    public RectTransform[] obstacles;  // Kırmızı engelleri buraya sürükle (Liste olarak)

    private Vector3 startPosition;
    private Vector2 moveDirection = Vector2.up;
    private bool isMoving = false;
    private HackingTerminal terminal;
    private RectTransform myRect;

    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        startPosition = myRect.anchoredPosition;
        terminal = Object.FindFirstObjectByType<HackingTerminal>();
    }

    void Update()
    {
        if (!terminal.isMiniGameActive) return;

        // 1. BAŞLATMA
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.Return)) isMoving = true;
            return;
        }

        // 2. YÖN KONTROLÜ (Ok Tuşları)
        if (Input.GetKeyDown(KeyCode.UpArrow)) { moveDirection = Vector2.up; myRect.rotation = Quaternion.Euler(0, 0, 0); }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) { moveDirection = Vector2.down; myRect.rotation = Quaternion.Euler(0, 0, 180); }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) { moveDirection = Vector2.left; myRect.rotation = Quaternion.Euler(0, 0, 90); }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) { moveDirection = Vector2.right; myRect.rotation = Quaternion.Euler(0, 0, -90); }

        // 3. HAREKET
        myRect.anchoredPosition += moveDirection * speed * Time.unscaledDeltaTime;

        // 4. MESAFE KONTROLÜ (Collider Gerektirmez)
        CheckCollisions();
    }
    void CheckCollisions()
    {
        if (Vector2.Distance(myRect.anchoredPosition, finishTarget.anchoredPosition) < 30f)
        {
            isMoving = false;
            terminal.EndMiniGame(true);
        }

        // 2. ENGEL KONTROLÜ (Herhangi bir engele dokunursa yanar)
        foreach (RectTransform obs in obstacles)
        {
            if (obs != null && RectOverlaps(myRect, obs))
            {
                isMoving = false;
                Debug.Log(obs.name + " objesine çarptın!");
                terminal.EndMiniGame(false);
                break;
            }
        }
    }

    // İki UI objesinin kutularının birbirine değip değmediğini kontrol eden yardımcı fonksiyon
    bool RectOverlaps(RectTransform rect1, RectTransform rect2)
    {
        // Objelerin dünya koordinatlarındaki alanlarını hesapla
        Rect r1 = GetWorldRect(rect1);
        Rect r2 = GetWorldRect(rect2);

        // İki dikdörtgen kesişiyor mu?
        return r1.Overlaps(r2);
    }

    // UI objesinin ekrandaki tam kutusunu veren fonksiyon
    Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        // corners[0] sol alt, corners[2] sağ üst köşedir
        return new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
    }
    public void ResetGame()
    {
        isMoving = false;
        myRect.anchoredPosition = startPosition;
        myRect.rotation = Quaternion.identity;
        moveDirection = Vector2.up;
    }
}