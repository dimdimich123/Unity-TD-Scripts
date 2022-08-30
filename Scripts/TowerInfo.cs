public class TowerInfo
{
    public string Name { get; private set; }
    public string Speed { get; private set; }
    public string Damage { get; private set; }
    public string DamageType { get; private set; }
    public string Radius { get; private set; }
    public string Gold { get; private set; }

    public TowerInfo(string name, string speed, string damage, string damageType, string radius, string gold)
    {
        Name = name;
        Speed = speed;
        Damage = damage;
        DamageType = damageType;
        Radius = radius;
        Gold = gold;
    }
}
