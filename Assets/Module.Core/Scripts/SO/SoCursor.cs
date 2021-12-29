using System;
using UnityEngine;

namespace Module.Core.SO {
    [Serializable]
    public enum CursorType {
        Default,
        Interactive,
        Dialogue,
        Question,
        Sign
    }
    
    public interface ICursor {
        CursorType GetCursorType();
        Texture2D GetTexture();
    }
    
    [CreateAssetMenu(fileName = "Cursor", menuName = "Module/Common/Cursor")]
    public class SoCursor : ScriptableObject {
        [SerializeField] private CursorData cursorData;
        
        public string GetKey() => cursorData?.GetCursorType().ToString();
        public ICursor GetData() => cursorData;
    }
    
    [Serializable]
    public class CursorData : ICursor {
        [SerializeField] private CursorType cursorType;
        [SerializeField] private Texture2D texture;
        
        public CursorType GetCursorType() => cursorType;
        public Texture2D GetTexture() => texture;
    }
}