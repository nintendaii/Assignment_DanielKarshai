using Module.Core.MVC;
using Module.Core.SO;
using UnityEngine;
using Zenject;

namespace Module.Project.Managers {
    public class ManagerCursor : ControllerBase {
        [Inject] private readonly SoCursorContainer container;
        
        private CursorType cursorType;

        public override void Initialize() {
            base.Initialize();
            ResetCursor();
        }

        public CursorType GetCursor() => cursorType;
        
        public void SetCursor(CursorType type) {
            var cursor = container.GetValue(type.ToString()) ?? container.GetValue(CursorType.Default.ToString());
            if (cursor == null) {
                return;
            }

            Cursor.SetCursor(cursor.GetTexture(), Vector2.zero, CursorMode.Auto);
            cursorType = type;
        }

        public void ResetCursor() {
            SetCursor(CursorType.Default);
        }
    }
}