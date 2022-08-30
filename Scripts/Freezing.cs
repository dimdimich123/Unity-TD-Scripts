using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Freezing Effect", menuName = "Effect/Freezing", order = 2)]
public class Freezing : Effect
{
    [SerializeField] [Range(0, 100)] private float _freezingPercent;

    public override IEnumerator StartEffect(GameObject enemy)
    {
        enemy.GetComponent<EnemyMoving>().UpdateSpeed();
        enemy.GetComponent<EnemyMoving>().ChangeSpeedOnPercent(_freezingPercent);

        foreach (SpriteRenderer sprite in enemy.GetComponentsInChildren<SpriteRenderer>())
            sprite.color = new Color(0.5f, 0.5f, 1f);

        yield return new WaitForSeconds(_duration);
        
        enemy.GetComponent<EnemyMoving>().UpdateSpeed();
        enemy.GetComponent<Enemy>().RemoveEffect(this);

        foreach (SpriteRenderer sprite in enemy.GetComponentsInChildren<SpriteRenderer>())
            sprite.color = new Color(1f, 1f, 1f);
    }
}
