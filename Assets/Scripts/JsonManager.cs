using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class JsonManager
{

    public string Serialize(object obj) {
        return JsonConvert.SerializeObject(obj);
    }

    public T Deserialize<T>(string obj)
    {
        return JsonConvert.DeserializeObject<T>(obj);
    }
}

