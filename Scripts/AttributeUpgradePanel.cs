using UnityEngine;
using UnityEngine.UI;

public class AttributeUpgradePanel : UpgradePanel
{
    [System.Serializable]
    private struct Attribute
    {
        public float Value;
        public int Cost;
        public int CurrentLevel;
    }

    [SerializeField] private Attribute _attackDamage;
    [SerializeField] private Attribute _reloading;
    [SerializeField] private Attribute _attackRange;

    [SerializeField] private Image[] _attackDamageLevel;
    [SerializeField] private Image[] _reloadingLevel;
    [SerializeField] private Image[] _attackRangeLevel;

    [SerializeField] private Sprite _updateLevel;
    [SerializeField] private GameObject _towerArea;

    private TowerMenu _towerMenu;
    private const float _ratio = 2.5f;

    private const int MaxLevel = 5;

    private void Start()
    {
        _towerMenu = GetComponentInChildren<TowerMenu>();
    }

    public void UpgradeDamage()
    {
        if (_attackDamage.CurrentLevel < MaxLevel && _attackDamage.Cost <= LevelManager.Current.Coins)
        {

            LevelManager.Current.ChangeCoins(-_attackDamage.Cost);
            _attackDamageLevel[_attackDamage.CurrentLevel].sprite = _updateLevel;
            _attackDamage.CurrentLevel++;

            Tower tower = GetComponent<Tower>();
            tower.IncreaseDamage((int)_attackDamage.Value);
            tower.UpdateInfo();

            _towerMenu.Price += _attackDamage.Cost;
            _towerMenu.UpdateSellMoney();
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (_reloading.CurrentLevel < MaxLevel && _reloading.Cost <= LevelManager.Current.Coins)
        {
            LevelManager.Current.ChangeCoins(-_reloading.Cost);
            _reloadingLevel[_reloading.CurrentLevel].sprite = _updateLevel;
            _reloading.CurrentLevel++;

            Tower tower = GetComponent<Tower>();
            tower.DecreaseReloading(_reloading.Value);
            tower.UpdateInfo();

            _towerMenu.Price += _reloading.Cost;
            _towerMenu.UpdateSellMoney();
        }
    }

    public void UpgradeAttackRange()
    {
        if (_attackRange.CurrentLevel < MaxLevel && _attackRange.Cost <= LevelManager.Current.Coins)
        {
            LevelManager.Current.ChangeCoins(-_attackRange.Cost);
            _attackRangeLevel[_attackRange.CurrentLevel].sprite = _updateLevel;
            _attackRange.CurrentLevel++;

            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            collider.radius += _attackRange.Value;
            GetComponent<Tower>().UpdateInfo();

            _towerMenu.Price += _attackRange.Cost;
            _towerMenu.UpdateSellMoney();

            float scale = collider.radius / _ratio;
            _towerArea.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
