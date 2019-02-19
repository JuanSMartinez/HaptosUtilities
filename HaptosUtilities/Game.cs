using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HaptosUtilities
{
    public static class Game
    {
        //Code name for the superplayer
        private static string superPlayerName = "SuperJJ";

        //reference to the current player
        public static Player CurrentPlayer { get; set; }

        //Super player active
        public static bool SuperPlayer { get; set; }

        //Create a new character
        public static bool CreateCharacter(string name)
        {
            if (name.Equals(superPlayerName))
            {
                CurrentPlayer = new Player(name);
                CurrentPlayer.Level = 999;
                CurrentPlayer.QuestPoints = 999;
                SuperPlayer = true;
                return true;
            }
            else
            {
                if (PersistenceUtilities.CreateFolderForPlayer(name.ToUpper()))
                {
                    CurrentPlayer = new Player(name.ToUpper());
                    return true;
                }
                else
                    return false;
            }
        }

        //load a player
        public static  void LoadPlayer(string playerName)
        {
            CurrentPlayer = PersistenceUtilities.LoadPlayer(playerName);
        }

    }
}
