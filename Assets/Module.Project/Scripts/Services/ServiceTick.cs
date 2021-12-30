using Module.Core.MVC;
using Module.Core.Utilities;
using UnityEngine;

namespace Module.Project.Services
{
    public enum TickStatus
    {
        Pause,
        Play
    }

    public class ServiceTick : ComponentControllerBase
    {
        public readonly Event<TickStatus> GameTickStatusEvent = new Event<TickStatus>();

        public readonly Event<float> GameTickEvent = new Event<float>();
        public readonly Event<float> GameLastTickEvent = new Event<float>();
        public readonly Event<float> GameFixedTickEvent = new Event<float>();

        public readonly Event<float> GameTickPerSecondEvent = new Event<float>();
        public readonly Event<float> GameLastTickPerSecondEvent = new Event<float>();
        public readonly Event<float> GameFixedTickPerSecondEvent = new Event<float>();

        private TickStatus gameTickStatus = TickStatus.Pause;

        private const float SECOND = 1f;

        private float timerTickPerSecond = SECOND;
        private float timerLastTickPerSecond = SECOND;
        private float timerFixedTickPerSecond = SECOND;

        public void SetGameTickStatus(TickStatus status)
        {
            GameTickStatusEvent.Invoke(status);
            gameTickStatus = status;
        }

        private void Update()
        {
            if (gameTickStatus == TickStatus.Play)
            {
                GameTickEvent.Invoke(Time.deltaTime);
                TickPerSecond(ref timerTickPerSecond, GameTickPerSecondEvent);
            }
        }

        private void LateUpdate()
        {
            if (gameTickStatus == TickStatus.Play)
            {
                GameLastTickEvent.Invoke(Time.deltaTime);
                TickPerSecond(ref timerLastTickPerSecond, GameLastTickPerSecondEvent);
            }
        }

        private void FixedUpdate()
        {
            if (gameTickStatus == TickStatus.Play)
            {
                GameFixedTickEvent.Invoke(Time.deltaTime);
                TickPerSecond(ref timerFixedTickPerSecond, GameFixedTickPerSecondEvent);
            }
        }

        private static void TickPerSecond(ref float timer, Event<float> eventInvoke)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer += SECOND;
                eventInvoke.Invoke(SECOND);
            }
        }
    }
}