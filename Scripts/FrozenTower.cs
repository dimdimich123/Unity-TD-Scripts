using UnityEngine;

public class FrozenTower : MagicTower, IEffect
{
    [SerializeField] private Effect _effect;

    public Effect GetEffect => _effect;
}
