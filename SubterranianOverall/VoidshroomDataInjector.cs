﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubterranianOverhaul
{
    class VoidshroomDataInjector : IAssetEditor
    {
        private IMonitor monitor;
        private VoidshroomSpore spore;

        public VoidshroomDataInjector(IMonitor monitor)
        {
            this.monitor = monitor;
            VoidshroomSpore.setIndex(); //get an item index for voidshroom spores if one isn't already set.
            this.spore = new VoidshroomSpore();
        }

        public bool CanEdit<T>(IAssetInfo asset)
        {
            return (asset.AssetNameEquals("Maps\\springobjects") || asset.AssetNameEquals("Data\\ObjectInformation"));
        }

        public void Edit<T>(IAssetData asset)
        {
            if (asset.AssetNameEquals("Maps\\springobjects"))
            {
                IAssetDataForImage editor = asset.AsImage();
                Texture2D data = editor.Data;
                Texture2D texture2D = new Texture2D(Game1.graphics.GraphicsDevice, data.Width, Math.Max(data.Height, 4096));
                editor.ReplaceWith(texture2D);
                try
                {
                    editor.PatchImage(TextureSet.voidShroomSpore, new Rectangle?(), new Rectangle?(this.objectRect(spore.ParentSheetIndex)), PatchMode.Replace);
                }
                catch (Exception)
                {   
                }
            } else if (asset.AssetNameEquals("Data\\ObjectInformation"))
            {
                IAssetDataForDictionary<int, string> editor = asset.AsDictionary<int, string>();

                IDictionary<int, string> data = editor.Data;
                if (!data.ContainsKey(spore.ParentSheetIndex))
                {
                    data.Add(spore.ParentSheetIndex, spore.getObjectData());
                }
            }
        }

        private Rectangle objectRect(int index)
        {
            return new Rectangle(index % 24 * 16, index / 24 * 16, 16, 16);
        }

        private void log(string message)
        {
            if(this.monitor != null)
            {
                this.monitor.Log(message);
            }
        }
    }
}
