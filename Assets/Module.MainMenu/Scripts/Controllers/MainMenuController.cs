using System;
using Module.Core;
using Module.Core.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Module.MainMenu.Scripts.Controllers
{
    [Serializable]
    public class MenuView : ViewBase
    {
        [SerializeField] public Button startButton;
        [SerializeField] public TMP_Text highestScore;
    }

    public class MainMenuController : ComponentControllerBase<ModelBase, MenuView>, IBindComponent
    {
        private const string topScorePref = "topScore";
        private void Awake()
        {
            ShowComponent();
            View.startButton.onClick.AddListener(StartGame);
            View.highestScore.text = $"Highest score: {PlayerPrefs.GetInt(topScorePref, 0).ToString()}";
        }

        private void TopScores()
        {
            HideComponent();
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Module.Game/Scenes/Snake");
        }
    }
}