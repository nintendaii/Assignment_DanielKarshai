namespace Module.Project.Managers
{
    public sealed partial class InternalCall
    {
#if UNITY_EDITOR
        partial void ReadUrlInner(ref string result)
        {
        }

        partial void ReadParamFromUrlInner(ref string result, string paramId)
        {
        }
#endif
    }
}