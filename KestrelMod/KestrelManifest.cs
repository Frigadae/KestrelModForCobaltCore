using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using KestrelMod.Cards;
using KestrelMod.CardActions;
using Microsoft.Extensions.Logging;
using KestrelMod.Artifacts;

namespace KestrelMod
{
    public partial class KestrelManifest : IModManifest, IShipManifest, IStartershipManifest, IShipPartManifest, IArtifactManifest, ICardManifest, IDeckManifest, IStatusManifest
    {
        //kestrel mod manifest name
        public DirectoryInfo? ModRootFolder { get; set; }
        public DirectoryInfo? GameRootFolder { get; set; }

        public string Name => "Frigadae.KestrelMod.KestrelModManifest";

        public IEnumerable<DependencyEntry> Dependencies => new DependencyEntry[0];

        //kestrel external ship obj
        private ExternalShip? KestrelShip;

        //kestrel ship part objects
        private ExternalPart? KestrelWingLeftPart;
        private ExternalPart? KestrelCannonPart;
        private ExternalPart? KestrelCockpitPart;
        private ExternalPart? KestrelMissilePart;
        private ExternalPart? KestrelWingRightPart;
        private ExternalPart? KestrelCannonHeavyPart;
        private ExternalPart? KestrelMissileHeavyPart;

        //external artifacts
        private ExternalArtifact? FederationMissile;
        private ExternalArtifact? FederationLaser;

        //kestrel deck
        private ExternalDeck? MissileDeck;
        private ExternalDeck? BurstLaserDeck;

        //external card
        private ExternalCard? MissileCard;
        private ExternalCard? BurstLaserCard;

        //external status
        public static ExternalStatus? WeaponCharge;

        //status enumerable
        //public static Dictionary<string, ExternalStatus> StatusArray = new Dictionary<string, ExternalStatus>();

        //logger
        public ILogger? Logger { get; set; }

        //load mod manifest
        void IModManifest.BootMod(IModLoaderContact contact)
        {
            //throw new Exception("Not implemented!");
        }

        //ship part registry
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
                KestrelWingSprite ?? throw new Exception("kestrel wings prite not loaded"),
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
                KestrelCannonSprite ?? throw new Exception("kestrel cannon not loaded"),
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
                KestrelCockpitSprite ?? throw new Exception("kestrel cockpit not loaded"),
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
                "Frigadae.KestrelMod.Parts.KestrelMissile",
                KestrelMissilePartObj,
                KestrelMissileSprite ?? throw new Exception("kestrel missiles not loaded"),
                null
            );

            //initialise kestrel heavy cannon
            Part KestrelMissileHeavyPartObj = new Part()
            {
                active = true,
                damageModifier = PDamMod.none,
                type = PType.cannon
            };
            KestrelMissileHeavyPart = new ExternalPart(
                "Frigadae.KestrelMod.Parts.KestrelMissileHeavy",
                KestrelMissileHeavyPartObj,
                KestrelMissileHeavySprite ?? throw new Exception("kestrel missiles heavy not loaded"),
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
                KestrelWingSprite ?? throw new Exception("kestrel wing sprite not loaded"),
                null
            );

