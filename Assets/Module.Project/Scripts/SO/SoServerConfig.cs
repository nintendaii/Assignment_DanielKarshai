using UnityEngine;
using Zenject;

namespace Module.Project.SO {
    [CreateAssetMenu(fileName = "ServerConfig", menuName = "Module/Project/ServerConfig")]
    public class SoServerConfig : ScriptableObjectInstaller {
        [SerializeField] private TextAsset token;
        
        public override void InstallBindings() {
            Container.BindInstance(this);
        }

        public string GetToken() => token == null ? string.Empty : token.text;
    }
}