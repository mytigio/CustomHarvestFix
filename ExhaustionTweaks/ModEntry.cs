using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;
using StardewModdingAPI;

namespace ExhaustionTweaks
{
    public class ModEntry : Mod
    {
        private IModHelper mHelper;
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            mHelper = helper;

            var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);

            // example patch, you'll need to edit this for your patch
            Farmer.passOutFromTired(null);
            
            harmony.Patch(
               original: AccessTools.Method(typeof(StardewValley.Farmer), nameof(StardewValley.Farmer.passOutFromTired)),
               prefix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.passOutFromTired_Prefix))
            );
        }

        public static bool passOutFromTired_Prefix(SObject __instance, Farmer who)
        {
            try
            {
                GameLocation passOutLocation = Game1.currentLocation;

                //turn this into a configurable list of locations.
                if (passOutLocation is StardewValley.Farm || passOutLocation is StardewValley.Locations.FarmCave)
                {
                    if (!who.IsLocalPlayer)
                        return;
                    if (who.isRidingHorse())
                        who.mount.dismount(false);
                    if (Game1.activeClickableMenu != null)
                    {
                        Game1.activeClickableMenu.emergencyShutDown();
                        Game1.exitActiveMenu();
                    }
                    who.completelyStopAnimatingOrDoingAction();
                    if ((bool)((NetFieldBase<bool, NetBool>)who.bathingClothes))
                        who.changeOutOfSwimSuit();
                    who.swimming.Value = false;
                    who.CanMove = false;
                    if (who == Game1.player && (FarmerTeam.SleepAnnounceModes)((NetFieldBase<FarmerTeam.SleepAnnounceModes, NetEnum<FarmerTeam.SleepAnnounceModes>>)Game1.player.team.sleepAnnounceMode) != FarmerTeam.SleepAnnounceModes.Off)
                    {
                        string str = "PassedOut";
                        int num = 0;
                        for (int index = 0; index < 2; ++index)
                        {
                            if (Game1.random.NextDouble() < 0.25)
                                ++num;
                        }
                        Game1.multiplayer.globalChatInfoMessage(str + (object)num, Game1.player.displayName);
                    }
                    GameLocation passOutLocation = Game1.currentLocation;
                    Vector2 bed = Utility.PointToVector2(Utility.getHomeOfFarmer(Game1.player).getBedSpot()) * 64f;
                    bed.X -= 64f;

                    LocationRequest.Callback callback = (LocationRequest.Callback)(() =>
                    {
                        who.Position = bed;
                        who.currentLocation.lastTouchActionLocation = bed;
                        if (!Game1.IsMultiplayer || Game1.timeOfDay >= 2600)
                            Game1.PassOutNewDay();
                        Game1.changeMusicTrack("none", false, Game1.MusicContext.Default);
                    });

                    return false;
                }

                return true;
                

                 // don't run original logic
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed in {nameof(passOutFromTired_Prefix)}:\n{ex}", LogLevel.Error);
                return true; // run original logic
            }
        }
    }
}
