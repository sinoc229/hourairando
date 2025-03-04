namespace NSShanghaiEXE.Map
{
    public class UnboundedMap
    {
        private const byte OutOfBoundsTileType = 0;

        private int[,,] definedMap;

        public UnboundedMap(int[,,] definedMap)
        {
            this.definedMap = definedMap;
        }

        public int this[int z, int x, int y]
        {
            get
            {
                return IsInBounds(z, x, y) ? this.definedMap[z, x, y] : OutOfBoundsTileType;
            }

            set
            {
                if (this.IsInBounds(z, x, y))
                {
                    this.definedMap[z, x, y] = value;
                }
            }
        }

        public int GetLength(int dim) => this.definedMap.GetLength(dim);

        private bool IsInBounds(int z, int x, int y)
        {
            var isInBounds = z >= 0 && x >= 0 && y >= 0
                && z < this.GetLength(0)
                && x < this.GetLength(1)
                && y < this.GetLength(2);

            if (!isInBounds)
            {
                // Game tried to access out-of-map area
                // Silently fail
            }

            return isInBounds;
        }
    }
}
