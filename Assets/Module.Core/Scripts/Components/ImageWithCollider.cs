using UnityEngine;
using UnityEngine.UI;

namespace Module.Core.Components
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class ImageWithCollider : RawImage
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            var mainRect = GetPixelAdjustedRect();
            var polygonCollider2D = GetComponent<PolygonCollider2D>();
            if (polygonCollider2D != null)
            {
                polygonCollider2D.pathCount = 1;
                GenerateCollider(polygonCollider2D, mainRect.x, mainRect.y, mainRect.x + mainRect.width,
                    mainRect.y + mainRect.height, 0);
            }
        }

        private static void GenerateCollider(PolygonCollider2D collider2d, float xMin, float yMin, float xMax,
            float yMax, int pathIndex)
        {
            collider2d.SetPath(pathIndex, new[]
            {
                new Vector2(xMin, yMin),
                new Vector2(xMin, yMax),
                new Vector2(xMax, yMax),
                new Vector2(xMax, yMin)
            });
        }
    }
}