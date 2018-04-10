using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SeniorProjectECS.Models
{
    public class FilterHandlerJSON : IModelHandler<Filter>
    {
        // The global filter list
        public static Dictionary<int, string> FilterList = new Dictionary<int, string>();

        public static void ReloadFilterList()
        {
            try
            {
                using (StreamReader sr = new StreamReader("filterIndex.json"))
                {
                    string jString = sr.ReadToEnd();
                    FilterList = JsonConvert.DeserializeObject<Dictionary<int, string>>(jString);
                }
            }
            catch (FileNotFoundException)
            {
                // Create a new filter index file
                using (StreamWriter sw = new StreamWriter("filterIndex.json", false))
                {
                    string jString = JsonConvert.SerializeObject(FilterList);
                    sw.WriteLine(jString);
                }
            }
        }

        public static void UpdateFilterList()
        {
            using(StreamWriter sw = new StreamWriter("filterIndex.json", false))
            {
                string jString = JsonConvert.SerializeObject(FilterList);
                sw.WriteLine(jString);
            }

            ReloadFilterList();
        }

        public void AddModel(Filter model)
        {
            if (model.FilterName != null && model.FilterName.Length > 0)
            {
                // Get the next key to be used
                int newKey = 0;
                if(FilterList.Count != 0)
                {
                    newKey = FilterList.OrderBy(f => f.Key).Last().Key + 1;
                }
                model.FilterID = newKey;

                // Convert to json
                string jString = JsonConvert.SerializeObject(model);

                // Write the filter to a json file
                try
                {
                    using (StreamWriter sw = new StreamWriter("filters/" + model.FilterName + ".json", false))
                    {
                        sw.WriteLine(jString);
                        FilterList.Add(model.FilterID.Value, model.FilterName);
                    }
                } catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory("filters");
                    AddModel(model);
                    return;
                }

                UpdateFilterList();
            }
        }

        public void DeleteModel(int id)
        {
            throw new NotImplementedException();
        }

        public Filter GetModel(int id)
        {
            if(FilterList.TryGetValue(id, out string filterName))
            {
                using(StreamReader sr = new StreamReader("filters/" + filterName + ".json"))
                {
                    string jString = sr.ReadToEnd();
                    return JsonConvert.DeserializeObject<Filter>(jString);
                }
            }

            return null;
        }

        public IEnumerable<Filter> GetModels()
        {
            throw new NotImplementedException();
        }

        public void UpdateModel(Filter model)
        {
            if (model.FilterName != null && model.FilterName.Length > 0)
            {
                // Convert to json
                string jString = JsonConvert.SerializeObject(model);

                // Write the filter to a json file
                try
                {
                    using (StreamWriter sw = new StreamWriter("filters/" + model.FilterName + ".json", false))
                    {
                        sw.WriteLine(jString);
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory("filters");
                    AddModel(model);
                    return;
                }
            }
        }
    }
}
