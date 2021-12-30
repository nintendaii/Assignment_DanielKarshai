using UnityEngine;
using UnityEngine.UI;

namespace SmartUI.Scene
{
    public enum Axis
    {
        Horizontal,
        Vertical
    }

    public enum Corner
    {
        UpperLeft,
        UpperRight,
        LowerLeft,
        LowerRight
    }

    [AddComponentMenu("UI Enhanced/EnhancedGridLayoutGroup")]
    [RequireComponent(typeof(RectTransform))]
    public class EnhancedGridLayoutGroup : LayoutGroup
    {
        [SerializeField] private Corner startCorner = Corner.UpperLeft;
        [SerializeField] private Vector2 spacing = Vector2.zero;
        [SerializeField] private int fixColumnCount = 1;
        [SerializeField] private int fixRowCount = 1;
        [SerializeField] private Axis startAxis = Axis.Horizontal;

        public int FixColumnCount => fixColumnCount;
        public int FixRowCount => fixRowCount;
        public int FixCount => fixColumnCount + fixRowCount;

        private Vector2 cellSize;

        #region LayoutGroup

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            cellSize.x = (rectTransform.rect.size.x - padding.horizontal - (fixColumnCount - 1) * spacing.x) /
                         fixColumnCount;
            var minSpace = (cellSize.x + spacing.x) * fixColumnCount - spacing.x;
            SetLayoutInputForAxis(minSpace, minSpace, -1, 0);
        }

        public override void CalculateLayoutInputVertical()
        {
            cellSize.y = (rectTransform.rect.size.y - padding.vertical - (fixRowCount - 1) * spacing.y) / fixRowCount;
            var minSpace = (cellSize.y + spacing.y) * fixRowCount - spacing.y;
            SetLayoutInputForAxis(minSpace, minSpace, -1, 1);
        }

        public override void SetLayoutHorizontal()
        {
            SetCellsAlongAxis(0);
        }

        public override void SetLayoutVertical()
        {
            SetCellsAlongAxis(1);
        }

        #endregion

        private void SetCellsAlongAxis(int axis)
        {
            // Normally a Layout Controller should only set horizontal values when invoked for the horizontal axis
            // and only vertical values when invoked for the vertical axis.
            // However, in this case we set both the horizontal and vertical position when invoked for the vertical axis.
            // Since we only set the horizontal position and not the size, it shouldn't affect children's layout,
            // and thus shouldn't break the rule that all horizontal layout must be calculated before all vertical layout.

            if (axis == 0)
            {
                // Only set the sizes when invoked for horizontal axis, not the positions.
                for (var i = 0; i < rectChildren.Count; i++)
                {
                    var rect = rectChildren[i];

                    m_Tracker.Add(this, rect,
                        DrivenTransformProperties.Anchors |
                        DrivenTransformProperties.AnchoredPosition |
                        DrivenTransformProperties.SizeDelta);

                    rect.anchorMin = Vector2.up;
                    rect.anchorMax = Vector2.up;
                    rect.sizeDelta = cellSize;
                }

                return;
            }

            var cellCountX = fixColumnCount;
            var cellCountY = fixRowCount;
            var cornerX = (int)startCorner % 2;
            var cornerY = (int)startCorner / 2;

            int cellsPerMainAxis, actualCellCountX, actualCellCountY;
            if (startAxis == Axis.Horizontal)
            {
                cellsPerMainAxis = cellCountX;
                actualCellCountX = Mathf.Clamp(cellCountX, 1, rectChildren.Count);
                actualCellCountY = Mathf.Clamp(cellCountY, 1,
                    Mathf.CeilToInt(rectChildren.Count / (float)cellsPerMainAxis));
            }
            else
            {
                cellsPerMainAxis = cellCountY;
                actualCellCountY = Mathf.Clamp(cellCountY, 1, rectChildren.Count);
                actualCellCountX = Mathf.Clamp(cellCountX, 1,
                    Mathf.CeilToInt(rectChildren.Count / (float)cellsPerMainAxis));
            }

            var requiredSpace = new Vector2(
                actualCellCountX * cellSize.x + (actualCellCountX - 1) * spacing.x,
                actualCellCountY * cellSize.y + (actualCellCountY - 1) * spacing.y
            );
            var startOffset = new Vector2(
                GetStartOffset(0, requiredSpace.x),
                GetStartOffset(1, requiredSpace.y)
            );

            for (var i = 0; i < rectChildren.Count; i++)
            {
                int positionX;
                int positionY;
                if (startAxis == Axis.Horizontal)
                {
                    positionX = i % cellsPerMainAxis;
                    positionY = i / cellsPerMainAxis;
                }
                else
                {
                    positionX = i / cellsPerMainAxis;
                    positionY = i % cellsPerMainAxis;
                }

                if (cornerX == 1) positionX = actualCellCountX - 1 - positionX;

                if (cornerY == 1) positionY = actualCellCountY - 1 - positionY;

                SetChildAlongAxis(rectChildren[i], 0, startOffset.x + (cellSize[0] + spacing[0]) * positionX,
                    cellSize[0]);
                SetChildAlongAxis(rectChildren[i], 1, startOffset.y + (cellSize[1] + spacing[1]) * positionY,
                    cellSize[1]);
            }
        }
    }
}