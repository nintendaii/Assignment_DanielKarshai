using UnityEngine;

namespace Module.MainMenu.Scripts.Controllers
{
    public class TopScoreDataHandler
    {
        private static string topScorePrefString = "topScore";

        public static void Save(int score) => PlayerPrefs.SetInt(topScorePrefString, score);

        public static int Load() => PlayerPrefs.GetInt(topScorePrefString, 0);

    }
}