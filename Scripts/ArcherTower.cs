using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ArcherTower : Tower
{
    [SerializeField] protected GameObject _bullet;
    [SerializeField] private Transform _archer1;
    [SerializeField] private Transform _archer2;

    private Dictionary<string, string> _aliasAnimationNames = new Dictionary<string, string>();

    private AudioSource _audio;
    private ArcherData _archerData1, _archerData2;

    private class ArcherData
    {
        public Transform Transform;
        public SpriteRenderer Renderer;
        public Animator Animator;

        public ArcherData(Transform transform, SpriteRenderer renderer, Animator animator)
        {
            Transform = transform;
            Renderer = renderer;
            Animator = animator;
        }
    }

    override protected void Start()
    {
        base.Start();

        SpriteRenderer _rendererArcher1 = _archer1.GetComponent<SpriteRenderer>();
        SpriteRenderer _rendererArcher2 = _archer2.GetComponent<SpriteRenderer>();

        Animator _animatorArcher1 = _archer1.GetComponent<Animator>();
        Animator _animatorArcher2 = _archer2.GetComponent<Animator>();

        AnimationClip[] _animationClips = _animatorArcher1.runtimeAnimatorController.animationClips;
        foreach (AnimationClip animClip in _animationClips)
        {
            if (animClip.name.EndsWith("Forward"))
            {
                _aliasAnimationNames["Forward"] = animClip.name;
            }
            else
            {
                _aliasAnimationNames["Behind"] = animClip.name;
            }
        }
        _audio = GetComponent<AudioSource>();

        _archerData1 = new ArcherData(_archer1, _rendererArcher1, _animatorArcher1);
        _archerData2 = new ArcherData(_archer2, _rendererArcher2, _animatorArcher2);
    }

    private void PlayAnimationArcher(Animator animatorArcher, SpriteRenderer rendererArcher, Transform archer)
    {
        Vector3 direction = _enemies[0] != null ? _enemies[0].transform.position - archer.position : archer.position;
        if (direction.x < 0)
            rendererArcher.flipX = true; 
        else
            rendererArcher.flipX = false;

        string stateName = direction.y < 0 ? "Forward" : "Behind";
        animatorArcher.Play(_aliasAnimationNames[stateName], -1, 0.25f);
    }

    private void Shoot(Vector3 position)
    {
        GameObject BulletNew = Instantiate(_bullet, position, Quaternion.identity);
        BulletNew.transform.parent = gameObject.transform;
        BulletNew.GetComponent<BulletMoving>().SetTarget(_enemies[0]);
        _enemies[0].GetComponent<Enemy>().AddBullet(BulletNew.GetComponent<BulletMoving>());
    }

    protected override void StartAttack()
    {
        if (_isWaiting)
        {
            StartCoroutine(Attack(_archerData1, 0));
            StartCoroutine(Attack(_archerData2, _reloading / 2));
            _isWaiting = false;
        }
    }

    private IEnumerator Attack(ArcherData archer, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        while(_enemies.Count > 0)
        {
            PlayAnimationArcher(archer.Animator, archer.Renderer, archer.Transform);
            _audio.PlayOneShot(_audio.clip, _audio.volume);
            Shoot(archer.Transform.position);
            yield return new WaitForSeconds(_reloading);
        }
        _isWaiting = true;
    }

    private void OnDestroy()
    {
        _archerData1.Animator.StopPlayback();
        _archerData2.Animator.StopPlayback();
    }
}
