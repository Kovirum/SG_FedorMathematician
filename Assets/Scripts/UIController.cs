using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image SoundStatusImage;
    public Image MusicStatusImage;

    public Sprite[] soundStatusSprites = new Sprite[2];   // 1 - включен, 0 - отключен
    public Sprite[] musicStatusSprites = new Sprite[2];

    private int _soundEnabled, _musicEnabled;

    public AudioClip ButtonClickSound;

    [Header("Game")]

    public Image PauseStatusImage;
    public Sprite[] pauseStatusSprites = new Sprite[2]; // 0 - пауза, 1 - продолжить
    public GameObject pausePanel;

    public void Awake()
    {
        _soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1);
        _musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1);

        SoundStatusImage.sprite = soundStatusSprites[_soundEnabled];
        MusicStatusImage.sprite = musicStatusSprites[_musicEnabled];
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Sound()
    {
        _soundEnabled = _soundEnabled == 0 ? 1 : 0;
        SoundStatusImage.sprite = soundStatusSprites[_soundEnabled];
        PlayerPrefs.SetInt("SoundEnabled", _soundEnabled);
        PlayerPrefs.Save();
    }

    public void Music()
    {
        _musicEnabled = _musicEnabled == 0 ? 1 : 0;
        MusicStatusImage.sprite = musicStatusSprites[_musicEnabled];
        PlayerPrefs.SetInt("MusicEnabled", _musicEnabled);
        PlayerPrefs.Save();
    }

    public void Pause()
    {
        GameController.IsGameStopped = !GameController.IsGameStopped;
        PauseStatusImage.sprite = pauseStatusSprites[GameController.IsGameStopped ? 1 : 0];
        pausePanel.SetActive(GameController.IsGameStopped);
    }

    public void OnButtonClicked()
    {
        if (_soundEnabled == 1)
            AudioSource.PlayClipAtPoint(ButtonClickSound, Vector3.zero);
    }
}
