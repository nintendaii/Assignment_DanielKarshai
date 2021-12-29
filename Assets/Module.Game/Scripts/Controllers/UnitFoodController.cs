using Module.Core;
using Module.Core.MVC;
using UnityEngine;

public class UnitFoodController : ComponentControllerBase, IBindComponent
{
    public Collider2D gridArea;

    private void Start()
    {
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        var bounds = gridArea.bounds;
        var a = new Color();
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
        RandomizePosition();
    }
}