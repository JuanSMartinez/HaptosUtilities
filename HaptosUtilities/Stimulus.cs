using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaptosUtilities
{
    public abstract class Stimulus
    {
        //Equals
        public abstract bool EqualsToOther(Stimulus other);

        //Deliver the stimulus
        public abstract void DeliverStimulus();

        //String name of the stimulus
        public string Name { get; set; }

    }
}
