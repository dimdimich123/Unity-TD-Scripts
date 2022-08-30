using UnityEngine;
using UnityEngine.UI;

public class InfoTable : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _attribute;
    [SerializeField] private Text _values;

    private void Start()
    {
        LevelManager.Current.OnEnemyClick += SetEnemyInfo;
        LevelManager.Current.OnTowerClick += SetTowerInfo;
    }

    public void SetEnemyInfo(EnemyInfo info)
    {
        _name.text = info.Name;
        _attribute.text = "Speed\nArmor Type\nArmor Count\nMax Health\nGold";
        _values.text = $"{info.Speed}\n{info.ArmorType}\n{info.ArmorCount}\n{info.MaxHealth}\n{info.Gold}";
    }

    public void SetTowerInfo(TowerInfo info)
    {
        _name.text = info.Name;
        _attribute.text = "Speed\nDamage\nDamageType\nRadius\nGold";
        _values.text = $"{info.Speed}\n{info.Damage}\n{info.DamageType}\n{info.Radius}\n{info.Gold}";
    }

    private void OnDestroy()
    {
        LevelManager.Current.OnEnemyClick -= SetEnemyInfo;
        LevelManager.Current.OnTowerClick += SetTowerInfo;
    }
}
