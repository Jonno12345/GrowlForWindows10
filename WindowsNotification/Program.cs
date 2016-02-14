namespace WindowsNotification
{
    class Program
    {
        static void Main(string[] args)
        {
            new WindowsToast(args[0]).ShowToast();
        }
    }
}
