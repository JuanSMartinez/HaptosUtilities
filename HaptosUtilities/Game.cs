using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaptosUtilities
{
    public class Game
    {
        //Singleton
        public static Game instance = null;
        public static Game Instance
        {
            get
            {
                if (instance == null)
                    instance = new Game();
                return instance;
            }
        }

        //Static reference to the current player
        public static Player CurrentPlayer;

        //Super player active
        public bool SuperPlayer { get; set; }

        //Constructor
        private Game()
        {
            SuperPlayer = false;
        }

        //Create a new character
        public void CreateCharacter(string name)
        {
            if (PersistenceUtilities.Instance.CreateFolerForPlayer(name))
            {
                CurrentPlayer = new Player(name);
            }
        }

        //load a player
        public void LoadPlayer(string playerName)
        {
            CurrentPlayer = PersistenceUtilities.Instance.LoadPlayer(playerName);
        }

    }
}
