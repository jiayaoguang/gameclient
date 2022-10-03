using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class JsonManager
{

    public string Serialize(object obj) {
        return JsonConvert.SerializeObject(obj);
    }

    public T Deserialize<T>(string obj)
    {
        try {
            return JsonConvert.DeserializeObject<T>(obj);
        }
        catch (Exception e) {
            Debug.Log(" Deserialize exception  : " + obj + " type :" + typeof(T));
            throw e;
        }

        
    }
}

