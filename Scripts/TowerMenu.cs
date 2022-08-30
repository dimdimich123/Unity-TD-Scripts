using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    [SerializeField] private int _cost;
    [SerializeField] private GameObject _upgradeHUD;
    [SerializeField] private GameObject _towerArea;
    [SerializeField] private Text _sellCost;

    public int Price { get; set; }

    public static TowerMenu ActiveMenu = null;

    public int Cost => _cost;

    private void Start()
    {
        UpdateSellMoney();
    }

    public void UpdateSellMoney()
    {
        _sellCost.text = (Price / 2).ToString();
    }

    public void SetActive()
    {
        if(CallUpBuildingMenu.ActiveMenu != null)
        {
            CallUpBuildingMenu.ActiveMenu.SetActiveFalse();
            CallUpBuildingMenu.ActiveMenu = null;
        }

        if (_upgradeHUD.activeSelf)
        {
            _upgradeHUD.SetActive(false);
            _towerArea.SetActive(false);
            ActiveMenu = null;
        }
        else
        {
            _upgradeHUD.SetActive(true);
            _towerArea.SetActive(true);
            if(ActiveMenu != null)
                ActiveMenu.SetActive();
            ActiveMenu = this;
        }
    }
}
