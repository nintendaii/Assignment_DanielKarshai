using System;
using Module.Core.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Core.SFX
{
    [Serializable]
    public class EffectMovementModel : ModelBase
    {
        [SerializeField] public EffectMovementController.Direction direction;
        [SerializeField] public float scrollSpeed = 1;

        public readonly int MainTex = Shader.PropertyToID("_MainTex");
    }

    [RequireComponent(typeof(RawImage))]
    public class EffectMovementController : ComponentControllerBase<EffectMovementModel, ViewBase>
    {
        public enum Direction
        {
            Horizontal,
            Vertical,
            CrossLeft,
            CrossRight
        }

        private RawImage rawImage;
        private float offset;
        private Action<float> onMovement;
        private Vector2 size;

        public override void Initialize()
        {
            base.Initialize();
            rawImage = GetComponent<RawImage>();
            size = rawImage.uvRect.size;

            rawImage.material.SetTextureOffset(Model.MainTex, new Vector2(0, 0));

            switch (Model.direction)
            {
                case Direction.Horizontal:
                    onMovement = OnMovementHorizontal;
                    break;
                case Direction.Vertical:
                    onMovement = OnMovementVertical;
                    break;
                case Direction.CrossLeft:
                    onMovement = OnMovementCrossLeft;
                    break;
                case Direction.CrossRight:
                    onMovement = OnMovementCrossRight;
                    break;
            }
        }

        //TODO Change Update to Service.Tick
        private void Update()
        {
            offset += Time.deltaTime * Model.scrollSpeed * 0.01f;
            offset %= 1;
            onMovement?.Invoke(offset);
        }

        private void OnMovementHorizontal(float value01)
        {
            rawImage.uvRect = new Rect(new Vector2(value01, 0), size);
        }

        private void OnMovementVertical(float value01)
        {
            rawImage.uvRect = new Rect(new Vector2(0, value01), size);
        }

        private void OnMovementCrossLeft(float value01)
        {
            rawImage.uvRect = new Rect(new Vector2(value01, value01), size);
        }

        private void OnMovementCrossRight(float value01)
        {
            rawImage.uvRect = new Rect(new Vector2(1 - value01, value01), size);
        }
    }
}