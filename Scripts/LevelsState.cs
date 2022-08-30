using System;

public static class LevelsConfigure
{
    public const int LevelsCount = 3;
    public const string Path = "/data/levels.dat";
}


[Serializable]
public class LevelsState
{
    private LevelInfo[] _levels = new LevelInfo[LevelsConfigure.LevelsCount];

    public LevelsState()
    {
        _levels[0] = new LevelInfo(0, true, false);
        for(int i = 1; i < LevelsConfigure.LevelsCount; ++i)
        {
            _levels[i] = new LevelInfo(0, false, false);
        }
    }

    public LevelInfo this[int index]
    {
        get
        {
            try
            {
                return _levels[index];
            }
            catch
            {
                throw new System.Exception("index was outside the array");
            }
        }

        set
        {
            _levels[index] = value;
        }
    }
}

[Serializable]
public struct LevelInfo
{
    public int Stars;
    public bool IsOpen;
    public bool IsComplete;

    public LevelInfo(int stars, bool isOpen, bool isComplete)
    {
        Stars = stars;
        IsOpen = isOpen;
        IsComplete = isComplete;
    }
}
