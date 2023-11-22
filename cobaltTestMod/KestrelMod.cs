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
    public class Manifest : IModManifest, ISpriteManifest, IShipManifest, IStartershipManifest, IShipPartManifest
    {

        //kestrel mod manifest name
        public DirectoryInfo? ModRootFolder { get; set; }
        public DirectoryInfo? GameRootFolder { get; set; }

        public string Name => "Frigadae.KestrelMod.KestrelModManifest";

        public IEnumerable<DependencyEntry> Dependencies => new DependencyEntry[0];

        Microsoft.Extensions.Logging.ILogger? IManifest.Logger { get; set; }


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

        //load mod manifest
        void IModManifest.BootMod(IModLoaderContact contact)
        {
            //nothing here
        }

        //load sprite registry
        public void LoadManifest(ISpriteRegistry spriteRegistry)
        {
            if (ModRootFolder == null)
            {
                throw new Exception("Modrootfolder missing!");
            }

            //load kestrel wing sprite
            var KestrelWingSpriteFile = Path.Combine(ModRootFolder?.FullName??"", "Sprites", Path.GetFileName("wing_kestrel.png"));
            KestrelWingSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelWing", new FileInfo(KestrelWingSpriteFile));
            spriteRegistry.RegisterArt(KestrelWingSprite);

            //load kestrel cannon sprite
            var KestrelCannonSpriteFile = Path.Combine(ModRootFolder?.FullName ?? "", "Sprites", Path.GetFileName("cannon_kestrel.png"));
            KestrelCannonSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelCannon", new FileInfo(KestrelCannonSpriteFile));
            spriteRegistry.RegisterArt(KestrelCannonSprite);

            //load kestrel cockpit sprite
            var KestrelCockpitSpriteFile = Path.Combine(ModRootFolder?.FullName ?? "", "Sprites", Path.GetFileName("cockpit_kestrel.png"));
            KestrelCockpitSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelCockpit", new FileInfo(KestrelCockpitSpriteFile));
            spriteRegistry.RegisterArt(KestrelCockpitSprite);

            //load kestrel missiles sprite
            var KestrelMissileSpriteFile = Path.Combine(ModRootFolder?.FullName ?? "", "Sprites", Path.GetFileName("missiles_kestrel.png"));
            KestrelMissileSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelMissiles", new FileInfo(KestrelMissileSpriteFile));
            spriteRegistry.RegisterArt(KestrelCannonSprite);

            //load kestrel cannon alt sprite
            var KestrelCannonAltSpriteFile = Path.Combine(ModRootFolder?.FullName ?? "", "Sprites", Path.GetFileName("cannon_kestrel_alt.png"));
            KestrelCannonHeavySprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelAltCannon", new FileInfo(KestrelCannonAltSpriteFile));
            spriteRegistry.RegisterArt(KestrelCannonHeavySprite);

            //load kestrel chassis sprite
            var KestrelChassisSpriteFile = Path.Combine(ModRootFolder?.FullName ?? "", "Sprites", Path.GetFileName("chassis_kestrel.png"));
            KestrelChassisSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelChassis", new FileInfo(KestrelChassisSpriteFile));
            spriteRegistry.RegisterArt(KestrelChassisSprite);
        }

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
                "Frigadae.KestrelMod.Parts.KestrelWingLeft",
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
                "Frigadae.KestrelMod.Parts.KestrelCannon",
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
                "Frigadae.KestrelMod.Parts.KestrelHeavyCannon",
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
                "Frigadae.KestrelMod.Parts.KestrelCockpit",
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
                "Frigadae.KestrelMod.Parts.KestrelCockpit",
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
                "Frigadae.KestrelMod.Parts.KestrelWingRight",
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

            //new kestrel ship and stats
            KestrelShip = new ExternalShip("Frigadae.KestrelMod.KestrelShip.Ship",
                new Ship()
                {
                    baseDraw = 5,
                    baseEnergy = 3,
                    heatTrigger = 3,
                    heatMin = 0,
                    hull = 8,
                    hullMax = 8,
                    shieldMaxBase = 4
                },
                KestrelParts,
                KestrelChassisSprite ?? throw new Exception("kestrel chassis sprite unable to be loaded"),
                null
            );

            shipRegistry.RegisterShip(KestrelShip);
        }

        //load startership registry
        public void LoadManifest(IStartershipRegistry registry)
        {
            if (KestrelShip == null)
            {
                throw new Exception("kestrelship unable to be loaded");
            }

            ExternalStarterShip KestrelStarterShip = new ExternalStarterShip(
                "Frigadae.KestrelMod.KestrelShip.StarterShip",
                KestrelShip.GlobalName,
                new ExternalCard[0] 
                {
                    
                },
                new ExternalArtifact[0] 
                {

                },
                new Type[] 
                {
                    typeof (CannonColorless),
                    typeof (CannonColorless),
                    typeof (DodgeColorless),
                    typeof (BasicShieldColorless)
                },
                new Type[] 
                {
                    typeof(ShieldPrep)
                }
            );

            KestrelStarterShip.AddLocalisation("The Kestrel", "A botched FTL jump sent this ship into this universe. Features the Burst Laser II weapon system.");

            registry.RegisterStartership(KestrelStarterShip);
        }
    }
}