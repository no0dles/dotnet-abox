namespace Abox.Validation.Attributes
{
    public class Length : Validation
    {
        public int? Max { get; set; }
        public int? Min { get; set; }

        public Length(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public Length(int min)
        {
            Min = min;
        }
    }
}