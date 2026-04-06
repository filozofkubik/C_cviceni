using Microsoft.VisualStudio.TestPlatform.TestHost;
using MergeSort;

namespace MergeSort.Tests
{
    public class UnitTest1
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { 1, 3, 5, 2, 3, 6 };
            int[] expectedArray = { 1, 2, 3, 3, 5, 6 };
            int left = 0;
            int right = array.Length-1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Merge(array, left, middle, right);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
    }
    public class UnitTest2
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { -1, -3, 5, 2, 3, 6 };
            int[] expectedArray = { -3,-1, 2,  3, 5, 6 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Merge(array, left, middle, right);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
    }
    public class UnitTest3
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { 1-2, 3, 5, 2, 3, 6+99 };
            int[] expectedArray = { 1-2, 2, 3, 3, 5, 6+99 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Merge(array, left, middle, right);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
    }
    public class UnitTest4
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { 1, 3, 5*8, 2, 3/3, 6 };
            int[] expectedArray = { 1,3/3, 2, 3, 3, 6 ,5*8};
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Merge(array, left, middle, right);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
    }
    public class UnitTest5
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { 1, 1, 1, 1, 1, 1 };
            int[] expectedArray = { 1, 1, 1, 1, 1, 1 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Merge(array, left, middle, right);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
    }
    public class UnitTest6
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { 1, 3, 55555555, 2, 3, 6 };
            int[] expectedArray = { 1, 2, 3, 3, 6,55555555 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Merge(array, left, middle, right);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
    }
    public class UnitTest7
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { 1, 3, 5, 2, 3, 6 };
            int[] expectedArray = { 1, 2, 3, 3, 5, 6 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Sort(array);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
    }
    public class UnitTest8
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = {  };
            int[] expectedArray = { };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Sort(array);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
    }
    public class UnitTest9
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { -1, 3, 5, 2, -3,3, 6 };
            int[] expectedArray = { -3,-1, 2, 3, 3, 5, 6 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Sort(array);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
    }

}
