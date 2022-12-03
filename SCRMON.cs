using System.Diagnostics;
using System.Drawing;

namespace SCRMON
{
    internal static class SCRMON
    {
        static void Main(string[] args)
        {
            ConsoleExtension.Show();
            Console.Write("Initialising.");
            Task.Delay(1000).Wait();
            Console.Write(".");
            Task.Delay(1000).Wait();
            Console.WriteLine(".");

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Task.Delay(1000);

            Console.WriteLine("Would you like to begin a new monitor session? (Y/N)");
            bool Y = Console.ReadLine()!.ToLower().Contains('y');

            if(Y)
            {
                //Begin a monitoring session.
                Console.Clear();
                Console.WriteLine("Please indicate the level of pixel sensitivity (1-1000):");
                int PSen = 0;
                if(int.TryParse(Console.ReadLine()!, out PSen))
                {
                    Console.Clear();
                    Console.WriteLine("Please indicate the level of time sensitivity in milliseconds (100-10000):");
                    int TSen = 0;
                    if (Int32.TryParse(Console.ReadLine()!, out TSen))
                    {
                        Console.Clear();
                        Console.WriteLine("Please indicate the level of brightness sensitivity in percentage change (0-100):");
                        int BSen = 0;
                        if (int.TryParse(Console.ReadLine()!, out BSen))
                        {
                            Console.Clear();
                            Console.WriteLine("Alarm Initialised");
                            //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
                            ConsoleExtension.Hide(); 

                            var Runner = CapCom(TSen, BSen, PSen);
                            Runner.Wait();

                            if(Runner.IsCompleted)
                            {
                                ConsoleExtension.Show();
                                Console.WriteLine("Alarm Activated: " + DateTime.Now.ToString());
                                Alarm();

                                
                            }    

                        }
                    }
                }
                
            }

            Console.WriteLine("Session terminated. Press any key to continue.");
            Console.ReadKey();
        }

        private static async Task CapCom(int ms, int bsen, int psen)
        {
            try
            {
                Screen CaptureAgent = new Screen();
                while (true)
                {

                    using Bitmap ScreenA = (Bitmap)CaptureAgent.CaptureScreen();
                    await Task.Delay(ms);
                    using Bitmap ScreenB = (Bitmap)CaptureAgent.CaptureScreen();

                    if(!Compare(ScreenA, ScreenB, bsen, psen))
                    {
                        return;
                    }
                }

            }
            catch
            {
                //Error break.
            }

        }

        public static bool Compare(Bitmap bmp1, Bitmap bmp2, int bsen, int psen)
        {

            //Test to see if we have the same size of image
            if (bmp1.Size != bmp2.Size)
            {
                return false;
            }
            else
            {
                //Sizes are the same so start comparing pixels
                for (int x = 0; x < bmp1.Width; x ++)
                {
                    for (int y = 0; y < bmp1.Height; y+=psen)
                    {
                        var pixA = bmp1.GetPixel(x, y);
                        var pixB = bmp2.GetPixel(x, y);

                        if(pixA.R != pixB.R)
                        {
                            if((pixA.R + bsen < pixB.R) || (pixA.R - bsen > pixB.R))
                            {
                                return false;
                            }
                        }

                        if (pixA.G != pixB.G)
                        {
                            if ((pixA.G + bsen < pixB.G) || (pixA.G - bsen > pixB.G))
                            {
                                return false;
                            }
                        }

                        if (pixA.B != pixB.B)
                        {
                            if ((pixA.B + bsen < pixB.B) || (pixA.B - bsen > pixB.B))
                            {
                                return false;
                            }
                        }

                    }
                }
            }
            return true;
        }

        public static void Alarm()
        {
            while(true)
            {
                Console.Beep(1000, 1000);
                Task.Delay(1000).Wait();
            }
        }
    }


}