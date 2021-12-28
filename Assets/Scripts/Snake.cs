using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public Vector2 direction = Vector2.right;
    public int initialSize = 4;

    private void Start()
    {
        Time.fixedDeltaTime = 0.08f;
        ResetState();
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                direction = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) direction = Vector2.down;
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                direction = Vector2.right;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (var i = _segments.Count - 1; i > 0; i--) _segments[i].position = _segments[i - 1].position;

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        var x = Mathf.Round(transform.position.x) + direction.x;
        var y = Mathf.Round(transform.position.y) + direction.y;

        transform.position = new Vector2(x, y);
    }

    public void Grow()
    {
        var segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    public void ResetState()
    {
        direction = Vector2.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (var i = 1; i < _segments.Count; i++) Destroy(_segments[i].gameObject);

        // Clear the list but add back this as the head
        _segments.Clear();
        _segments.Add(transform);

        // -1 since the head is already in the list
        for (var i = 0; i < initialSize - 1; i++) Grow();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
            Grow();
        else if (other.tag == "Obstacle") ResetState();
    }
}