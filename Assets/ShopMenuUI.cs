using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ShopMenuUI : MonoBehaviour
{
    public static ShopMenuUI Instance;

    public GameObject menuPanel;
    public TextMeshProUGUI baslikText;
    public TextMeshProUGUI stokText;
    public TextMeshProUGUI fiyatText;

    private StockManager aktifStok;
    private bool menuAcik = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!menuAcik) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(
                menuPanel.GetComponent<RectTransform>(),
                Mouse.current.position.ReadValue(),
                null))
            {
                MenuKapat();
            }
        }
    }

    public void MenuAc(StockManager stok)
    {
        if (stok == null) return;

        aktifStok = stok;
        menuAcik = true;
        menuPanel.SetActive(true);
        MenuGuncelle();
    }

    public void MenuKapat()
    {
        menuAcik = false;
        menuPanel.SetActive(false);
        aktifStok = null;
    }

    public void StokAl()
    {
        if (aktifStok == null) return;
        aktifStok.StokYenile();
        MenuGuncelle();
    }

    private void MenuGuncelle()
    {
        baslikText.text = aktifStok.urunAdi;
        stokText.text = "Stok: " + aktifStok.stok + "/" + aktifStok.maksStok;
        fiyatText.text = "Fiyat: $" + aktifStok.fiyat;
    }
}