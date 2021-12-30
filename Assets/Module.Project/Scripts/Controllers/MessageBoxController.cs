using System;
using Module.Core.MVC;
using Module.Project.CommandSignals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Project.Controllers
{
    [Serializable]
    public class MessageBoxView : ViewBase
    {
        [SerializeField] public TextMeshProUGUI messageBoxText;
        [SerializeField] public Button closeButton;
    }

    public class MessageBoxController : ComponentControllerBase<ModelBase, MessageBoxView>
    {
        protected override bool VisibilityOnAwake => false;

        public override void Initialize()
        {
            base.Initialize();
            View.closeButton.onClick.AddListener(() => SignalBus.Fire(new SignalHideMessageBox()));
        }

        public void SetMessage(string value)
        {
            View.messageBoxText.text = value;
        }
    }
}