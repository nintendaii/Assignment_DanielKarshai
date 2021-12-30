using System;
using ModestTree;
using Module.Core;
using Module.Core.MVC;
using Module.Game.Scripts.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Module.Game.Scripts.Controllers
{
    [Serializable]
    public class ScoreView : ViewBase
    {
        [SerializeField] public TMP_Text scoreText;
    }

    [Serializable]
    public class ScoreModel : ModelBase
    {
        public int currentScore;
        public int currentStreak;
        public FoodModel currentFoodModel;
    }
    public class ScoreController: ComponentControllerBase<ScoreModel, ScoreView>, IBindComponent
    {
        [Inject] private readonly UnitFoodController unitFoodController;
        private void Start()
        {
            InitializeScore();
            print($"Started score: {Model.currentFoodModel}");
        }

        public void InitializeScore()
        {
            Model.currentScore = 0;
            Model.currentStreak = 0;
            Model.currentFoodModel = unitFoodController.GetCurrentFoodModel();
            RefreshView();
        }

        public int GetScore() => Model.currentScore;

        private void RefreshView() => View.scoreText.text = Model.currentScore.ToString();
        
        public void Hit(FoodModel foodModel)
        {
            if (Model.currentFoodModel.points.Equals(foodModel.points))
            {
                Model.currentStreak += 1;
            }
            else
            {
                Model.currentFoodModel = foodModel;
                Model.currentStreak = 1;
            }

            print($"Streak: {Model.currentStreak}");
            Model.currentScore += foodModel.points * Model.currentStreak;
            RefreshView();
        }
        
    }
    
}