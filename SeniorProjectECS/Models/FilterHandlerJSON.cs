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

        public void AddModel(Filter model)
        {
            if (model.FilterName != null && model.FilterName.Length > 0)
            {
                string jString = JsonConvert.SerializeObject(model);

                // Write the filter to a json file
                try
                {
                    using (StreamWriter sw = new StreamWriter("filters/" + model.FilterName + ".json", false))
                    {
                        sw.WriteLine(jString);
                    }
                } catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory("filters");
                    AddModel(model);
                    return;
                }

                ReloadFilterList();
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

            }

            return null;
        }

        public IEnumerable<Filter> GetModels()
        {
            throw new NotImplementedException();
        }

        public void UpdateModel(Filter Model)
        {
            throw new NotImplementedException();
        }
    }
}
