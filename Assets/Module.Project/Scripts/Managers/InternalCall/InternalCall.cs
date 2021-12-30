using Module.Core.MVC;

namespace Module.Project.Managers
{
    public sealed partial class InternalCall : ControllerBase, IInternalCall
    {
        public string ReadUrl()
        {
            var result = string.Empty;
            ReadUrlInner(ref result);
            return result;
        }

        public string ReadParamFromUrl(string paramId)
        {
            var result = string.Empty;
            ReadParamFromUrlInner(ref result, paramId);
            return result;
        }

        partial void ReadUrlInner(ref string result);
        partial void ReadParamFromUrlInner(ref string result, string paramId);
    }
}