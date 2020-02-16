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
    public class CaveCarrotFlower : Crop
    {
        private static int seedIndex = -1;
        private static int cropTextureIndex = -1;

        private const string CROP_DATA_STRING = "2 7 9 6 4/spring summer fall winter/{1}/{0}/3/0/true 4 8 2 .2/false/false";

        public static void setIndex()
        {
            if (CaveCarrotFlower.seedIndex == -1)
            {
                CaveCarrotFlower.seedIndex = IndexManager.getUnusedObjectIndex();
            }
        }

        public static int getIndex()
        {
            if (CaveCarrotFlower.seedIndex == -1)
            {
                CaveCarrotFlower.setIndex();
            }

            return CaveCarrotFlower.seedIndex;
        }

        public static void setCropIndex()
        {
            if (CaveCarrotFlower.cropTextureIndex == -1)
            {
                CaveCarrotFlower.cropTextureIndex = IndexManager.getUnusedCropIndex();
            }
        }

        public static int getCropIndex()
        {
            if (CaveCarrotFlower.cropTextureIndex == -1)
            {
                CaveCarrotFlower.setCropIndex();
            }

            return CaveCarrotFlower.cropTextureIndex;
        }

        public CaveCarrotFlower() : this(Vector2.Zero)
        {   
        }

        public CaveCarrotFlower(Vector2 tileLocation) : base(seedIndex,(int) tileLocation.X, (int) tileLocation.Y)
        {   
        }

        public static string getCropData()
        {
            return String.Format(CROP_DATA_STRING,CaveCarrotSeed.getIndex(), CaveCarrotFlower.getCropIndex());
        }
    }
}
