using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid
{
    public abstract class Grid<T, T2> where T : GridField<T2> where T2: IGridElement
    {
        protected GridField<T2>[,] Fields;

        protected Grid(int xSize, int ySize)
        {
            Fields = new GridField<T2>[xSize, ySize];
        }
        
        protected Grid(MeshCollider meshCollider, int gridSizeX, int gridSizeY)
        {
            var size = meshCollider.bounds.size;
            var cellWidth = size.x / gridSizeX;
            var cellHeight = size.z / gridSizeY;

            Fields = new GridField<T2>[gridSizeX, gridSizeY];

            for (var x = 0; x < gridSizeX; x++)
            {
                for (var y = 0; y < gridSizeY; y++)
                {
                    var cellPosition = new Vector3(x * cellWidth, 0, y * cellHeight);
                    Fields[x, y] = new GridField<T2>(cellPosition);
                    // Initialize your grid field here, for example:
                    // Fields[x, y] = new YourGridFieldClass(cellPosition);
                }
            }
        }
    }
}