namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Data
{
    public interface IQuadtreeConfig
    {
        int Depth { get; }
        public int MinNodeSize { get; }
        public int PreferredMaxObjectsPerNode { get; }
    }
}