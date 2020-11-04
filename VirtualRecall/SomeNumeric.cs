namespace CodingSolution
{
    public class SomeNumeric : IHasNumeric
    {
        public SomeNumeric(double d)
        {
            Num = d;
        }

        public double Num { get; set; }
    }
}

