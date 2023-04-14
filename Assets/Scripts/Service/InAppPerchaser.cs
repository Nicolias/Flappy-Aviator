using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class InAppPerchaser : MonoBehaviour
{
    [SerializeField] private CreditPanel _creditPanel;
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "20k.gold":
                Accure(20000);
                break;

            case "50k.gold":
                Accure(50000);
                break;

            case "200k.gold":
                Accure(200000);
                break;
        }
    }

    private void Accure(int gold)
    {
        _creditPanel.AddCredits(gold);
    }
}
