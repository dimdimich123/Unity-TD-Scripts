using System.Collections.Generic;

public static class Resist
{
    private static readonly Dictionary<Armor.Type, Dictionary<Attack.Type, float>> ResistDictionary = new Dictionary<Armor.Type, Dictionary<Attack.Type, float>>
    {
        [Armor.Type.Normal] = new Dictionary<Attack.Type, float>
        {
            [Attack.Type.Normal] = 0.5f,
            [Attack.Type.Magical] = 0.25f,
            [Attack.Type.Chaos] = 0
        },

        [Armor.Type.Magical] = new Dictionary<Attack.Type, float>
        {
            [Attack.Type.Normal] = 0.25f,
            [Attack.Type.Magical] = 0.5f,
            [Attack.Type.Chaos] = 0
        },

        [Armor.Type.Without] = new Dictionary<Attack.Type, float>
        {
            [Attack.Type.Normal] = 0,
            [Attack.Type.Magical] = 0,
            [Attack.Type.Chaos] = 0
        }

    };

    public static Dictionary<Attack.Type, float> GetResist(Armor.Type ArmorType)
    {
        return ResistDictionary[ArmorType];
    }
}
