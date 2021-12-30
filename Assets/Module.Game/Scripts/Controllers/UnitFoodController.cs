using System;
using Module.Core.MVC;
using Module.Game.Scripts.Controllers;
using Module.Game.Scripts.Models;
using UnityEngine;
using Zenject;
using IBindComponent = Module.Game.Scripts.IBindComponent;
using Random = UnityEngine.Random;

[Serializable]
public class UnitFoodView : ViewBase
{
    [SerializeField] public SpriteRenderer sprite;
}

public class UnitFoodController : ComponentControllerBase<ModelBase, UnitFoodView>, IBindComponent
{
    [Inject] private readonly FoodDataController foodDataController;
    [Inject] private readonly ScoreController scoreController;

    public Collider2D gridArea;
    private FoodSchema _currentFoodSchema;

    private void Awake()
    {
        RandomizePosition();
        RandomizeFoodType();
    }

    private void RandomizeFoodType()
    {
        _currentFoodSchema = foodDataController.GetRandomFood();
        var colorData = _currentFoodSchema.color;
        View.sprite.color = new Color(colorData[0], colorData[1], colorData[2], colorData[3]);
    }

    public FoodSchema GetCurrentFoodModel()
    {
        return _currentFoodSchema;
    }

    private void RandomizePosition()
    {
        var bounds = gridArea.bounds;
        // Pick a random position inside the bounds
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        scoreController.Hit(_currentFoodSchema);
        RandomizePosition();
        RandomizeFoodType();
    }
}