using System;
using System.IO;
using Module.Core;
using Module.Core.MVC;
using Module.Game.Scripts.Models;
using UnityEngine;

namespace Module.Game.Scripts.Controllers
{
    public class FoodDataController : ComponentControllerBase, IBindComponent
    {
        private const string foodDataFileName = "foodData.json";
        private string pathToJson;
        private FoodModel[] foodModels;

        private void Start()
        {
            pathToJson = $"{Application.persistentDataPath}/{foodDataFileName}";
            if (File.Exists(pathToJson))
            {
                ReadDataFromJson();
            }
            else
            {
                WriteDataToJson();
            }
        }

        public FoodModel[] GetFoodData() => foodModels;
        private void WriteDataToJson()
        {
            try
            {
                var foods = new FoodModel[2];
                foods[0] = new FoodModel
                {
                    color = new[] { 255f, 0f, 0f, 255f },
                    points = 20
                };
                foods[1] = new FoodModel
                {
                    color = new[] { 100, 9f, 201f, 255f },
                    points = 10
                };

                var s = JsonHelper.ToJson(foods);
                File.WriteAllText(pathToJson, s);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                throw;
            }
            
        }

        private void ReadDataFromJson()
        {
            try
            {
                var fileData = File.ReadAllText(pathToJson);
                foodModels = JsonHelper.FromJson<FoodModel>(fileData);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                throw;
            }
            
        }
    }
}