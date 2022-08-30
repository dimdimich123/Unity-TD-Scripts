using UnityEngine;

public class ChaosTower : MagicTower, IEffect
{
    [SerializeField] private Effect _effect;

    public Effect GetEffect => _effect;
}
