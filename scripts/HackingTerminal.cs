using UnityEngine;

public class HackingTerminal : MonoBehaviour
{
    [Header("Ba�lant�lar")]
    public GameObject tabletCanvas;     // Tabletin ana objesi
    public HackSignal signalScript;     // Y�lan�n �zerindeki script
    public GameObject door;             // A��lacak kap�

    [HideInInspector] public bool isMiniGameActive = false;
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && !isMiniGameActive && Input.GetKeyDown(KeyCode.E))
        {
            StartMiniGame();
        }
    }

    void StartMiniGame()
    {
        isMiniGameActive = true;
        Time.timeScale = 0f; // D�nyay� durdur
        tabletCanvas.SetActive(true);
        signalScript.gameObject.SetActive(true);
        signalScript.ResetGame();
    }

    public void EndMiniGame(bool success)
    {
        isMiniGameActive = false;
        Time.timeScale = 1f; // D�nyay� devam ettir
        tabletCanvas.SetActive(false);

        if (success && door != null)
        {
            door.SetActive(false);
            Debug.Log("Hack Ba�ar�l�!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) isPlayerNearby = true; }
    private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Player")) isPlayerNearby = false; }
}