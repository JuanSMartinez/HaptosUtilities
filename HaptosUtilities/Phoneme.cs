using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taps;

namespace HaptosUtilities
{
    public class Phoneme : Stimulus
    {

        //Phoneme label
        public string Label { get; set; }

        //Constructor
        public Phoneme(string phonemeLabel)
        {
            Label = phonemeLabel;
        }
    

        public override void DeliverStimulus()
        {
            Motu.Instance.PlayPhoneme(Label);
        }

        public override bool EqualsToOther(Stimulus other)
        {
            Phoneme otherPhoneme = other as Phoneme;
            return Label.Equals(otherPhoneme.Label);
        }
    }
}
