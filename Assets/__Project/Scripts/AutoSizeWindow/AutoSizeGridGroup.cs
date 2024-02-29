using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.AutoSize
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class AutoSizeGridGroup : MonoBehaviour, IAutoSize
    {
        private RectTransform baseContainer;
        [SerializeField] private GridLayoutGroup.Axis axis = GridLayoutGroup.Axis.Horizontal;

        [SerializeField] private Operation operation = Operation.Count;
        [HideIf("operation", Operation.Count)]
        [SerializeField] private Form form = Form.Cube;

        #region Count
        [TitleGroup("Count")]
        [VerticalGroup("Count/Parme")]
        [ShowIf("operation", Operation.Count)]
        [SerializeField]
        private int count = 2;
        public int Count { private get { return count; } set { count = value; } }
        #endregion

        #region Procent
        //[TitleGroup("Procent")]
        //[VerticalGroup("Procent/Parme")]
        //[ShowIf("operation", Operation.Procent)]
        //[SerializeField]
        //private 
        #endregion


        #region Square
        [TitleGroup("Square")]
        [VerticalGroup("Square/Parme")]
        [ShowIf(nameof(isValidSquare))]
        [Range(0f, 1f)]
        [SerializeField]
        private float procentWidth;

        [TitleGroup("Square")]
        [VerticalGroup("Square/Parme")]
        [ShowIf(nameof(isValidSquare))]
        [Range(0f, 1f)]
        [SerializeField]
        private float procentHeigt;

        private bool isValidSquare => operation != Operation.Count && form == Form.Square;
        private bool isValidCubeAll => operation != Operation.Count && form == Form.Cube;
        #endregion

        #region Cube
        [TitleGroup("Cube")]
        [VerticalGroup("Cube/Parme")]
        [ShowIf(nameof(isValidCubeAll))]
        [Range(0f, 1f)]
        [SerializeField] private float procentAll;
        #endregion

        public RectTransform Container
        {
            get => this.baseContainer != null ? this.baseContainer : this.baseContainer = this.GetComponent<RectTransform>();
        }

        //private bool isInit = false;
        private GridLayoutGroup grid;
        public GridLayoutGroup Grid
        {
            get
            {
                return this.grid == null ? this.grid = GetComponent<GridLayoutGroup>() : this.grid;
            }
        }

        public void AutoSize()
        {
            NewSizeGrid();
        }
        [Button]
        private void NewSizeGrid()
        {
            var vector = new Vector2();
            switch (operation)
            {
                case Operation.Count:
                    vector = CubeCount();
                    break;
                case Operation.Procent:
                    switch (form)
                    {
                        case Form.Cube:
                            vector = ProcentCube();
                            break;
                        case Form.Square:
                            vector = ProcentOther();
                            break;
                    }
                    break;
            }
            Grid.cellSize = vector;
            Grid.startAxis = axis;
            // isInit = true;
        }
        private float SizeCube(float value) => axis == GridLayoutGroup.Axis.Horizontal ? value - Grid.spacing.x : value - Grid.spacing.y;
        private Vector2 CubeCount()
        {
            var size = this.size;
            var newSize = size / Count;

            float sizeCube = SizeCube(newSize);

            var sizeForVector2 = axis == GridLayoutGroup.Axis.Horizontal ? (sizeCube > Container.rect.height ? Container.rect.height : sizeCube) : (sizeCube > Container.rect.width ? Container.rect.width : sizeCube);
            var newCellSize = new Vector2(sizeForVector2, sizeForVector2);
            return newCellSize;
        }
        private Vector2 ProcentCube()
        {
            var size = this.size;
            float sizeCube = size * procentAll;

            var sizeForVector2 = axis == GridLayoutGroup.Axis.Horizontal ? (sizeCube > Container.rect.height ? Container.rect.height : sizeCube) : (sizeCube > Container.rect.width ? Container.rect.width : sizeCube);
            var newCellSize = new Vector2(sizeForVector2, sizeForVector2);
            return newCellSize;
        }
        private Vector2 ProcentOther()
        {
            var sizeWidht = Container.rect.width;
            var sizeHeight = Container.rect.height;

            var newSizeWidht = sizeWidht * procentWidth;
            var newSizeHeight = sizeHeight * procentHeigt;

            var newCellSize = new Vector2(newSizeWidht, newSizeHeight);
            return newCellSize;
        }

        public float size
        {
            get { return axis == GridLayoutGroup.Axis.Horizontal ? Container.rect.width : Container.rect.height; }
            set { }
        }
    }

    public enum Operation
    {
        Count,
        Procent
    }
    public enum Form
    {
        Cube,
        Square
    }
}