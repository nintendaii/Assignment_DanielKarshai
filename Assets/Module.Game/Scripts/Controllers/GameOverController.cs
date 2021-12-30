using System;
using Module.Core;
using Module.Core.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Module.Game.Scripts.Controllers
{
    [Serializable]
    public class GameOverView : ViewBase
    {
        [SerializeField] public TMP_Text totalScoreText;
        [SerializeField] public Button okButton, restartButton;
    }

    public class GameOverController : ComponentControllerBase<ModelBase, GameOverView>, IBindComponent
    {
        [Inject] private readonly UnitSnakeController unitSnakeController;
        [Inject] private readonly ScoreController scoreController;

        private void Awake()
        {
            HideComponent();
            View.okButton.onClick.AddListener(OnOkButtonPress);
            View.restartButton.onClick.AddListener(OnRestartButtonPress);
        }

        public void Execute()
        {
            View.totalScoreText.text = $"Your score is {scoreController.GetScore()} points";
            Time.timeScale = 0;
            ShowComponent();
        }

        private void OnRestartButtonPress()
        {
            Time.timeScale = 1;
            HideComponent();
            unitSnakeController.ResetState();
        }

        private void OnOkButtonPress()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Module.MainMenu/Scenes/Main");
        }
    }
}