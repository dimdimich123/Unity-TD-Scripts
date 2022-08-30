using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private int _heartsMax;
    [SerializeField] private Text _heartsText;
    [SerializeField] private int _coins;
    [SerializeField] private Text _coinsText;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Image _starsImage;
    [SerializeField] private Sprite[] _starsSprites;

    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _loadingPanel;
    private Loading _loading;

    [Header("Sounds")]
    [SerializeField] private AudioClip _clipLoseLife;
    [SerializeField] private AudioClip[] _clipCompleteLevel;
    [SerializeField] private AudioClip[] _clipFailedLevel;
    private AudioSource _audio;
    [SerializeField] private AudioSource _backgroundMusic;

    [SerializeField] private AudioMixerGroup _mixerMusic;
    [SerializeField] private AudioMixerGroup _mixerSounds;

    private int _hearts;
    private bool _isSpeedUp = false;
    private float _currentGameSpeed = 1f;

    public static LevelManager Current { get; private set; }

    public int Coins => _coins;

    private void Awake()
    {
        Current = this;
    }

    private void Start()
    {
        _hearts = _heartsMax;
        _coinsText.text = _coins.ToString();
        _heartsText.text = _hearts.ToString();
        _loading = GetComponent<Loading>();
        _audio = GetComponent<AudioSource>();
    }

    public void ChangeCoins(int value)
    {
        _coins += value;
        _coinsText.text = _coins.ToString();
    }

    public void DecreaseHearts()
    {
        if(_hearts > 0)
        {
            _hearts--;
            _heartsText.text = _hearts.ToString();
            _audio.PlayOneShot(_clipLoseLife, _audio.volume);
            if(_hearts <= 0)
            {
                ShowLosePanel();
            }
        }
    }

    public void ShowWinPanel()
    {
        int rating = (_heartsMax - _hearts) > 2 ? 0 : 3 - (_heartsMax - _hearts);
        _starsImage.sprite = _starsSprites[rating];
        UpdateLevelState(rating);
        _backgroundMusic.Stop();
        _audio.PlayOneShot(_clipCompleteLevel[new System.Random().Next(0, _clipCompleteLevel.Length)], _audio.volume);
        Time.timeScale = 0f;
        BackgroundClickListener.DisableAllMenu();
        _winPanel.SetActive(true);
    }

    private void UpdateLevelState(int rating)
    {
        LevelsState levels;
        string path = Application.persistentDataPath + LevelsConfigure.Path;
        BinaryFormatter formatter = new BinaryFormatter();
        using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
        {
            levels = (LevelsState)formatter.Deserialize(stream);
        }

        if(rating > levels[_levelNumber - 1].Stars)
            levels[_levelNumber - 1] = new LevelInfo(rating, true, true);

        if(_levelNumber < LevelsConfigure.LevelsCount && levels[_levelNumber].IsOpen == false)
            levels[_levelNumber] = new LevelInfo(0, true, false);

        using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
        {
            formatter.Serialize(stream, levels);
        }
    }

    private void ShowLosePanel()
    {
        _backgroundMusic.Stop();
        _audio.PlayOneShot(_clipFailedLevel[new System.Random().Next(0, _clipFailedLevel.Length)], _audio.volume);
        Time.timeScale = 0f;
        BackgroundClickListener.DisableAllMenu();
        _losePanel.SetActive(true);
    }

    public void GoToMenu()
    {
        _losePanel.SetActive(false);
        _winPanel.SetActive(false);
        _loadingPanel.SetActive(true);
        Time.timeScale = 1f;
        SaveSettings();
        _loading.StartLoading("MainMenu");
    }


    public event Action<EnemyInfo> OnEnemyClick;
    public void OnEnemyClickInvoke(EnemyInfo info)
    {
        OnEnemyClick?.Invoke(info);
    }

    public event Action<TowerInfo> OnTowerClick;
    public void OnTowerClickInvoke(TowerInfo info)
    {
        OnTowerClick?.Invoke(info);
    }

    public void SpeedUp()
    {
        if(_isSpeedUp)
            Time.timeScale = 1f;
        else
            Time.timeScale = 2f;

        _isSpeedUp = !_isSpeedUp;
    }

    public void RestartLevel()
    {
        _losePanel.SetActive(false);
        _winPanel.SetActive(false);
        _loadingPanel.SetActive(true);
        Time.timeScale = 1f;
        _loading.StartLoading("Level" + _levelNumber);
    }

    public void PauseGame()
    {
        _currentGameSpeed = Time.timeScale;
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = _currentGameSpeed;
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    private void SaveSettings()
    {
        float musicValue, soundsValue;
        _mixerMusic.audioMixer.GetFloat("MusicValue", out musicValue);
        _mixerSounds.audioMixer.GetFloat("SoundsValue", out soundsValue);
        SettingsData settings = new SettingsData(musicValue, soundsValue);
        SettingsData.SerializeSettings(settings);
    }
}
