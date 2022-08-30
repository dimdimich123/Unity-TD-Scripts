using UnityEngine;
using System.Collections;
public class MagicTower : Tower
{
    [SerializeField] Transform _towerBody;

    private Animator _animator;
    private string _clipName;
    private AudioSource _audio;

    override protected void Start()
    {
        base.Start();
        _animator = _towerBody.GetComponent<Animator>();
        AnimationClip[] animationClips = _animator.runtimeAnimatorController.animationClips;
        _clipName = animationClips[0].name;

        _audio = GetComponent<AudioSource>();
    }

    private void PlayAnimation()
    {
        _animator.Play(_clipName, -1, 0.5f);
    }

    private void Shoot()
    {
        // for (int i = 0; i < _enemies.Count; ++i)
        //     _enemies[i].GetComponent<Enemy>().DamageCalculation(this);

        GameObject[] enemies = _enemies.ToArray();
        for(int i = 0; i < enemies.Length; ++i)
            if(enemies[i] != null)
                enemies[i].GetComponent<Enemy>().DamageCalculation(this);
    }

    protected override void StartAttack()
    {
        if (_isWaiting)
        {
            StartCoroutine(Attack());
            _isWaiting = false;
        }
    }

    private IEnumerator Attack()
    {
        while (_enemies.Count > 0)
        {
            PlayAnimation();
            _audio.PlayOneShot(_audio.clip, _audio.volume);
            Shoot();
            yield return new WaitForSeconds(_reloading);
        }
        _isWaiting = true;
    }

    //private void Update()
    //{
    //    if (_enemies.Count > 0)
    //    {
    //        _time += Time.deltaTime;
    //        if (_time >= _reloading)
    //        {
    //            PlayAnimation();
    //            _audio.PlayOneShot(_audio.clip, _audio.volume);
    //            Shoot();
    //            _time = 0;
    //        }
    //    }
    //}

}
