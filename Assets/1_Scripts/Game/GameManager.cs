using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Game")] 
    [SerializeField] private int countToFinish;
    
    [Space]
    [Header("Player")]
    [SerializeField] private Controllable _player;
    [SerializeField] private Transform _playerPos;
    [Space]
    [Header("Bot")]
    [SerializeField] private Controllable _bot;
    [SerializeField] private Transform _botPos;
    [Space]
    [Header("Ball")]
    [SerializeField] private Ball _ball;
    [SerializeField] private Transform _ballPos;

    
    [Space]
    [Header("UI")]
    [SerializeField] private Text _botText;
    [SerializeField] private Text _playerText;
    [SerializeField] private GameObject _goalUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _finishUI;
    [SerializeField] private GameObject _finishParty;
    [SerializeField] private Text _finishTextPlayer;
    [SerializeField] private Text _finishTextBot;
    [SerializeField] private Text _finishText;
    [SerializeField] private Transform _content;

    [Space] 
    [Header("Audio")]
    [SerializeField] private AudioClip _loseClip;
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _goalClip;

    private int botCount;
    private int playerCount;

    private void Start()
    {
        volume = PlayerPrefs.GetInt("VolumeMusic");
        Init();
    }
    
    public void Goal(Gate gate)
    {
        _player.SetState(Controllable.PlayerState.None);
        _bot.SetState(Controllable.PlayerState.None);
        Instantiate(_goalUI, _content);
        switch (gate)
        {
            case Gate.Bot:
                playerCount++;
                _finishText.text = "YOU WIN!";
                _finishParty.SetActive(true);
                break;
            case Gate.Player:
                botCount++;
                _finishText.text = "YOU LOSE";
                _finishParty.SetActive(false);
                break;
        }

        UpdateCount();
        AudioManager.getInstance().PlayAudio(_goalClip);
        Vibration.Vibrate(800);
        if (isFinish())
        {
            Invoke("FinishGame", 5f);
        }
        else
        {
            Invoke("Init", 5f);
            Invoke("StartNewRound", 5);
        }
    }

    private int volume;
    
    public void Pause()
    {
        _pauseUI.SetActive(true);
        _player.SetState(Controllable.PlayerState.None);
        _bot.SetState(Controllable.PlayerState.None);
        volume = PlayerPrefs.GetInt("VolumeMusic");
        AudioManager.getInstance().MisicOff(true);
    }
    
    private bool isFinish()
    {
        return botCount >= countToFinish || playerCount >= countToFinish;
    }

    private void FinishGame()
    {
        _player.SetState(Controllable.PlayerState.None);
        _bot.SetState(Controllable.PlayerState.None);
        _finishUI.SetActive(true);
        _finishTextBot.text = botCount.ToString();
        _finishTextPlayer.text = playerCount.ToString();
        AudioManager.getInstance().PlayAudio(botCount > playerCount ? _loseClip : _winClip);
    }

    private void UpdateCount()
    {
        _botText.text = botCount.ToString();
        _playerText.text = playerCount.ToString();
    }

    public void StartNewRound()
    {
        PlayerPrefs.SetInt("VolumeMusic", volume);
        _player.SetState(Controllable.PlayerState.Move);
        _bot.SetState(Controllable.PlayerState.Move);
        _ball.UnsetParent();
    }
    
    private void Init()
    {
        _player.transform.position = _playerPos.position;
        _player.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = SkinUtil.GetSkinPlayer();
        _bot.transform.position = _botPos.position;
        _bot.transform.Find("Skin").GetComponent<SpriteRenderer>().sprite = SkinUtil.GetSkinBot();
        _ball.transform.position = _ballPos.position;
        _pauseUI.SetActive(false);
        _finishUI.SetActive(false);
        UpdateCount();
    }

    public enum Gate
    {
        Player = 0,
        Bot = 1
    }
}
