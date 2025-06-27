using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEpart3
{
    public enum QuestionType { MultipleChoice, TrueFalse }

    public class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public string Answer { get; set; }       
        public string Explanation { get; set; }
        
    }

}
