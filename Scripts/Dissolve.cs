using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private Material _material;
    private SpriteRenderer[] _sprites;

    private float _fade = 1;

    private void Start()
    {
        _sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    public void StartDissolve()
    {
        foreach (SpriteRenderer item in _sprites)
            item.material = _material;
        StartCoroutine(Dissolving());
    }

    private IEnumerator Dissolving()
    {
        yield return new WaitForSeconds(1);
        while(_fade > 0)
        {
            _fade -= Time.deltaTime;

            if(_fade <= 0)
                _fade = 0;

            foreach(SpriteRenderer sprite in _sprites)
               sprite.material.SetFloat("_Fade", _fade);

            yield return null;
        }
    }
}
