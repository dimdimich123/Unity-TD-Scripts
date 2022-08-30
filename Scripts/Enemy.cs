using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System;
using System.Collections;

public class Enemy : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string _name;
    [SerializeField] private int _hp;
    [SerializeField] private Armor.Type _armorType;
    [SerializeField] private float _armorCount;
    [SerializeField] private int _gold;
    [SerializeField] private Material _takeDamageMaterial;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private float _soundDelay;

    private float _currentArmorCount;
    private int _currentHP;
    private Dictionary<Attack.Type, float> _resist;
    private List<Tower> _attackTowers = new List<Tower>();
    private List<BulletMoving> _towerBullets = new List<BulletMoving>();
    private Dictionary<Type, KeyValuePair<Effect, Coroutine>> _effectsEf = new Dictionary<Type, KeyValuePair<Effect, Coroutine>>();
    private EnemyInfo _info;
    private Coroutine _takeDamageCoroutine;
    private SpriteRenderer[] _sprites;
    private AudioSource _audio;
    public delegate void DamageHandler(float damage);
    public event DamageHandler _onTakeDamage;


    public EnemyInfo GetInfo()
    {
        if(_info == null)
        {
            string speed = GetComponent<EnemyMoving>().Speed.ToString();
            _info = new EnemyInfo(_name, speed, _armorType.ToString(), _armorCount.ToString(), _hp.ToString(), _gold.ToString());
            return _info;
        }
        return _info;
    }

    private void Start()
    {
        _resist = Resist.GetResist(_armorType);
        _currentArmorCount = _armorCount;
        _currentHP = _hp;
        _sprites = GetComponentsInChildren<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
    }


    public void ChangeArmorOnPercent(float value) 
    {
        _currentArmorCount -= _currentArmorCount * value / 100;
    }

    public void UpdateArmor() => _currentArmorCount = _armorCount;

    public void DecreaseHealthPoints(int value)
    {
        if (_currentHP > 0)
        {
            _currentHP -= value;
            _onTakeDamage?.Invoke(_currentHP / (float)_hp);
            CheckForDead();
        }
    }

    public void AddTower(Tower tower)
    {
        _attackTowers.Add(tower);
    }

    public void AddBullet(BulletMoving bullet)
    {
        _towerBullets.Add(bullet);
    }

    private void TakeDamage(Attack.Type AttackType, int damage)
    {
        _currentHP -= (int)(damage * (1 - _resist[AttackType]) * (1 - Mathf.Sqrt(_currentArmorCount / 50f)));
        _onTakeDamage?.Invoke(_currentHP / (float)_hp);
    }

    private void CheckForDead()
    {
        if (_currentHP <= 0)
        {
            foreach (BulletMoving bullet in _towerBullets) bullet.SetLastPosition(transform.position);
            LevelManager.Current.ChangeCoins(_gold);
            GetComponent<EnemyMoving>().DieAction();
            foreach (Tower tower in _attackTowers) tower.RemoveEnemyFromList(gameObject);
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 2f);

            _audio.clip = _clips[new System.Random().Next(0, _clips.Length)];
            _audio.PlayDelayed(_soundDelay / Time.timeScale);

            transform.parent.GetComponentInChildren<SpawnEnemies>()?.IncreaseCountDestroyedEnemies();
        }
    }

    public void DamageCalculation(Tower tower)
    {
        if (_currentHP > 0)
        {
            if(_takeDamageCoroutine == null)
            {
                _takeDamageCoroutine = StartCoroutine(TakeDamageEffect());
            }
            else
            {
                StopCoroutine(_takeDamageCoroutine);
                _takeDamageCoroutine = StartCoroutine(TakeDamageEffect());
            }
            int damage = tower.DamageValue;
            Attack.Type AttackType = tower.AttackType;

            if (tower is IEffect towerWithEffect)
                SetEffect(towerWithEffect);

            TakeDamage(AttackType, damage);
            CheckForDead();
        }
    }

    private void SetEffect(IEffect tower)
    {
        Effect effect = tower.GetEffect;
        Type effectType = effect.GetType();

        if (_effectsEf.Keys.Contains(effectType))
        {
            if(effect.IsLevelHigherOrEqual(_effectsEf[effectType].Key))
            {
                StopCoroutine(_effectsEf[effectType].Value);
                Coroutine coroutine = StartCoroutine(effect.StartEffect(gameObject));
                _effectsEf[effectType] = new KeyValuePair<Effect, Coroutine>(effect, coroutine);
            }
        }
        else
        {
            Coroutine coroutine = StartCoroutine(effect.StartEffect(gameObject));
            _effectsEf.Add(effectType, new KeyValuePair<Effect, Coroutine>(effect, coroutine));
        }
    }

    public void RemoveEffect(Effect effect)
    {
        _effectsEf.Remove(effect.GetType());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Bullet") && _towerBullets.Contains(collision.gameObject.GetComponent<BulletMoving>()))
        {
            Tower tower = collision.transform.parent.GetComponent<Tower>();
            DamageCalculation(tower);

            _towerBullets.Remove(collision.GetComponent<BulletMoving>());
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator TakeDamageEffect()
    {
        Color color = _takeDamageMaterial.GetColor("_Alpha");
        color.a = 1;

        foreach(SpriteRenderer sprite in _sprites)
            sprite.material = _takeDamageMaterial;
        while(color.a > 0)
        {
            color.a -= Time.deltaTime * 4;
            foreach(SpriteRenderer sprite in _sprites)
                sprite.material.SetColor("_Alpha", color);
            yield return null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        LevelManager.Current.OnEnemyClickInvoke(GetInfo());
        BackgroundClickListener.DisableAllMenu();
    }

    private void OnDestroy()
    {
        //transform.parent.GetComponentInChildren<SpawnEnemies>()?.IncreaseCountDestroyedEnemies();
    }

}

