namespace Code
{
    public class Streams
    {
        public static Stream Ones()
        {
            return Stream.ConsStream(1, Ones);
        }

        public static Stream IntegersStartingFrom(int n)
        {
            return Stream.ConsStream(n, () => IntegersStartingFrom(n + 1));
        }

        public static Stream Integers()
        {
            return IntegersStartingFrom(1);
        }
    }
}
