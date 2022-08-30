public class EnemyInfo
{
    public string Name { get; private set; }
    public string Speed { get; private set; }
    public string ArmorType { get; private set; }
    public string ArmorCount { get; private set; }
    public string MaxHealth { get; private set; }
    public string Gold { get; private set; }

    public EnemyInfo(string name, string speed, string armorType, string armorCount, string maxHealth, string gold)
    {
        Name = name;
        Speed = speed;
        ArmorType = armorType;
        ArmorCount = armorCount;
        MaxHealth = maxHealth;
        Gold = gold;
    }

    // private List<KeyValuePair<string, string>> _attibute = new List<KeyValuePair<string, string>>();

    // public void Add(string attribute, string value)
    // {
    //    _attibute.Add(new KeyValuePair<string, string>(attribute, value));
    // }

    // public KeyValuePair<string, string> this[int index]
    // {
    //     get
    //     {
    //         return _attibute[index];
    //     }
    // }

    // public int Count => _attibute.Count;
}
