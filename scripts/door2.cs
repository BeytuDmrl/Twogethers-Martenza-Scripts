using UnityEngine;

public class Door2 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarpan kişi oyuncu mu?
        PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            if (inventory.hasKey)
            {
                Debug.Log("Kapı açılıyor...");
                OpenDoor();
            }
            else
            {
                Debug.Log("Bu kapı kilitli, bir anahtar bulmalısın!");
            }
        }
    }

    void OpenDoor()
    {
        // Kapıyı yok etmek yerine devredışı bırakıyoruz (Görünmez olur ve geçilebilir)
        gameObject.SetActive(false);
    }
}