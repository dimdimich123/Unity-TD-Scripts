using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Poison Effect", menuName = "Effect/Poison", order = 3)]
public class Poison : DamageEffect
{
    [SerializeField] private GameObject _particle;

    private void Start()
    {
        ParticleSystem ps = _particle.GetComponent<ParticleSystem>();
        ps.Stop();

        ParticleSystem.MainModule main = ps.main;
        main.duration = _duration;
 
        ps.Play();
    }

    public override IEnumerator StartEffect(GameObject enemy)
    {
        GameObject particle = null;
        ParticleSystem particleSystem = enemy.GetComponentInChildren<ParticleSystem>();
        if(particleSystem == null)
        {
            particle = Instantiate(_particle, enemy.transform);
            particle.transform.parent = enemy.transform;
        }
        else
        {
            particle = particleSystem.gameObject;
        }
        
        Enemy enemyHealthPoints = enemy.GetComponent<Enemy>();
        float time = 0;
        while(time < _duration)
        {
            yield return new WaitForSeconds(_reloading);
            enemyHealthPoints.DecreaseHealthPoints(_damage);
            time += _reloading;
        }

        particleSystem = enemy.GetComponentInChildren<ParticleSystem>();
        ParticleSystem.MainModule main = particleSystem.main;
        main.loop = false;

        Destroy(particle, main.duration + 1f);
    }
}
