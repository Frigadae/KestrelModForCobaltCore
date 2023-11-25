using System;
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
    public class ArtifactManifest : ISpriteManifest, IArtifactManifest
    {
        public IEnumerable<DependencyEntry> Dependencies => new DependencyEntry[0];

        public DirectoryInfo? GameRootFolder { get; set; }
        public ILogger? Logger { get; set; }
        public DirectoryInfo? ModRootFolder { get; set; }

        public string Name => throw new NotImplementedException();

        public void LoadManifest(ISpriteRegistry artRegistry)
        {
            throw new NotImplementedException();
        }

        public void LoadManifest(IArtifactRegistry registry)
        {
            throw new NotImplementedException();
        }
    }
}
