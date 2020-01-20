using UnityEngine;
using UnityEngine.UI;

public class ShopButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Button _upgradeButton = null, _refreshButton = null, _freezeButton = null;

    [SerializeField]
    private Text _upgradeCostText = null;

    private ShopCards _shopCards;

    private void Awake()
    {
        _shopCards = GetComponentInParent<ShopCards>();
        //_upgradeButton.onClick.AddListener(UpgradeTier);
        _refreshButton.onClick.AddListener(_shopCards.RefreshShop);
        //_freezeButton.onClick.AddListener(_shopCards.FreezeCards);

        //TODO: change upgrade button cost text and value every round
    }
}
