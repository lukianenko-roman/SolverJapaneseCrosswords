using System;

namespace SolverJapaneseCrosswords
{
    class RowOrColumnToBeCalculatedComparator : IComparable<RowOrColumnToBeCalculatedComparator>
    {
        // Класс для сортировки массива экземпляров класса RowOrColumnToBeCalculated. Сортировка от незаполненной строки/столбца с наименьшим количеством вариантов расположения условия
        // к наибольшему количеству вариантов расположения, только после них - заполненные строки/столбцы
        // Сортировка применяется для вызова элемента массива, для которого расчет будет самым быстрым
        // При необходимости поменять логику сортировки - поменять метод CompareTo
        internal bool IsRow { get; set; }
        private protected bool IsFull { get; set; }
        private protected bool IsNeedToRemoveVariants { get; set; }
        internal int VariantsCount { get; set; }
        internal int IndexOfRowOrColumn { get; set; }
        internal RowOrColumnToBeCalculatedComparator() { }
        internal RowOrColumnToBeCalculatedComparator(RowOrColumnToBeCalculated rowOrColumnToBeCalculated)
        {
            IsRow = rowOrColumnToBeCalculated.IsRow;
            IsFull = rowOrColumnToBeCalculated.IsFull;
            IsNeedToRemoveVariants = rowOrColumnToBeCalculated.IsNeedToRemoveVariants;
            VariantsCount = rowOrColumnToBeCalculated.VariantsCount;
            IndexOfRowOrColumn = rowOrColumnToBeCalculated.IndexOfRowOrColumn;
        }
        public int CompareTo(RowOrColumnToBeCalculatedComparator other)
        {
            if (other.IsFull && !IsFull) return -1;
            else
            {
                if (!other.IsFull && IsFull) return 1;
                else
                {
                    if (other.VariantsCount > VariantsCount) return -1;
                    else
                    {
                        if (other.VariantsCount < VariantsCount) return 1;
                        else return 0;
                    }
                }
            }
        }
    }
}