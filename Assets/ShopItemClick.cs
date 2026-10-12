using UnityEngine;

public class ShopItemClick : MonoBehaviour
{
    void OnMouseDown()
    {
        ShopMenuUI.Instance.MenuAc(GetComponent<StockManager>());
    }
}