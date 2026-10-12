using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public float baseInterval = 15f;
    public int maksKasadaMusteri = 2;

    public Transform[] urunNoktalar;    // Tüm ürün noktalarý
    public Transform cashierPoint;
    public Transform exitPoint;

    private float timer = 0f;
    private int kasadakiMusteri = 0;

    void Update()
    {
        timer += Time.deltaTime;
        float spawnTime = baseInterval / RatingManager.Instance.GetPopularity();

        if (timer >= spawnTime)
        {
            timer = 0f;
            MusteriSpawn();
        }
    }

    private void MusteriSpawn()
    {
        if (kasadakiMusteri >= maksKasadaMusteri) return;

        GameObject yeniMusteri = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerAI ai = yeniMusteri.GetComponent<CustomerAI>();
        ai.urunNoktalar = urunNoktalar;
        ai.cashierPoint = cashierPoint;
        ai.exitPoint = exitPoint;
        ai.spawner = this;
    }

    public void KasayaGeldi() { kasadakiMusteri++; }
    public void KasadanAyrildi() { kasadakiMusteri--; }
}