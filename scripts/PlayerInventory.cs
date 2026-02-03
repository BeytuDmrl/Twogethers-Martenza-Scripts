using UnityEngine;
using UnityEngine.UI; // UI elementlerini kontrol etmek için gerekli

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;
    public GameObject keyTextObj; // Inspector'dan hazýrladýðýn Text objesini buraya sürükle
    private GameObject currentKey = null;

    void Update()
    {
        // Anahtarýn yanýndaysak ve E'ye basarsak
        if (currentKey != null && Input.GetKeyDown(KeyCode.E))
        {
            PickUpKey();
        }
    }

    void PickUpKey()
    {
        hasKey = true;
        Debug.Log("Anahtar toplandý!");

        if (keyTextObj != null)
        {
            keyTextObj.SetActive(true);
            if (keyTextObj.GetComponent<Text>() != null)
                keyTextObj.GetComponent<Text>().text = "Anahtar Alýndý!";

            Invoke("HideText", 3f);
        }
        else
        {
            Debug.LogError("HATA: Player üzerinde KeyTextObj atanmamýþ!");
        }

        Destroy(currentKey);
        currentKey = null;
    }

    void HideText()
    {
        if (keyTextObj != null) keyTextObj.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key") && !hasKey)
        {
            currentKey = other.gameObject;
            Debug.Log("Anahtarý almak için E'ye bas.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            currentKey = null;
        }
    }
}