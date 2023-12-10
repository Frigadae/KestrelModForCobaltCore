﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using Microsoft.Extensions.Logging;

namespace KestrelMod
{
    public partial class Manifest: ISpriteManifest
    {
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
            var KestrelMissileCardSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("BurstLaser.png"));
            KestrelMissileCardSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.ArtemisMissile", new FileInfo(KestrelMissileCardSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelMissileCardSprite))
            {
                throw new Exception("artemis missile card sprite not loaded");
            };

            //load artemis missile artifact sprite
            var KestrelMissileArtifactSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("BurstLaser.png"));
            KestrelMissileArtifactSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.ArtemisMissileArtifact", new FileInfo(KestrelMissileArtifactSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelMissileArtifactSprite))
            {
                throw new Exception("artemis missile artifact sprite not loaded");
            };

            //load artemis missile object sprite
            var KestrelArtemisMissileSpriteFile = Path.Combine(ModRootFolder.FullName, "Sprites", Path.GetFileName("BurstLaser.png"));
            KestrelArtemisMissileSprite = new ExternalSprite("Frigadae.KestrelMod.Sprites.ArtemisMissileObject", new FileInfo(KestrelArtemisMissileSpriteFile));
            if (!spriteRegistry.RegisterArt(KestrelArtemisMissileSprite))
            {
                throw new Exception("artemis missile object sprite not loaded");
            };
        }
    }
}