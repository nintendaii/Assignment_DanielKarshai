using System;
using Module.Core;
using Module.Core.MVC;
using Module.MainMenu.Scripts.Controllers;
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
        [SerializeField] public TMP_Text totalScoreText, newRecordText;
        [SerializeField] public Button okButton, restartButton;
    }

    public class GameOverController : ComponentControllerBase<ModelBase, GameOverView>, IBindComponent
    {
        [Inject] private readonly UnitSnakeController unitSnakeController;
        [Inject] private readonly ScoreController scoreController;

        private int summaryScore;

        private void Awake()
        {
            HideComponent();
            View.okButton.onClick.AddListener(OnOkButtonPress);
            View.restartButton.onClick.AddListener(OnRestartButtonPress);
        }

        public void Execute()
        {
            summaryScore = scoreController.GetScore();
            View.totalScoreText.text = $"Your score is {summaryScore} points";
            CheckForRecord();
            Time.timeScale = 0;
            ShowComponent();
        }

        private void CheckForRecord()
        {
            var topScore = TopScoreDataHandler.Load();
            if (summaryScore >= topScore && summaryScore!=0)
            {
                TopScoreDataHandler.Save(summaryScore);
                View.newRecordText.text = "New record!";
            }
            else
            {
                View.newRecordText.text = "";
            }
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