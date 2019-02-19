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

        //Required percent correct score to pass
        public float requiredPercentCorrect;

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

        //Stop training mode
        public abstract void StopTrainingMode();

        //Stop testing mode
        public abstract void StopTestingMode();

        //Abruptly stop training mode
        public abstract void InterruptTrainingMode();

        //Abruptly stop testing mode
        public abstract void InterruptTestingMode();

    }
}
