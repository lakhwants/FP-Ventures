namespace FPVenturesFive9Disposition
{
	public static class Function1
    {
        [Function("Function1")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
        }
    }
}
