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
    public partial class Manifest : IModManifest, ISpriteManifest, IShipManifest, IStartershipManifest, IShipPartManifest, IArtifactManifest, ICardManifest, IDeckManifest
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

        //kestrel deck
        private ExternalDeck? MissileDeck;

        //logger
        public ILogger? Logger { get; set; }


        //kestrel ship parts sprites
        private ExternalSprite? KestrelWingSprite;
        private ExternalSprite? KestrelCannonSprite;
        private ExternalSprite? KestrelCockpitSprite;
        private ExternalSprite? KestrelMissileSprite;
        private ExternalSprite? KestrelChassisSprite;
        private ExternalSprite? KestrelCannonHeavySprite;
        private ExternalSprite? KestrelMissileHeavySprite;

        //card sprites
        private ExternalSprite? KestrelCardBorderSprite;
        private ExternalSprite? KestrelLaserCardSprite;
        private ExternalSprite? KestrelMissileCardSprite;

        //artifact sprites
        private ExternalSprite? KestrelMissileArtifactSprite;

        //missile sprite
        private ExternalSprite? KestrelArtemisMissileSprite;

        //load sprite registry
        public void LoadManifest(ISpriteRegistry spriteRegistry)
        {
            if (ModRootFolder == null)
            {
                throw new Exception("Modrootfolder missing!");
            }

            //load kestrel wing sprite
            var KestrelWingSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("wing_kestrel.png"));
            KestrelWingSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelWing", new FileInfo(KestrelWingSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelWingSprite))
            {
                throw new Exception("kestrel wing sprite not loaded");
            };

            //load kestrel cannon sprite
            var KestrelCannonSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("cannon_kestrel.png"));
            KestrelCannonSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelCannon", new FileInfo(KestrelCannonSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelCannonSprite))
            {
                throw new Exception("kestrel cannon sprite not loaded");
            };

            //load kestrel cockpit sprite
            var KestrelCockpitSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("cockpit_kestrel.png"));
            KestrelCockpitSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelCockpit", new FileInfo(KestrelCockpitSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelCockpitSprite))
            {
                throw new Exception("kestrel cockpit sprite not loaded");
            };

            //load kestrel missiles sprite
            var KestrelMissileSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("missiles_kestrel.png"));
            KestrelMissileSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelMissiles", new FileInfo(KestrelMissileSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelMissileSprite))
            {
                throw new Exception("kestrel missile sprite not loaded");
            };

            //load kestrel cannon alt sprite
            var KestrelCannonAltSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("cannon_kestrel_alt.png"));
            KestrelCannonHeavySprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelAltCannon", new FileInfo(KestrelCannonAltSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelCannonHeavySprite))
            {
                throw new Exception("kestrel cannon heavy sprite not loaded");
            };

            //load kestrel missiles alt sprite
            var KestrelMissileHeavySpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("missiles_kestrel_alt.png"));
            KestrelMissileHeavySprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelMissilesHeavy", new FileInfo(KestrelMissileHeavySpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelMissileHeavySprite))
            {
                throw new Exception("kestrel missile heavy sprite not loaded");
            };

            //load kestrel chassis sprite
            var KestrelChassisSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("chassis_kestrel.png"));
            KestrelChassisSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelChassis", new FileInfo(KestrelChassisSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelChassisSprite))
            {
                throw new Exception("kestrel chassis sprite not loaded");
            };

            //load kestrel card border sprite
            var KestrelCardBorderSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("border_kestrel.png"));
            KestrelCardBorderSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.KestrelCardBorder", new FileInfo(KestrelCardBorderSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelCardBorderSprite))
            {
                throw new Exception("kestrel card border sprite not loaded");
            };

            //load burst laser card sprite
            var KestrelLaserCardSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("BurstLaser.png"));
            KestrelLaserCardSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.BurstLaser", new FileInfo(KestrelLaserCardSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelLaserCardSprite))
            {
                throw new Exception("burst laser card sprite not loaded");
            };

            //load artemis missile card sprite
            var KestrelMissileCardSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("ArtemisMissile.png"));
            KestrelMissileCardSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.ArtemisMissile", new FileInfo(KestrelMissileCardSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelMissileCardSprite))
            {
                throw new Exception("artemis missile card sprite not loaded");
            };

            //load artemis missile artifact sprite
            var KestrelMissileArtifactSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("ArtemisMissileArtifact.png"));
            KestrelMissileArtifactSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.ArtemisMissileArtifact", new FileInfo(KestrelMissileArtifactSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelMissileArtifactSprite))
            {
                throw new Exception("artemis missile artifact sprite not loaded");
            };

            //load artemis missile object sprite
            var KestrelArtemisMissileSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("missile_federation.png"));
            KestrelArtemisMissileSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.ArtemisMissileObject", new FileInfo(KestrelArtemisMissileSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelArtemisMissileSprite))
            {
                throw new Exception("artemis missile object sprite not loaded");
            };
        }


        //load mod manifest
        void IModManifest.BootMod(IModLoaderContact contact)
        {
            //throw new Exception("Not implemented!");
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
                KestrelCannonHeavySprite ?? throw new Exception("kestrel cannon heavy not loaded"),
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
            shipPartRegistry.RegisterPart(KestrelCannonHeavyPart);
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

        public void LoadManifest(IArtifactRegistry registry)
        {
            {
                if (KestrelMissileArtifactSprite == null)
                {
                    throw new Exception("Kestrel missile artifact is null!");
                }

                FederationMissile = new ExternalArtifact("Frigadae.KestrelMod.FederationMissileArtifact", typeof(FederationMissile), KestrelMissileArtifactSprite, new ExternalGlossary[0], null, null);
                FederationMissile.AddLocalisation("Federation Missile", "Fires a missile and a piercing shot. A limited amount of missiles can be fired per combat.");

                registry.RegisterArtifact(FederationMissile);
            }
        }


        public void LoadManifest(IDeckRegistry registry)
        {
            MissileDeck = new ExternalDeck("Frigadae.KestrelMod.FederationMissileDeck", System.Drawing.Color.Beige, System.Drawing.Color.WhiteSmoke,
                KestrelMissileCardSprite ?? throw new Exception("Kestrel card sprite is null!"),
                KestrelCardBorderSprite ?? throw new Exception("Kestrel card border is null!"),
                null);

            if (!registry.RegisterDeck(MissileDeck))
            {
                throw new Exception("Kestrel deck not loaded");
            }
        }

        public void LoadManifest(ICardRegistry registry)
        {
            if (KestrelMissileCardSprite == null)
            {
                throw new Exception("Kestrel card sprite is null!");
            }

            var missileCard = new ExternalCard("Frigadae.KestrelMod.FederationMissileCard", typeof(LaunchFederationMissile), KestrelMissileCardSprite, null);
            missileCard.AddLocalisation("Federation Missile", "Fires a missile and a piercing shot");

            registry.RegisterCard(missileCard);
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
                new ExternalCard[0]
                {

                },
                new ExternalArtifact[1]
                {
                    FederationMissile ?? throw new Exception("Federation missile artifact not found!"),
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
    }
}