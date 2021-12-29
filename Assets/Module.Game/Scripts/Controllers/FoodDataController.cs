using System;
using System.IO;
using Module.Core;
using Module.Core.MVC;
using Module.Game.Scripts.Models;
using UnityEngine;
using Random = UnityEngine.Random;

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
                ReadDataFromJson();
            }
        }

        public FoodModel GetRandomFood()
        {
            var f = foodModels;
            var randI = Random.Range(0, f.Length);
            return foodModels[randI];
        }
        
        private void WriteDataToJson()
        {
            try
            {
                var foods = new FoodModel[2];
                foods[0] = new FoodModel
                {
                    color = new[] { 1f, 1f, 0f, 1f },
                    points = 20
                };
                foods[1] = new FoodModel
                {
                    color = new[] { 1f, 0f, 0f, 1f },
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