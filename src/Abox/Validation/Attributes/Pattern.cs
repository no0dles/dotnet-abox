using System.Text.RegularExpressions;

namespace Abox.Validation.Attributes
{
    public class Pattern : Validation
    {
        public Regex Expression { get; set; }

        public Pattern(Regex expression)
        {
            Expression = expression;
        }

        public Pattern(string expression)
        {
            Expression = new Regex(expression);
        }
    }
}