using UnityEngine;
using System.Collections;

abstract public class Effect : ScriptableObject, System.IEquatable<Effect>
{
    [SerializeField] [Range(0, 50)] protected float _duration;

    [SerializeField] protected int _level;

    public abstract IEnumerator StartEffect(GameObject enemy);

    public override int GetHashCode()
    {
       return (int)(_duration + _level);
    }

    public bool Equals(Effect other)
    {
       return 
            other.GetType() == GetType();
    }

    public bool IsLevelHigher(Effect other)
    {
        return _level > other._level;
    }

    public bool IsLevelEqual(Effect other)
    {
        return _level == other._level;
    }

    public bool IsLevelHigherOrEqual(Effect other)
    {
        return _level >= other._level;
    }
}
