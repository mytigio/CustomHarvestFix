using Microsoft.Xna.Framework;
using Netcode;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SubterranianOverhaul.Crops
{
    public class CaveCarrot : Crop
    {
        private static int seedIndex = -1;
        private static int cropTextureIndex = -1;

        private const string CROP_DATA_STRING = "1 1 1/spring summer fall winter/{1}/{0}/-1/0/false/false/false";
        public const int HARVEST_INDEX = 78;

        public static void setIndex()
        {
            if (CaveCarrot.seedIndex == -1)
            {
                CaveCarrot.seedIndex = CaveCarrotSeed.getIndex();
            }
        }

        public static int getIndex()
        {
            if (CaveCarrot.seedIndex == -1)
            {
                CaveCarrot.setIndex();
            }

            return CaveCarrot.seedIndex;
        }

        public static void setCropIndex()
        {
            if (CaveCarrot.cropTextureIndex == -1)
            {
                CaveCarrot.cropTextureIndex = IndexManager.getUnusedCropIndex();
            }
        }

        public static int getCropIndex()
        {
            if (CaveCarrot.cropTextureIndex == -1)
            {
                CaveCarrot.setCropIndex();
            }

            return CaveCarrot.cropTextureIndex;
        }

        public CaveCarrot() : this(Vector2.Zero)
        {   
        }

        public CaveCarrot(Vector2 tileLocation) : base(seedIndex,(int) tileLocation.X, (int) tileLocation.Y)
        {   
        }

        public static string getCropData()
        {
            return String.Format(CROP_DATA_STRING,HARVEST_INDEX,CaveCarrot.getCropIndex());
        }

        /*
        private static HashSet<GameLocation> processedLocations = new HashSet<GameLocation>();

        internal static void RemoveAll()
        {
            SetHelper();
            SetMonitor();

            if (!Game1.IsMasterGame)
                return;

            monitor.Log("CaveCarrot.RemovalAll()", StardewModdingAPI.LogLevel.Trace);
            ModState.caveCarrotsPlanted.Clear();

            processedLocations.Clear();

            foreach (GameLocation location in Game1.locations)
            {
                ProcessLocation(location, ProcessingMethod.Remove);
            }

            processedLocations.Clear();
        }

        internal static void ReplaceAll()
        {
            throw new NotImplementedException();
        }
        public static void ProcessLocation(GameLocation location, ProcessingMethod method)
        {
            if (location == null)
                return;

            monitor.Log("CaveCarrot.ProcessLocation(" + location.Name + ", " + method + ")", StardewModdingAPI.LogLevel.Trace);

            if (processedLocations.Contains(location))
            {
                monitor.Log("CaveCarrot.ProcessLocation(" + location.Name + ", " + method + "): Already processed this location (infinite recursion?), aborting!", StardewModdingAPI.LogLevel.Warn);
                return;
            }

            processedLocations.Add(location);

            bool itemsToRemove = false;

            if (CaveCarrotSeed.IsValidLocation(location))
            {
                String locationName = location.Name.ToString();
                if (method.Equals(ProcessingMethod.Remove))
                {

                    foreach (Vector2 featureSpot in location.terrainFeatures.Keys)
                    {
                        if (location.terrainFeatures[featureSpot] is HoeDirt)
                        {
                            HoeDirt dirtSpot = location.terrainFeatures[featureSpot] as HoeDirt;
                            if (dirtSpot.crop.netSeedIndex.Value == CaveCarrotSeed.getIndex())
                            {
                                StringWriter writer = new StringWriter();

                                if (!ModState.caveCarrotsPlanted.ContainsKey(locationName))
                                {
                                    ModState.caveCarrotsPlanted.Add(locationName, new Dictionary<Vector2, CaveCarrotCropSaveData>());
                                }

                                CaveCarrot carrot = dirtSpot.crop as CaveCarrot;

                                ModState.caveCarrotsPlanted[locationName].Add(featureSpot, carrot.GetSaveData());
                                itemsToRemove = true;
                            }
                        }
                    }

                    //data is stored, but if we're removing we need to actually clear stuff out of the list now.
                    if (itemsToRemove)
                    {
                        foreach (Vector2 locationData in ModState.caveCarrotsPlanted[location.Name.ToString()].Keys)
                        {
                            location.terrainFeatures.Remove(locationData);
                        }
                    }
                }
                else if (method.Equals(ProcessingMethod.Restore))
                {
                    if (ModState.caveCarrotsPlanted.ContainsKey(location.Name.ToString()))
                    {
                        foreach (KeyValuePair<Vector2, CaveCarrotCropSaveData> locationData in ModState.caveCarrotsPlanted[location.Name.ToString()])
                        {
                            
                            location.terrainFeatures.Add(locationData.Key, new VoidshroomTree(locationData.Value));
                        }
                    }
                }
            }
        }

        private CaveCarrotCropSaveData GetSaveData()
        {
            return new CaveCarrotCropSaveData(this);
        }

        public static void SetHelper()
        {
            if (CaveCarrot.helper == null)
            {
                CaveCarrot.helper = ModEntry.GetHelper();
            }
        }

        public static void SetMonitor()
        {
            if (CaveCarrot.monitor == null)
            {
                CaveCarrot.monitor = ModEntry.GetMonitor();
            }
        }
        */
    }
}
