using ObjCRuntime;
using UIKit;

namespace ARMauiApp;

public class Program
{
	// This is the main entry point of the application.
    static void Main(string[] args)
    {
        Runtime.MarshalObjectiveCException += (sender, e) =>
        {
            Console.WriteLine($"[ObjCException] Name={e.Exception},Mode={e.ExceptionMode}");
            if (e.Exception is not null)
                Console.WriteLine(e.Exception.ToString()); 
        };

        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}

