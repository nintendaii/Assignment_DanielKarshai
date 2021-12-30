using UnityEngine;
using UnityEngine.UI;

namespace Module.Core.Components
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class ImageCropper : Image
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            var mainRect = GetPixelAdjustedRect();

            var xMinMain = mainRect.x;
            var xMaxMain = mainRect.x + mainRect.width;
            var yMinMain = mainRect.y;
            var yMaxMain = mainRect.y + mainRect.height;

            var rectTransformCache = rectTransform;
            var offsetMin = rectTransformCache.offsetMin;
            var offsetMax = rectTransformCache.offsetMax;

            //Left
            GenerateMesh(vh, xMinMain - offsetMin.x, yMinMain - offsetMin.y, xMinMain, yMaxMain - offsetMax.y, color,
                0);
            //Right
            GenerateMesh(vh, xMaxMain, yMinMain - offsetMin.y, xMaxMain - offsetMax.x, yMaxMain - offsetMax.y, color,
                4);
            //Top
            GenerateMesh(vh, xMinMain, yMaxMain, xMaxMain, yMaxMain - offsetMax.y, color, 8);
            //Bottom
            GenerateMesh(vh, xMinMain, yMinMain - offsetMin.y, xMaxMain, yMinMain, color, 12);

            var polygonCollider2D = GetComponent<PolygonCollider2D>();
            if (polygonCollider2D != null)
            {
                polygonCollider2D.pathCount = 4;
                //Left
                GenerateCollider(polygonCollider2D, xMinMain - offsetMin.x, yMinMain - offsetMin.y, xMinMain,
                    yMaxMain - offsetMax.y, 0);
                //Right
                GenerateCollider(polygonCollider2D, xMaxMain, yMinMain - offsetMin.y, xMaxMain - offsetMax.x,
                    yMaxMain - offsetMax.y, 1);
                //Top
                GenerateCollider(polygonCollider2D, xMinMain, yMaxMain, xMaxMain, yMaxMain - offsetMax.y, 2);
                //Bottom
                GenerateCollider(polygonCollider2D, xMinMain, yMinMain - offsetMin.y, xMaxMain, yMinMain, 3);
            }
        }

        private static void GenerateMesh(VertexHelper vh, float xMin, float yMin, float xMax, float yMax,
            Color32 color32, int triangleIndex)
        {
            vh.AddVert(new Vector3(xMin, yMin), color32, Vector2.zero);
            vh.AddVert(new Vector3(xMin, yMax), color32, Vector2.zero);
            vh.AddVert(new Vector3(xMax, yMax), color32, Vector2.zero);
            vh.AddVert(new Vector3(xMax, yMin), color32, Vector2.zero);

            vh.AddTriangle(triangleIndex, triangleIndex + 1, triangleIndex + 2);
            vh.AddTriangle(triangleIndex + 2, triangleIndex + 3, triangleIndex);
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