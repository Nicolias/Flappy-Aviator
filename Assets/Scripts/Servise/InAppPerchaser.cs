using UnityEngine;
using UnityEngine.Purchasing;

public class InAppPerchaser : MonoBehaviour
{
    [SerializeField] private CreditPanel _creditPanel;
    [SerializeField] private AdsServise _adsServise;

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

            case "7.days.without.ads":
                _adsServise.BlockAdsOnPeriods(7);
                break;

            case "30.days.without.ads":
                _adsServise.BlockAdsOnPeriods(30);
                break;
        }
    }

    private void Accure(int gold)
    {
        _creditPanel.AddCredits(gold);
    }
}
