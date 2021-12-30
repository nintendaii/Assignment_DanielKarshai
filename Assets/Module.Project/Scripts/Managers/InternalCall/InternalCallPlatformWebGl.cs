namespace Module.Project.Managers
{
    public sealed partial class InternalCall
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        [System.Runtime.InteropServices.DllImport("__Internal")] 
        private static extern string GetUrl();
        
        [System.Runtime.InteropServices.DllImport("__Internal")] 
        private static extern string GetParamFromUrl(string paramId);

        partial void ReadUrlInner(ref string result) {
            result = GetUrl();
        }

        partial void ReadParamFromUrlInner(ref string result, string paramId) {
            result = GetParamFromUrl(paramId);
        }
#endif
    }
}