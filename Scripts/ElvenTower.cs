using UnityEngine;

public class ElvenTower : ArcherTower, IEffect
{
    [SerializeField] private Effect _effect;

    public Effect GetEffect => _effect;
}
