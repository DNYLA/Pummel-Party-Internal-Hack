using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace PummelPartyHack.Minigames
{
    public class MysteryMaze : MonoBehaviour
    {
        List<MysteryMazeVisibilityEvent> mEventList = new List<MysteryMazeVisibilityEvent>();
        List<MysteryMazePlayer> players = new List<MysteryMazePlayer>();
        MysteryMazePlayer me = new MysteryMazePlayer();
        private void GetPlayers()
        {
            players.Clear();
            players.AddRange(FindObjectsOfType<MysteryMazePlayer>());

            foreach (MysteryMazePlayer ply in players)
            {
                if (ply.GamePlayer.IsLocalPlayer && !ply.GamePlayer.IsAI)
                {
                    me = ply;
                    break;
                }
            }
        }

        public void MysterMazeCheat()
        {
            GetPlayers();
            if (players.Count <= 0 || me == null)
                return;

            MysteryMazeController mEvent = FindObjectOfType<MysteryMazeController>();

            Type type = typeof(MysteryMazeController);
            FieldInfo fInfo = type.GetField("m_clipRadius", BindingFlags.NonPublic | BindingFlags.Instance);

            fInfo.SetValue(mEvent, 4000f);
            

        }


    }
}
