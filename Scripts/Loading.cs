using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image _image;
    private AsyncOperation _loadingScene;

    private void Update()
    {
        if(_loadingScene != null)
        {
            _image.fillAmount = _loadingScene.progress;

            if(_loadingScene.progress >= 0.89)
            {
                _loadingScene.allowSceneActivation = true;
            }
        }
    }

    public void StartLoading(string sceneName)
    {
        _loadingScene = SceneManager.LoadSceneAsync(sceneName);
        _loadingScene.allowSceneActivation = false;
    }

}
