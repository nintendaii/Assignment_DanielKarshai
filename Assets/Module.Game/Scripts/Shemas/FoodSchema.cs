using System;
using UnityEngine;

namespace Module.Game.Scripts.Models
{
    [Serializable]
    public class FoodSchema
    {
        public float[] color;
        public int points;

        public override bool Equals(object obj)
        {
            var f = obj as FoodSchema;
            if (f == null) return false;

            return f.points.Equals(points);
        }
    }
}