using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public TextMeshProUGUI paraText;

    private int para = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ParaGuncelle();
    }

    public void ParaKazan(int miktar)
    {
        para += miktar;
        ParaGuncelle();
    }

    public void ParaHarca(int miktar)
    {
        para -= miktar;
        ParaGuncelle();
    }

    public bool ParaYeterlimi(int miktar)
    {
        return para >= miktar;
    }

    private void ParaGuncelle()
    {
        paraText.text = "Para: $" + para;
    }
}