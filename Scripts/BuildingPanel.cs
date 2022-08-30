using UnityEngine;
public class BuildingPanel : MonoBehaviour
{
    [SerializeField] private GameObject _archerTower;
    [SerializeField] private GameObject _magicTower;
    private GameObject _buildingArea;
    private float _topPadding = 2.5f;

    public void SetPosition(Vector3 Position, GameObject NewObject)
    {
        transform.position = new Vector3(Position.x, Position.y + _topPadding);
        _buildingArea = NewObject;
    }

    public void BuildArcherTower()
    {
        BuildTower(_archerTower);
    }

    public void BuildMagicTower()
    {
        BuildTower(_magicTower);
    }

    private void BuildTower(GameObject tower)
    {
        int cost = tower.GetComponent<TowerMenu>().Cost;
        if (cost <= LevelManager.Current.Coins)
        {
            LevelManager.Current.ChangeCoins(-cost);

            Vector3 towerPosition = new Vector3(_buildingArea.transform.position.x, _buildingArea.transform.position.y, 0);
            GameObject NewTower = Instantiate(tower, towerPosition, Quaternion.identity);

            NewTower.GetComponent<TowerMenu>().Price = cost;

            NewTower.GetComponent<UpgradePanel>().SetBuildingArea(_buildingArea);

            _buildingArea.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
