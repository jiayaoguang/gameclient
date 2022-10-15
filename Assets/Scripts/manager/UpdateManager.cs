using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class UpdateAble
{
    public bool isStop = false;
    public abstract void Update();


    public void Stop() {
        this.isStop = true;
    }


    public bool IsStop() {

        return isStop;
    }


}

public class UpdateManager
{

    public List<UpdateAble> updateList = new List<UpdateAble>();


    public void Update()
    {
        List<UpdateAble> waitDeleteList = null;

        foreach (UpdateAble update in updateList)
        {
            if (update.IsStop())
            {
                if (waitDeleteList == null)
                {
                    waitDeleteList = new List<UpdateAble>();
                }
                waitDeleteList.Add(update);
            }
            else{
                update.Update();
            }

            

            //Debug.Log("executer update ..................");

        }

        
        if (waitDeleteList != null)
        {
            foreach (UpdateAble update in waitDeleteList) {
                updateList.Remove(update);
            }
        }
    }

    public void AddUpdate(UpdateAble update)
    {

        updateList.Add(update);

    }




}

