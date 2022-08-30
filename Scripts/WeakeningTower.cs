using UnityEngine;

public class WeakeningTower : MagicTower, IEffect
{
    [SerializeField] private Effect _effect;

    public Effect GetEffect => _effect;
}
