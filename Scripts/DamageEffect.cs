using UnityEngine;

public abstract class DamageEffect : Effect
{
    [SerializeField] protected int _damage;

    [SerializeField] [Range(0, 10)] protected float _reloading;
}
