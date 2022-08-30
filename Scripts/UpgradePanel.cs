using UnityEngine;

public abstract class UpgradePanel : MonoBehaviour
{
    protected GameObject _buildingArea;

    public void SetBuildingArea(GameObject BuildingArea) => _buildingArea = BuildingArea;

    public void SellTower()
    {
        int cost = GetComponent<TowerMenu>().Price;
        LevelManager.Current.ChangeCoins(cost / 2);

        _buildingArea.SetActive(true);
        _buildingArea.GetComponent<CallUpBuildingMenu>().PlaySellTowerSound();

        Destroy(gameObject);
    }
}
