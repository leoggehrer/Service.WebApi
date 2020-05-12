using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Service.WebApi.Controllers
{
    public class GenericController<T> : ControllerBase
        where T : Models.ModelObject, new()
    {
        private string DataFolder => "Data";
        private string FileName => $"{typeof(T).Name}.ser";

        protected string GetModelMetaData()
        {
            var result = "{";
            var model = new T();

            foreach (var pi in model.GetType().GetProperties())
            {
                result += Environment.NewLine;
                result += $"  \"{pi.Name}\": {pi.PropertyType}"; 
            }

            result += Environment.NewLine +  "}";
            return result;
        }

        protected IEnumerable<T> LoadData()
        {
            var result = default(List<T>);
            var path = Path.Combine(Directory.GetCurrentDirectory(), DataFolder);
            var filePath = Path.Combine(path, FileName);

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            else if (System.IO.File.Exists(filePath))
            {
                var formatter = new BinaryFormatter();
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                result = (List<T>)formatter.Deserialize(fs);
                fs.Close();
            }
            else
            {
                result = new List<T>();
            }
            return result;
        }
        protected void SaveData(IEnumerable<T> models)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), DataFolder);
            var filePath = Path.Combine(path, FileName);

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            var formatter = new BinaryFormatter();
            using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            formatter.Serialize(fs, models.ToList());
            fs.Close();
        }

        protected T InsertModel(T model)
        {
            var models = new List<T>(LoadData());

            model.Id = models.Any() == false ? 1 : models.Max(m => m.Id) + 1;
            models.Add(CreateModel(model));
            SaveData(models);
            return model;
        }
        protected void UpdateModel(int id, T model)
        {
            var models = LoadData();
            var item = models.SingleOrDefault(m => m.Id == id);

            if (item != null)
            {
                model.Id = id;
                UpdateModel(model, item);
                SaveData(models);
            }
        }

        private T CreateModel(T sourceModel)
        {
            T createModel = new T();

            UpdateModel(sourceModel, createModel);
            return createModel;
        }
        private void UpdateModel(T sourceModel, T targetModel)
        {
            if (sourceModel == null)
                throw new ArgumentNullException(nameof(sourceModel));

            if (targetModel == null)
                throw new ArgumentNullException(nameof(targetModel));

            foreach (var srcPi in sourceModel.GetType().GetProperties())
            {
                var trgPi = targetModel.GetType().GetProperty(srcPi.Name);

                if (trgPi != null
                    && srcPi.CanRead
                    && trgPi.CanWrite
                    && srcPi.PropertyType.Equals(trgPi.PropertyType))
                {
                    trgPi.SetValue(targetModel, srcPi.GetValue(sourceModel));
                }
            }
        }
        protected void DeleteModel(int id)
        {
            var models = new List<T>(LoadData());
            var item = models.SingleOrDefault(m => m.Id == id);

            if (item != null)
            {
                models.Remove(item);
                SaveData(models);
            }
        }
    }
}
