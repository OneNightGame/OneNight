using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OneNight.Data
{
    public class RoomData
    {
        public List<string> players;
        public string code;

        public void AddPlayer(string playerName)
        {
            players.Add(playerName);
        }
    }

    public class RoomManagerService
    {
        private string curGameCode = "AAAA";
        private Dictionary<string, RoomData> rooms = new Dictionary<string, RoomData>();

        public event Action<string> NotifyUpdate;

        private void IncrementGameCodeAt(int index, StringBuilder strBuilder = null)
        {
            if (strBuilder == null)
                strBuilder = new StringBuilder(curGameCode);
            strBuilder[index] = (char)(strBuilder[index] + 1);
            if (strBuilder[index] > 'Z')
            {
                strBuilder[index] = 'A';
                if (index > 0) IncrementGameCodeAt(index - 1, strBuilder);
                else throw new NotImplementedException("out of game codes. guess we really did play those 26^4 games without restarting the server");
            }
            curGameCode = strBuilder.ToString();
        }

        // Create room, return string ID
        public string CreateRoom(UserDataService roomOwner)
        {
            string gameCode = curGameCode.Clone().ToString();
            IncrementGameCodeAt(curGameCode.Length - 1);

            roomOwner.hostOfRoom = gameCode;
            rooms.Add(gameCode, new RoomData { players = new List<string>() { roomOwner.username } });

            return gameCode;
        }

        public bool JoinRoom(UserDataService userData, string roomCode)
        {
            if (!rooms.ContainsKey(roomCode)) { return false; }
            rooms[roomCode].AddPlayer(userData.username);

            if (NotifyUpdate != null)
            {
                NotifyUpdate?.Invoke(roomCode);
            }

            return true;
        }

        public List<string> GetRoomPlayers(string roomCode)
        {
            return rooms[roomCode].players;
        }

        public string GetRoomHost(string roomCode)
        {
            return rooms[roomCode].players[0];
        }
    }
}
