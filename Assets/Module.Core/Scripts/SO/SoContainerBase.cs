using Module.Core.Utilities;
using UnityEngine;
using Zenject;

namespace Module.Core.SO {
    public abstract class SoContainerBase : ScriptableObjectInstaller {
        [SerializeField] protected FolderReference folderPath;
        
        public abstract void Execute();
    }
}