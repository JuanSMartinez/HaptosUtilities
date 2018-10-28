using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HaptosUtilities
{
    public abstract class Quest:MonoBehaviour
    {
        //Required points
        public int RequiredPoints;

        //Points worth
        public int PointsWorth;

        //External text asset where the stimuli are stored
        public TextAsset StimuliAsset;

        void Awake()
        {
            LoadStimuliSet();
        }

        //Load the stimuli set
        public abstract void LoadStimuliSet();

        //Start training mode
        public abstract void StartTrainingMode();

        //Start testing mode
        public abstract void StartTestingMode();

    }
}
