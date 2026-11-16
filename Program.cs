namespace Clockv2.nn
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Clock._7segmentTester());

//            Icon ic = new Icon(@"Clockv2ICO256x256.bmp", 256,256);//
            Application.Run(new NESixClock());
        }
    }
}