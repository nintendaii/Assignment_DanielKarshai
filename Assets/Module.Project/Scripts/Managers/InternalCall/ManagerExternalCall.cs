using System;
using Module.Core.MVC;

namespace Module.Project.Managers
{
    public class ManagerExternalCall : ControllerMonoBase
    {
        private event Action<string> SetStudentIdEvent;
        private event Action<string> SetStoryIdEvent;
        private event Action<string> SetLoginTypeEvent;

        public string StudentId { get; private set; }
        public string StoryId { get; private set; }
        public string LoginType { get; private set; }
        public bool HasExternalCall { get; private set; }

        public void SetStudentId(string value)
        {
            StudentId = value;
            HasExternalCall = true;
            SetStudentIdEvent?.Invoke(StudentId);
        }

        public void SetLoginType(string value)
        {
            LoginType = value;
            HasExternalCall = true;
            SetLoginTypeEvent?.Invoke(LoginType);
        }

        public void SubscribeLoginType(Action<string> action)
        {
            SetLoginTypeEvent += action;
        }

        public void UnsubscribeLoginType(Action<string> action)
        {
            if (SetLoginTypeEvent != null) SetLoginTypeEvent -= action;
        }

        public void SubscribeStudentId(Action<string> action)
        {
            SetStudentIdEvent += action;
        }

        public void UnsubscribeStudentId(Action<string> action)
        {
            if (SetStudentIdEvent != null) SetStudentIdEvent -= action;
        }

        public void SetStoryId(string value)
        {
            StoryId = value;
            HasExternalCall = true;
            SetStoryIdEvent?.Invoke(StoryId);
        }

        public void SubscribeStoryId(Action<string> action)
        {
            SetStoryIdEvent += action;
        }

        public void UnsubscribeStoryId(Action<string> action)
        {
            if (SetStoryIdEvent != null) SetStoryIdEvent -= action;
        }
    }
}