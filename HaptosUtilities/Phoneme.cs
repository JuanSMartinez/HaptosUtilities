using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taps;

namespace HaptosUtilities
{
    public class Phoneme : Stimulus
    {

        //Constructor
        public Phoneme(string phonemeLabel)
        {
            Name = phonemeLabel;
        }
    

        public override void DeliverStimulus()
        {
            Motu.Instance.PlayPhoneme(Name);
        }

        public override bool EqualsToOther(Stimulus other)
        {
            Phoneme otherPhoneme = other as Phoneme;
            return Name.Equals(otherPhoneme.Name);
        }
    }
}
