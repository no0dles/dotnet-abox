namespace Abox.Validation.Attributes
{
    public class Range : Validation
    {
        public int? Max { get; set; }
        public int? Min { get; set; }

        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}