using UnityEngine;
using TMPro;

public class RatingManager : MonoBehaviour
{
    public static RatingManager Instance;

    [Header("Rating Ayarları")]
    public float rating = 2.5f;      // Başlangıç puanı
    public float maxRating = 5f;     // Maksimum puan
    public float minRating = 0f;     // Minimum puan

    [Header("UI")]
    public TextMeshProUGUI ratingText;  // "3.8" gibi sayısal gösterim
    public TextMeshProUGUI star1;       // ★ veya ☆
    public TextMeshProUGUI star2;
    public TextMeshProUGUI star3;
    public TextMeshProUGUI star4;
    public TextMeshProUGUI star5;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RatingGuncelle();
    }

    public void MusteriMemnun()
    {
        rating = Mathf.Min(rating + 0.1f, maxRating);
        RatingGuncelle();
    }

    public void MusteriMutsuz()
    {
        rating = Mathf.Max(rating - 0.2f, minRating);
        RatingGuncelle();
    }

    // Popularity olarak kullanılacak (spawn sistemi için)
    public float GetPopularity()
    {
        return Mathf.Max(rating, 0.1f);
    }

    private void RatingGuncelle()
    {
        // Sayısal gösterim
        if (ratingText != null)
            ratingText.text = rating.ToString("F1");

        // Yıldız gösterimi
        // 3.8 → 3 tam yıldız, 1 yarım, 1 boş
        AyarlaYildiz(star1, 1);
        AyarlaYildiz(star2, 2);
        AyarlaYildiz(star3, 3);
        AyarlaYildiz(star4, 4);
        AyarlaYildiz(star5, 5);
    }

    private void AyarlaYildiz(TextMeshProUGUI star, int yildizNo)
    {
        if (star == null) return;

        if (rating >= yildizNo)
        {
            star.text = "*";
            star.color = Color.yellow;
            star.fontSize = 36;
        }
        else if (rating >= yildizNo - 0.5f)
        {
            star.text = "*";
            star.color = new Color(1f, 0.6f, 0f);
            star.fontSize = 36;
        }
        else
        {
            star.text = "*";
            star.color = Color.gray;
            star.fontSize = 36;
        }
    }
}