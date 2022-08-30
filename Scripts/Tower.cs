using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class Tower : MonoBehaviour
{
    [SerializeField] protected TowerStats _stats;

    protected int _damage;
    protected float _reloading;
    protected bool _isWaiting = true;

    protected List<GameObject> _enemies = new List<GameObject>();

    protected TowerInfo _info;

    public TowerInfo Info => _info;

    public int DamageValue => _damage;

    public void IncreaseDamage(int additionalDamage) => _damage += additionalDamage;

    public void DecreaseReloading(float reducedReloading) => _reloading -= reducedReloading;

    public Attack.Type AttackType => _stats.AttackType;

    protected virtual void Start()
    {
        UpdateInfo();
        _damage = _stats.Damage;
        _reloading = _stats.Reloading;
    }

    public void UpdateInfo()
    {
        string radius = Math.Round(GetComponent<CircleCollider2D>().radius, 2).ToString();
        string cost = GetComponent<TowerMenu>().Cost.ToString();
        _info = new TowerInfo(_stats.Name, _reloading.ToString(), _damage.ToString(), _stats.AttackType.ToString(), radius, cost);
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Enemy"))
        {
            collision.GetComponent<Enemy>().AddTower(this);
            //Enemies.Insert(0, collision.gameObject);
            _enemies.Add(collision.gameObject);
            StartAttack();
        }
    }

    protected abstract void StartAttack();

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveEnemyFromList(collision.gameObject);
    }
}
