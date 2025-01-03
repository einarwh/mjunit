﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Gadgeteer Designer.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace Mjunit.Spider
{
    public partial class Program : Gadgeteer.Program
    {
        // GTM.Module defintions
		Gadgeteer.Modules.GHIElectronics.UsbClientDP usbClient;
		Gadgeteer.Modules.GHIElectronics.MulticolorLed led;
		Gadgeteer.Modules.GHIElectronics.Display_T35 display;

		public static void Main()
        {
			//Important to initialize the Mainboard first
            Mainboard = new GHIElectronics.Gadgeteer.FEZSpider();			

            Program program = new Program();
			program.InitializeModules();
            program.ProgramStarted();
            program.Run(); // Starts Dispatcher
        }

        private void InitializeModules()
        {   
			// Initialize GTM.Modules and event handlers here.		
			usbClient = new GTM.GHIElectronics.UsbClientDP(1);
		
			display = new GTM.GHIElectronics.Display_T35(14, 13, 12, 10);
		
			led = new GTM.GHIElectronics.MulticolorLed(11);

        }
    }
}
