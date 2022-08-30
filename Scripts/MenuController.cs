using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MenuController : MonoBehaviour
{
    [Header ("Panels")]
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _levelsPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Sprite[] _stars;

    [SerializeField] private AudioMixerGroup _mixerMusic;
    [SerializeField] private AudioMixerGroup _mixerSounds;

    private Loading _loadingBar;

    public void ButtonSettingsClick()
    {
        _menuPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    public void ButtonStartClick()
    {
        _menuPanel.SetActive(false);
        _levelsPanel.SetActive(true);
    }

    public void ButtonCloseLevelsClick()
    {
        _menuPanel.SetActive(true);
        _levelsPanel.SetActive(false);
    }

    public void ButtonLevelClick(int index)
    {
        _levelsPanel.SetActive(false);
        _loadingPanel.SetActive(true);
        _loadingBar.StartLoading("Level" + index);
    }

    private void Awake()
    {
        string path = Application.persistentDataPath + LevelsConfigure.Path;
        string[] directs = Application.persistentDataPath.Split('/');
        for(int i = 1; i < directs.Length; ++i)
        {
            string tempPath = string.Empty;
            for(int j = 0; j < i; ++j)
            {
                tempPath += directs[j] + "/";
            }
            tempPath.Remove(tempPath.Length - 1);
            if(Directory.Exists(tempPath) == false)
            {
                Directory.CreateDirectory(tempPath);
            }
        }
        
        if(Directory.Exists(Application.persistentDataPath + "/data") == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/data");
        }
        //Debug.Log(path);

        LevelsState levels;
        BinaryFormatter formatter = new BinaryFormatter();

        if(File.Exists(path) == false)
        {
            levels = new LevelsState();
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, levels);
            }
        }
        else
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                levels = (LevelsState)formatter.Deserialize(stream);
            }

            Button[] buttons = _levelsPanel.GetComponentsInChildren<Button>();
            for(int i = 0; i < LevelsConfigure.LevelsCount; ++i)
            {
                //Debug.Log($"Level: {i + 1}  Stars: {levels[i].Stars}   Open: {levels[i].IsOpen}   Complete: {levels[i].IsComplete}");
                Image[] images = buttons[i].GetComponentsInChildren<Image>();
                if(levels[i].IsOpen)
                {
                    buttons[i].interactable = true;
                    images[1].color = new Color(255, 255, 255, 255);
                    if(levels[i].IsComplete)
                    {
                        images[2].sprite = _stars[levels[i].Stars];
                        images[2].enabled = true;
                    }
                    else
                    {
                        images[2].enabled = false;
                    }
                }
                else
                {
                    buttons[i].interactable = false;
                    images[1].color = new Color(255, 255, 255, 127);
                    images[2].enabled = false;
                }
            }
        }
    }

    private void Start()
    {
        _loadingBar = GetComponent<Loading>();

        SettingsData settings = SettingsData.DeserializeSettings();
        _mixerMusic.audioMixer.SetFloat("MusicValue", settings.MusicVolume);
        _mixerSounds.audioMixer.SetFloat("SoundsValue", settings.SoundsVolume);
    }

    // private void OnApplicationQuit()
    // {
    //     float musicValue, soundsValue;
    //     _mixerMusic.audioMixer.GetFloat("MusicValue", out musicValue);
    //     _mixerSounds.audioMixer.GetFloat("SoundsValue", out soundsValue);
    //     SettingsData settings = new SettingsData(musicValue, soundsValue);
    //     SettingsData.SerializeSettings(settings);
    // }
}
