using UnityEngine;

public class EarlyLevelUpgradePanel : UpgradePanel
{
    [SerializeField] private Tower NextLevelTower;

    public void UpgradeTower()
    {
        int cost = NextLevelTower.GetComponent<TowerMenu>().Cost;
        if (cost <= LevelManager.Current.Coins)
        {
            LevelManager.Current.ChangeCoins(-cost);

            Vector3 towerPosition = new Vector3(_buildingArea.transform.position.x, _buildingArea.transform.position.y, 0);
            GameObject NewTower = Instantiate(NextLevelTower.gameObject, towerPosition, Quaternion.identity);

            int price = GetComponent<TowerMenu>().Price;
            NewTower.GetComponent<TowerMenu>().Price = price + cost;

            NewTower.GetComponent<UpgradePanel>().SetBuildingArea(_buildingArea);

            Destroy(gameObject);
        }
    }
}
