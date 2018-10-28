using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaptosUtilities
{
    [System.Serializable]
    public class Player
    {
        //Level
        public int Level { get; set; }

        //Quest points
        public int QuestPoints { get; set; }

        //Player name
        public string Name { get; set; }

        //Total training time
        public float TotalTrainingTime { get; set; }

        //Total testing time
        public float TotalTestingTime { get; set; }

        //Constructor
        public Player(string name)
        {
            Level = 1;
            QuestPoints = 0;
            TotalTrainingTime = 0;
            TotalTestingTime = 0;
        }
    }
}
