using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarpan kiþi oyuncu mu?
        PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            if (inventory.hasKey)
            {
                Debug.Log("Kapý açýlýyor...");
                OpenDoor();
            }
            else
            {
                Debug.Log("Bu kapý kilitli, bir anahtar bulmalýsýn!");
            }
        }
    }

    void OpenDoor()
    {
        // Kapýyý yok etmek yerine devredýþý býrakýyoruz (Görünmez olur ve geçilebilir)
        gameObject.SetActive(false);
    }
}