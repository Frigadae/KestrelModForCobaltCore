﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using Microsoft.Extensions.Logging;

namespace KestrelMod
{
    public partial class Manifest : IModManifest, ISpriteManifest, IShipManifest, IStartershipManifest, IShipPartManifest, IArtifactManifest, ICardManifest
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

        //logger
        public ILogger? Logger { get; set; }

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

            KestrelStarterShip.AddLocalisation("Kestrel", "A cruiser from another universe. A runaway FTL jump beckons a new mission. Equipped with Burst Laser and Artemis Missile.");

            registry.RegisterStartership(KestrelStarterShip);
        }

        public void LoadManifest(IArtifactRegistry registry)
        {
            
        }

        public void LoadManifest(ICardRegistry registry)
        {

        }
    }
}