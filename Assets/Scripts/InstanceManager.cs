using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class InstanceManager
{
    private static InstanceManager inner_instance = new InstanceManager();
    public static InstanceManager instance
    {
        get {
            return inner_instance;
        }
    }

    
   

    public PlayerManager playerManager = new PlayerManager();

    public readonly JsonManager jsonManager = new JsonManager();




}

