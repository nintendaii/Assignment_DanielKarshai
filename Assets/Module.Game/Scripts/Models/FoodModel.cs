using System;
using UnityEngine;

namespace Module.Game.Scripts.Models
{
    [Serializable]
    public class FoodModel
    {
        public float[] color;
        public int points;

        public override bool Equals(object obj)
        {
            var f = obj as FoodModel;
            if (f == null) return false;

            return f.points.Equals(points);
        }
    }
}