using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Reduction Effect", menuName = "Effect/ArmorReduction", order = 1)]
public class ArmorReduction : Effect
{
    [SerializeField] [Range(0, 100)] private float _redactionPercent;

    public override IEnumerator StartEffect(GameObject enemy)
    {
        Enemy enemyAttribute = enemy.GetComponent<Enemy>();
        
        enemyAttribute.UpdateArmor();
        enemyAttribute.ChangeArmorOnPercent(_redactionPercent);

        yield return new WaitForSeconds(_duration);
        
        enemyAttribute.UpdateArmor();
        enemyAttribute.RemoveEffect(this);
    }
}
