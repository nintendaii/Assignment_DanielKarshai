using System;
using Module.Core.MVC;
using UnityEngine;

namespace Module.Project.Controllers {
    [Serializable]
    public class ProjectOverlayView : ViewBase {
        [SerializeField] public Transform Content;
    }
    
    public class ProjectOverlayController : ComponentControllerBase<ModelBase, ProjectOverlayView> {
        
    }
}