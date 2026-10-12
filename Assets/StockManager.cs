using UnityEngine;
using TMPro;

public class StockManager : MonoBehaviour
{
    public static StockManager Instance;

    public string urunAdi = "Ýçecek";
    public int stok = 10;
    public int maksStok = 10;        // Maksimum stok
    public int yenilemeMaliyeti = 10; // Kaç paraya stok alýnýr
    public int yenilemeAdedi = 5;    // Bir seferde kaç stok eklenir
    public TextMeshProUGUI stokText;
    public int fiyat = 5;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StokGuncelle();
    }

    public bool UrunAl()
    {
        if (stok <= 0) return false;
        stok--;
        StokGuncelle();
        return true;
    }

    public bool StokVarMi()
    {
        return stok > 0;
    }

    // UI butonuna baðlanacak metod
    public void StokYenile()
    {
        // Para yeterli mi?
        if (MoneyManager.Instance.ParaYeterlimi(yenilemeMaliyeti))
        {
            // Maksimum stok aþýlmasýn
            int eklenecek = Mathf.Min(yenilemeAdedi, maksStok - stok);
            if (eklenecek <= 0)
            {
                Debug.Log("Stok zaten dolu!");
                return;
            }

            MoneyManager.Instance.ParaHarca(yenilemeMaliyeti);
            stok += eklenecek;
            StokGuncelle();
        }
        else
        {
            Debug.Log("Yeterli paran yok!");
        }
    }

    private void StokGuncelle()
    {
        if (stokText != null)
            stokText.text = urunAdi + ": " + stok + "/" + maksStok;
    }
}