            //register parts
            shipPartRegistry.RegisterPart(KestrelWingLeftPart);
            shipPartRegistry.RegisterPart(KestrelCannonPart);
            shipPartRegistry.RegisterPart(KestrelCockpitPart);
            shipPartRegistry.RegisterPart(KestrelMissilePart);
            shipPartRegistry.RegisterPart(KestrelMissileHeavyPart);
            shipPartRegistry.RegisterPart(KestrelWingRightPart);
        }


        //load ship registry
        public void LoadManifest(IShipRegistry shipRegistry)
        {
            //array for kestrel parts
            ExternalPart[] KestrelParts = {
                KestrelWingLeftPart ?? throw new Exception("kestrel wing left part not loaded"),
                KestrelMissilePart ?? throw new Exception("kestrel missile part not loaded"),
                KestrelCockpitPart ?? throw new Exception("kestrel cockpit part not loaded"),
                KestrelCannonPart ?? throw new Exception("kestrel cannon part not loaded"),
                KestrelWingRightPart ?? throw new Exception("kestrel wing right part not loaded")
            };

            //new kestrel ship and stats
            KestrelShip = new ExternalShip("Frigadae.KestrelMod.KestrelShip.Ship",
                new Ship()
                {
                    baseDraw = 5,
                    baseEnergy = 3,
                    heatTrigger = 3,
                    heatMin = 0,
                    hull = 10,
                    hullMax = 10,
                    shieldMaxBase = 4
                },
                KestrelParts,
                KestrelChassisSprite ?? throw new Exception("kestrel chassis sprite not loaded"),
                null
            );

            shipRegistry.RegisterShip(KestrelShip);
        }

        //artifact registry
        public void LoadManifest(IArtifactRegistry registry)
        {
            //federation missile artifact
            {
                if (KestrelMissileArtifactSprite == null)
                {
                    throw new Exception("Kestrel missile artifact is null!");
                }

                FederationMissile = new ExternalArtifact("Frigadae.KestrelMod.FederationMissileArtifact", typeof(FederationMissile), KestrelMissileArtifactSprite, new ExternalGlossary[0], MissileDeck, null, null);
                FederationMissile.AddLocalisation("Federation Missiles", "Fires a missile and a piercing shot. A limited amount of missiles can be fired per combat.");

                registry.RegisterArtifact(FederationMissile);
            }

            //federation laser artifact
            {
                if (KestrelLaserArtifactSprite == null)
                {
                    throw new Exception("Kestrel federation laser artifact is null!");
                }

                FederationLaser = new ExternalArtifact("Frigadae.KestrelMod.FederationLaserArtifact", typeof(FederationLaser), KestrelLaserArtifactSprite, new ExternalGlossary[0], BurstLaserDeck, null, null);
                FederationLaser.AddLocalisation("Federation Laser", "Adds one charge each turn, on full charge fires three shots dealing 1 DMG");

                registry.RegisterArtifact(FederationLaser);
            }
        }

        //deck registry
        public void LoadManifest(IDeckRegistry registry)
        {
            //missile deck
            {
                MissileDeck = new ExternalDeck("Frigadae.KestrelMod.FederationMissileDeck", System.Drawing.Color.Beige, System.Drawing.Color.Black,
                KestrelMissileCardSprite ?? throw new Exception("Kestrel card sprite is null!"),
                KestrelCardBorderSprite ?? throw new Exception("Kestrel card border sprite is null!"),
                null);

                if (!registry.RegisterDeck(MissileDeck))
                {
                    throw new Exception("missile deck not loaded");
                }
            }

            //burst laser deck
            {
                BurstLaserDeck = new ExternalDeck("Frigadae.KestrelMod.FederationBurstLaserDeck", System.Drawing.Color.Beige, System.Drawing.Color.Black,
                KestrelLaserCardSprite ?? throw new Exception("Kestrel card sprite is null!"),
                KestrelCardBorderSprite ?? throw new Exception("Kestrel card border sprite is null!"),
                null);

                if (!registry.RegisterDeck(BurstLaserDeck))
                {
                    throw new Exception("burst laser deck not loaded");
                }
            }
        }

        //card registry
        public void LoadManifest(ICardRegistry registry)
        {
            //register artemis missile card
            { 
                if (KestrelMissileCardSprite == null)
                {
                    throw new Exception("Kestrel missile card sprite is null!");
                }

                MissileCard = new ExternalCard("Frigadae.KestrelMod.FederationMissileCard", typeof(LaunchFederationMissile), KestrelMissileCardSprite, MissileDeck);
                MissileCard.AddLocalisation("Artemis Missile", "Fires a missile and a piercing shot");

                registry.RegisterCard(MissileCard);
            }

            //register burst laser card
            {
                if (KestrelLaserCardSprite == null)
                {
                    throw new Exception("Kestrel burst laser card sprite is null!");
                }

                BurstLaserCard = new ExternalCard("Frigadae.KestrelMod.FederationLaserCard", typeof(FireBurstLaser), KestrelLaserCardSprite, BurstLaserDeck);
                BurstLaserCard.AddLocalisation("Burst Laser", "Fires three normal shots");

                registry.RegisterCard(BurstLaserCard);
            }
        }

        //load startership registry
        public void LoadManifest(IStartershipRegistry registry)
        {
            if (KestrelShip == null)
            {
                throw new Exception("kestrel ship unable to be loaded");
            }

            ExternalStarterShip KestrelStarterShip = new ExternalStarterShip(
                "Frigadae.KestrelMod.KestrelShip.StarterShip",
                KestrelShip.GlobalName,
                new ExternalCard[]
                {
                    MissileCard ?? throw new Exception("Artemis Missile card not found!"),
                    BurstLaserCard ?? throw new Exception("Burst Laser card not found!")
                },
                new ExternalArtifact[]
                {
                    FederationMissile ?? throw new Exception("Federation missiles artifact not found!"),
                    FederationLaser ?? throw new Exception("Federation laser artifact not found!"),
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

            KestrelStarterShip.AddLocalisation("Kestrel", "A cruiser from another universe. A runaway FTL jump beckons a new mission. Equipped with Burst Laser and Artemis Missile.");

            registry.RegisterStartership(KestrelStarterShip);
        }

        //status registry
        public void LoadManifest(IStatusRegistry registry)
        {
            if (CooldownChargeSprite == null)
            {
                throw new Exception("Cooldown charge status sprite not loaded!");
            }

            WeaponCharge = new ExternalStatus("Frigadae.KestrelMod.Status.WeaponCharge", true, System.Drawing.Color.Black, System.Drawing.Color.LightSalmon, CooldownChargeSprite, false);
            WeaponCharge.AddLocalisation("Weapon charge", "Gain a charge every turn, on full charge can be used to fire burst laser.");
            registry.RegisterStatus(WeaponCharge);
            //StatusArray["WeaponCharge"] = WeaponCharge;
        }
    }
}