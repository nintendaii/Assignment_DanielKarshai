namespace Module.Project.Managers {
    public interface IInternalCall {
        string ReadUrl();
        string ReadParamFromUrl(string paramId);
    }
}