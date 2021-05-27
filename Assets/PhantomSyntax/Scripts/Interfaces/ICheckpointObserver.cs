namespace PhantomSyntax.Scripts.Interfaces {
    public interface ICheckpointObserver {
        int CheckpointsNeeded { get; set; }
        void UpdateCheckpointUI();
        void StopObjectSpawning();
    }
}