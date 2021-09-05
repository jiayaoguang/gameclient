using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PlayerManager
{

    public PlayerInfo myPlayerInfo;

    private Dictionary<long, PlayerInfo> playerInfoMap = new Dictionary<long, PlayerInfo>();
    private Dictionary<string, PlayerInfo> name2playerInfoMap = new Dictionary<string, PlayerInfo>();


    public void PutPlayerInfo(PlayerInfo playerInfo)
    {
        playerInfoMap.Add(playerInfo.id , playerInfo);
        name2playerInfoMap.Add(playerInfo.name , playerInfo);
    }

    public PlayerInfo GetPlayerInfo(string name)
    {
        PlayerInfo playerInfo;
        name2playerInfoMap.TryGetValue(name,out playerInfo);

        return playerInfo;
    }

    public void RemovePlayerInfo(String name)
    {
        //TODO
        //playerInfoMap.TryGetValue(playerInfo.id, playerInfo);
        //name2playerInfoMap.TryGetValue(name, playerInfo);
    }


}

