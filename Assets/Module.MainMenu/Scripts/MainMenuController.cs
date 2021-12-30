using System;
using Module.Core;
using Module.Core.MVC;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Module.MainMenu.Scripts.Controllers
{
    [Serializable]
    public class MenuView : ViewBase
    {
        [SerializeField] public Button startButton, topScoresButton;
    }

    public class MainMenuController : ComponentControllerBase<ModelBase, MenuView>, IBindComponent
    {
        private void Awake()
        {
            ShowComponent();
            View.startButton.onClick.AddListener(StartGame);
            View.topScoresButton.onClick.AddListener(TopScores);
        }

        private void TopScores()
        {
            throw new NotImplementedException();
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Module.Game/Scenes/Snake");
        }
    }
}