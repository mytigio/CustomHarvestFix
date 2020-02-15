using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubterranianOverhaul
{
    class IndexManager
    {
        public const int MAX_INDEX = 2000;
        public static IMonitor monitor;
        public static int lastIndex = -1;

        public static int getUnusedObjectIndex(int startingIndex = 984)
        {   
            if(lastIndex == -1)
            {
                lastIndex = startingIndex;
            }

            int start = lastIndex;
            int currentIndex = start;

            if(currentIndex < MAX_INDEX)
            {
                IndexManager.log("Issuing Object ID " + currentIndex);
                lastIndex = currentIndex + 1;
                return currentIndex;
            } else
            {
                IndexManager.log("Could not find unused object index between " + startingIndex + " and " + MAX_INDEX);
                return -1;
            }
        }

        private static void log(string message)
        {
            if(monitor != null)
            {
                monitor.Log(message,LogLevel.Debug);
            }
        }
    }
}
