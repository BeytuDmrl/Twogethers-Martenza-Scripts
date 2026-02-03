using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MultiPlayerLevelExit : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private FadeUI fadeUI;

    private int playersInZone;
    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playersInZone++;

        if (playersInZone >= 2 && !triggered)
        {
            triggered = true;
            StartCoroutine(Transition());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playersInZone--;
    }

    IEnumerator Transition()
    {
        fadeUI.FadeOut();

        while (fadeUI.IsFading)
            yield return null;

        // --- YENİ EKLENEN KAYIT KISMI ---
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex; // Tutorial için bu 3'tür
        int nextLevelIndex = currentLevelIndex + 1; // Bir sonraki seviye 4 olur

        // Hafızadaki mevcut rekoru al, eğer yeni geçtiğimiz seviye daha büyükse kaydet
        int reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 3);
        if (nextLevelIndex > reachedLevel)
        {
            PlayerPrefs.SetInt("ReachedLevel", nextLevelIndex);
            PlayerPrefs.Save(); // Veriyi diske yaz
            Debug.Log("Sistem: Yeni seviye kilidi açıldı! Kayıt: " + nextLevelIndex);
        }
        // -------------------------------

        SceneManager.LoadScene(nextSceneName);
    }
}