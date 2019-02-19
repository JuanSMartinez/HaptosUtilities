using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaptosUtilities
{
    public class TestMonitor
    {
        //Stimuli set
        private List<Stimulus> Stimuli { get;  set; }

        //Random number generator
        Random r;

        //Testing trials
        private int trials;
        private int Trials
        {
            get
            {
                return trials;
            }
            set
            {
                if (value > 0)
                    trials = value;
                else throw new Exception("Invalid trial number");

            }
        }
        public float PercentCorrect { get; private set; }

        //Stimulus-response set
        private List<string> stimuliResponsePairs;

        //Response times array in seconds
        private List<float> responseTimesSeconds;

        //Test stimuli
        private Queue<Stimulus> testStimuli;

        //Constructor
        public TestMonitor(int trials, List<Stimulus> stimuli)
        {
            Trials = trials;
            Stimuli = stimuli;
            testStimuli = new Queue<Stimulus>();
            stimuliResponsePairs = new List<string>();
            responseTimesSeconds = new List<float>();
            PercentCorrect = 0;
            r = new Random();
            GenerateRandomStimuliSet();

        }

        //Generate random indices
        private void GenerateRandomStimuliSet()
        {
            for(int i =0; i < trials; ++i)
            {     
                Stimulus newStimulus = GetRandomStimulus();
                testStimuli.Enqueue(newStimulus);
            }
        }

        //Get a random stimulus from the stimuli set
        public Stimulus GetRandomStimulus()
        {
            return Stimuli.ElementAt(r.Next(0, Stimuli.Count));
        }

        //Dequeue a stimulus from the queue 
        public Stimulus NextStimulus()
        {
            if (testStimuli.Count > 0)
                return testStimuli.Dequeue();
            else
                throw new Exception("Queue empty");
        }

        //Record a stimulus-reponse pair
        public void RecordStimulusResponsePair(string stimulusName, string responseName, float responseTime)
        {
            if (stimulusName.Equals(responseName))
                PercentCorrect += 1f;
            stimuliResponsePairs.Add(stimulusName + "," + responseName);
            responseTimesSeconds.Add(responseTime);
        }

        //Finalize test
        public void FinishTest()
        {
            PercentCorrect = PercentCorrect / (1f*trials);
        }

        //Generate trial response log
        public string GenerateResponseLog()
        {
            string response = "Stimulus,Response,ResponseTime(s)\n";
            int i = 0;
            foreach(string pair in stimuliResponsePairs)
            {
                float responseTime = responseTimesSeconds.ElementAt(i);
                ++i;
                if (i < stimuliResponsePairs.Count)
                    response += pair + ","+ responseTime + "\n";
                else
                    response += pair + "," + responseTime;
            }
            return response;
        }


        //Generate confusion matrix log
        public string GenerateConfusionMatrixLog()
        {
            int k = Stimuli.Count;
            int[,] matrix = new int[k, k];
            for (int i = 0; i < k; ++i)
                for (int j = 0; j < k; ++j)
                    matrix[i, j] = 0;

            foreach (string pair in stimuliResponsePairs)
            {
                string stimulus = pair.Split(","[0])[0];
                string response = pair.Split(","[0])[1];
                int i = GetIndexOfStimulus(stimulus);
                int j = GetIndexOfStimulus(response);
                if (i != -1 && j != -1)
                    matrix[i, j] += 1;
            }

            string log = "EMPTY";
            foreach (Stimulus stimulus in Stimuli)
                log += "," + stimulus.Name;
            log += "\n";
            for (int i = 0; i < k; ++i)
            {
                log += Stimuli.ElementAt(i).Name;
                for (int j = 0; j < k; ++j)
                {
                    log += "," + matrix[i, j];
                }
                if (i != k - 1)
                    log += "\n";
            }
            return log;
        }

        //Find the index of a stimulus in the stimuli set
        private int GetIndexOfStimulus(string stimulusName)
        {
            int i = 0;
            foreach(Stimulus stimulus in Stimuli)
            {
                if (stimulus.Name.Equals(stimulusName))
                    return i;
                else
                    ++i;
            }
            return -1;
        }
    }
}
