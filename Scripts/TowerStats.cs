using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Stats", menuName = "Tower Stats", order = 1)]
public class TowerStats : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Attack.Type _attackType;
    [SerializeField] private int _damage;
    [SerializeField] private float _reloading;

    public string Name => _name;

    public Attack.Type AttackType => _attackType;

    public int Damage => _damage;

    public float Reloading => _reloading;
}
