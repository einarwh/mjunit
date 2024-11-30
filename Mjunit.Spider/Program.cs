using System;
using System.Collections;
using System.Reflection;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;

using Mjunit.Clients;
using Mjunit.Clients.GHI;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace Mjunit.Spider
{
    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");

            led.GreenBlueSwapped = true;
            var font = Resources.GetFont(Resources.FontResources.NinaB);
            var clients = new ArrayList { new LedTestClient(led), new DebugTestClient(), new DisplayTestClient(display, font) };
            var runner = new TestRunner(clients);
            var assembly = Assembly.GetExecutingAssembly();
            runner.Run(assembly);

            while (true)
            {
                Thread.Sleep(5000);
            }
        }

    }
}
