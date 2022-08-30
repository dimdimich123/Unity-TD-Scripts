using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private Image _waveButtonImage;
    [SerializeField] private Text _waveCountText;
    [SerializeField] private List<WaveOfEnemies> _enemies;
    [SerializeField] private float _timeBetWeenWave;
    [SerializeField] private AudioSource _audio;
    private int _waveCounter = 0;
    private Coroutine _nextWaveCoroutine = null;
    private int _countEnemies = 0;
    private int _countDestroyedEnemies = 0;

    private void Start()
    {
        _waveCountText.text = "0/" + _enemies.Count;
    }

    private IEnumerator SpawnWaveOfEnemies(WaveOfEnemies wave)
    {
        _audio.Play();
        List<EnemyEntry> enemies = wave.Enemies;
        float spawnTime = wave.SpawnTime;
        int count = 0;
        int index = 0;
        while(index < enemies.Count)
        {
            yield return new WaitForSeconds(spawnTime);
            GameObject NewEnemy = Instantiate(enemies[index].Enemy, transform.position, Quaternion.identity);
            NewEnemy.transform.parent = transform.parent;
            count++;
            if (count >= enemies[index].EnemyCount)
            {
                count = 0;
                index++;
            }
        }
    }

    private IEnumerator SetTimerToNextWave()
    {
        float time = _timeBetWeenWave;
        while(time > 0)
        {
            time -= Time.deltaTime;
            _waveButtonImage.fillAmount = time / _timeBetWeenWave;
            yield return null;
        }
        StartNextWave();
    }

    public void StartNextWave()
    {
        if(_nextWaveCoroutine != null)
            StopCoroutine(_nextWaveCoroutine);

        _waveButtonImage.transform.parent.gameObject.SetActive(false);
        _waveButtonImage.fillAmount = 1f;
        _waveCountText.text = (_waveCounter + 1) + "/" + _enemies.Count;

        _countEnemies = _enemies[_waveCounter].EnemiesCount();
        _countDestroyedEnemies = 0;
        StartCoroutine(SpawnWaveOfEnemies(_enemies[_waveCounter]));
    }

    public void IncreaseCountDestroyedEnemies()
    {
        _countDestroyedEnemies++;
        CheckEndOfWave();
    }

    private void CheckEndOfWave()
    {
        if(_countDestroyedEnemies >= _countEnemies && _waveButtonImage != null)
        {
            _waveCounter++;
            if(_waveCounter < _enemies.Count)
            {
                
                _waveButtonImage.transform.parent.gameObject.SetActive(true);
                _nextWaveCoroutine = StartCoroutine(SetTimerToNextWave());
            }
            else
            {
                LevelManager.Current.ShowWinPanel();
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
