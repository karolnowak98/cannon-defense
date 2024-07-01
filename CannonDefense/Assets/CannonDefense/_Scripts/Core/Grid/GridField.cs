using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid
{
    public class GridField<T> where T: IGridElement
    {
        public Vector3 CellPosition { get;  }
        
        public GridField(Vector3 cellPosition)
        {
            CellPosition = cellPosition;
        }
    }
}