using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{

    [Serializable]
    public struct PlayerData
    {
        public Image image;
        public Sprite spriteBackground;
        public Color colorOutline;
        public GameManager.Gate type;
    }

    [SerializeField] private AudioClip _clickSound;

    [SerializeField] private PlayerData[] _players;
    [SerializeField] private PlayerData _playerData;
    
    [SerializeField] private GameObject _skinPrefab;
    [SerializeField] private GameObject _skinPanel;
    [SerializeField] private Transform _content;

    private void Start()
    {
        foreach (var data in _players) SetSpritePlayer(data);

        HidePanel();
    }

    public void OpenPanel(int type)
    {
        AudioManager.getInstance().PlayAudio(_clickSound);
        _playerData = GetDataByType((GameManager.Gate) type);
        _skinPanel.SetActive(true);
        GenerateViews();
    }

    public void HidePanel()
    {
        AudioManager.getInstance().PlayAudio(_clickSound);
        _skinPanel.SetActive(false);
    }

    private void SetSpritePlayer(PlayerData data)
    {
        switch (data.type)
        {
            case GameManager.Gate.Player:
                data.image.sprite = SkinUtil.GetSkinPlayer();
                break;
            case GameManager.Gate.Bot:
                data.image.sprite = SkinUtil.GetSkinBot();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void GenerateViews()
    {
        foreach (Transform obj in _content) Destroy(obj.gameObject);
        var skins = SkinUtil.GetSkins();
        for (var i = 1; i <= skins.Count; i++)
            GenerateView(skins[i-1], int.Parse(skins[i-1].name));
    }

    private void GenerateView(Sprite sprite, int id)
    {
        var view = Instantiate(_skinPrefab, _content);
        view.transform.Find("Image").Find("Skin").GetComponent<Image>().sprite = sprite;
        view.transform.GetComponent<Image>().sprite = _playerData.spriteBackground;
        view.transform.Find("Image").GetComponent<Image>().color = _playerData.colorOutline;
        view.GetComponent<Button>().onClick.AddListener(()=>
        {
            HidePanel();
            _playerData.image.sprite = sprite;
            PlayerPrefs.SetInt(_playerData.type == GameManager.Gate.Player ?
                SkinUtil.PLAYER_PREFS : SkinUtil.BOT_PREFS, id);
        });
    }

    private PlayerData GetDataByType(GameManager.Gate type)
    {
        var data = new PlayerData();
        foreach (var player in _players)
        {
            if (player.type == type) data = player;
        }

        return data;
    }
}
