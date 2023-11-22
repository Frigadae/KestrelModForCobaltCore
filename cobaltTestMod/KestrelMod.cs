using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;

namespace KestrelMod
{
    public class KestrelMod : IManifest, IShipManifest, IStartershipManifest
    {
        public DirectoryInfo? ModRootFolder { get; set; }
        public DirectoryInfo? GameRootFolder { get; set; }

        //kestrel mod manifest name
        string IManifest.Name => "Frigadae.KestrelMod";
        public string Name = "Frigadae.KestrelMod";

        //kestrel ship parts sprites
        public static ExternalSprite? KestrelWingSprite;
        public static ExternalSprite? KestrelCannonSprite;
        public static ExternalSprite? KestrelCockpitSprite;
        public static ExternalSprite? KestrelMissileSprite;
        public static ExternalSprite? KestrelChassisSprite;
        public static ExternalSprite? KestrelCannonHeavySprite;

        //kestrel external ship obj
        public static ExternalShip? KestrelShip;

        //kestrel ship part objects
        public static ExternalPart? KestrelWingLeftPart;
        public static ExternalPart? KestrelCannonPart;
        public static ExternalPart? KestrelCockpitPart;
        public static ExternalPart? KestrelMissilePart;
        public static ExternalPart? KestrelWingRightPart;
        public static ExternalPart? KestrelCannonHeavyPart;

        public void LoadManifest(IShipPartRegistry shipPartRegistry)
        {
            //initialise ship parts
            //initialise kestrel left wing
            Part KestrelWingLeftPartObj = new Part()
            {
                active = true,
                damageModifier = PDamMod.none,
                type = PType.wing
            };

            KestrelWingLeftPart = new ExternalPart(
                "Frigadae.Kestrel.Parts.KestrelWingLeft",
                KestrelWingLeftPartObj,
                KestrelWingSprite ?? throw new Exception("kestrelwingsprite unable to be loaded"),
                null
            );

            //initialise kestrel cannon
            Part KestrelCannonPartObj = new Part()
            {
                active = true,
                damageModifier = PDamMod.none,
                type = PType.cannon
            };

            KestrelCannonPart = new ExternalPart(
                "Frigadae.Kestrel.Parts.KestrelCannon",
                KestrelCannonPartObj,
                KestrelCannonSprite ?? throw new Exception("kestrelcannon unable to be loaded"),
                null
            );

            //initialise kestrel heavy cannon
            Part KestrelCannonHeavyPartObj = new Part() 
            {
                active = true, 
                damageModifier = PDamMod.none,
                type = PType.cannon
            };

            KestrelCannonHeavyPart = new ExternalPart(
                "Frigadae.Kestrel.Parts.KestrelHeavyCannon",
                KestrelCannonHeavyPartObj,
                KestrelCannonHeavySprite ?? throw new Exception("kestrelcannonheavy unable to be loaded"),
                null
            );

            //initialise kestrel cockpit
            Part KestrelCockpitPartObj = new Part()
            {
                active = true,
                damageModifier = PDamMod.none,
                type = PType.cockpit
            };

            KestrelCockpitPart = new ExternalPart(
                "Frigadae.Kestrel.Parts.KestrelCockpit",
                KestrelCockpitPartObj,
                KestrelCockpitSprite ?? throw new Exception("kestrelcockpit unable to be loaded"),
                null
            );

            //initialise kestrel missile
            Part KestrelMissilePartObj = new Part()
            {
                active = true,
                damageModifier = PDamMod.none,
                type = PType.missiles
            };

            KestrelMissilePart = new ExternalPart(
                "Frigadae.Kestrel.Parts.KestrelCockpit",
                KestrelMissilePartObj,
                KestrelMissileSprite ?? throw new Exception("kestrelmissiles unable to be loaded"),
                null
            );

            //initialise kestrel right wing
            Part KestrelWingRightPartObj = new Part()
            {
                active = true,
                damageModifier = PDamMod.none,
                type = PType.wing,
                flip = true
            };

            KestrelWingRightPart = new ExternalPart(
                "Frigadae.Kestrel.Parts.KestrelWingRight",
                KestrelWingRightPartObj,
                KestrelWingSprite ?? throw new Exception("kestrelwingsprite unable to be loaded"),
                null
            );

            //register parts
            shipPartRegistry.RegisterPart(KestrelWingLeftPart);
            shipPartRegistry.RegisterPart(KestrelCannonPart);
            shipPartRegistry.RegisterPart(KestrelCannonHeavyPart);
            shipPartRegistry.RegisterPart(KestrelCockpitPart);
            shipPartRegistry.RegisterPart(KestrelMissilePart);
            shipPartRegistry.RegisterPart(KestrelWingRightPart);
        }

        //load ship registry
        public void LoadManifest(IShipRegistry shipRegistry)
        {
            //array for kestrel parts
            ExternalPart[] KestrelParts = {
                KestrelWingLeftPart ?? throw new Exception("kestrelwingleftpart unable to be loaded"),
                KestrelCannonPart ?? throw new Exception("kestrelcannonpart unable to be loaded"),
                KestrelCockpitPart ?? throw new Exception("kestrelcockpitpart unable to be loaded"),
                KestrelMissilePart ?? throw new Exception("kestrelmissilepart unable to be loaded"),
                KestrelCannonHeavyPart ?? throw new Exception("kestrelcannonheavy part unable to be loaded"),
                KestrelWingRightPart ?? throw new Exception("kestrelwingrightpart unable to be loaded")
            };

            //new kestrel ship
            KestrelShip = new ExternalShip("Frigadae.Kestrel.KestrelShip.Ship",
                new Ship(),
                KestrelParts,
                KestrelChassisSprite ?? throw new Exception("kestrel chassis sprite unable to be loaded"),
                null
            );

            shipRegistry.RegisterShip(KestrelShip);
        }

        public void LoadManifest(IStartershipRegistry registry)
        {
            if (KestrelShip == null)
            {
                throw new Exception("kestrelship unable to be loaded");
            }

            ExternalStarterShip KestrelStarterShip = new ExternalStarterShip(
                "Frigadae.KestrelMod.KestrelShip.StarterShip",
                KestrelShip.GlobalName,
                new ExternalCard[0] { },
                new ExternalArtifact[0] { },
                new Type[0] { },
                new Type[0] { }
            );

            registry.RegisterStartership(KestrelStarterShip);
        }
    }
}