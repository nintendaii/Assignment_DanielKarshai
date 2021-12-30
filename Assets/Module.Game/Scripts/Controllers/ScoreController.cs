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
        [SerializeField] public TMP_Text scoreText, streakText;
    }

    [Serializable]
    public class ScoreModel : ModelBase
    {
        public int currentScore;
        public int currentStreak;
        public FoodSchema currentFoodSchema;
    }

    public class ScoreController : ComponentControllerBase<ScoreModel, ScoreView>, IBindComponent
    {
        [Inject] private readonly UnitFoodController unitFoodController;

        private void Start()
        {
            InitializeScore();
        }

        public void InitializeScore()
        {
            Model.currentScore = 0;
            Model.currentStreak = 1;
            Model.currentFoodSchema = unitFoodController.GetCurrentFoodModel();
            RefreshView();
        }

        public int GetScore()
        {
            return Model.currentScore;
        }

        private void RefreshView()
        {
            View.scoreText.text = Model.currentScore.ToString();
            View.streakText.text = $"Streak: x{Model.currentStreak}";
        }

        public void Hit(FoodSchema foodSchema)
        {
            if (Model.currentFoodSchema.points.Equals(foodSchema.points))
            {
                Model.currentStreak += 1;
            }
            else
            {
                Model.currentFoodSchema = foodSchema;
                Model.currentStreak = 1;
            }

            Model.currentScore += foodSchema.points * Model.currentStreak;
            RefreshView();
        }
    }
}