// Tests that share static state (UprootedSettings._settingsPath, PlatformPaths.ProfileDirOverride)
// must run sequentially to avoid cross-test interference.
[CollectionDefinition("SequentialTests", DisableParallelization = true)]
public class SequentialTestsCollection { }
