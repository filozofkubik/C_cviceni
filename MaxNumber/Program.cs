


namespace MaxNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] pole = { 1, 2, 4, -70, 38 };
            Console.WriteLine(HledejMaximum(pole));
        }
        static int HledejMaximum(int[] ciselnepole)
        {
            int max = ciselnepole[0];
            for (int i = 0; i < ciselnepole.Length; i++)
            {
                if (max <= ciselnepole[i])
                {
                    max = ciselnepole[i];
                }



            }
            return max;

        }
    }
}
