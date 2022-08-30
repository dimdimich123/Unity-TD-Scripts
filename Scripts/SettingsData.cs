using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SettingsData
{
    public const string Path = "/data/Settings.dat";
    private float _musicVolume;
    private float _soundsVolume;

    public float MusicVolume => _musicVolume;
    public float SoundsVolume => _soundsVolume;


    public SettingsData(float musicVolume, float soundsVolume)
    {
        _musicVolume = musicVolume;
        _soundsVolume = soundsVolume;
    }

    public static void SerializeSettings(SettingsData settings)
    {
        string path = Application.persistentDataPath + Path;
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, settings);
        }   
    }

    public static SettingsData DeserializeSettings()
    {
        string path = Application.persistentDataPath + Path;
        BinaryFormatter formatter = new BinaryFormatter();
        SettingsData settings;
        
        if(File.Exists(path) == false)
        {
            settings = new SettingsData(0, 0);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, settings);
            }
        }
        else
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                settings = (SettingsData)formatter.Deserialize(stream);
            }
        }
        return settings;
    }
}
