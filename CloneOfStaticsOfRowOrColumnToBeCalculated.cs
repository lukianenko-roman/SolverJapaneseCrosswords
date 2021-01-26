using System.Collections.Generic;

namespace SolverJapaneseCrosswords
{
    class CloneOfStaticsOfRowOrColumnToBeCalculated
    {
        // применяется в случае перебора всех вариантов. представляет собой "слепок"
        internal bool isMainFieldFilled;
        internal Cell[,] mainField;
        internal int totalFilledCells;
        internal List<bool[]> possibleSolutions;
        internal RowOrColumnToBeCalculated[] cloneRowOrColumnToBeCalculated;

        internal CloneOfStaticsOfRowOrColumnToBeCalculated(RowOrColumnToBeCalculated[] rowOrColumnToBeCalculated, int index)
        {
            isMainFieldFilled = RowOrColumnToBeCalculated.IsMainFieldFilled;
            totalFilledCells = RowOrColumnToBeCalculated.TotalFilledCells;
            mainField = new Cell[RowOrColumnToBeCalculated.RowsCount, RowOrColumnToBeCalculated.ColumnsCount];
            mainField = RowOrColumnToBeCalculated.CloneMainField();
            possibleSolutions = new List<bool[]>();

            cloneRowOrColumnToBeCalculated = new RowOrColumnToBeCalculated[RowOrColumnToBeCalculated.RowsCount + RowOrColumnToBeCalculated.ColumnsCount];

            RowOrColumnToBeCalculated.CloneArray(rowOrColumnToBeCalculated, cloneRowOrColumnToBeCalculated);
        }
    }
}
