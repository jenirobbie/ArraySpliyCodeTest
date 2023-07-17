using FluentAssertions;

namespace Shared.Tests
{
    public class EnumerableExtensionTests
    {
        [Fact]
        public void Split_PartsToSpltIsLessThanZero_ExceptionThrown()
        {
            // Arrange
            var partsToSplitInto = -1;
            var enumerableToSplit = new int[] { 1 };
      
            // Act
            var result = () => enumerableToSplit.Split(partsToSplitInto);

            // Assert
            result.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Split_PartsToSpltIsZero_EmptyArrayReturned()
        {
            // Arrange
            var partsToSplitInto = 0;
            var enumerableToSplit = new int[] { 1 };
    
            // Act
            var result = enumerableToSplit.Split(partsToSplitInto);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Split_EnumerableLengthIs0_EmptyArrayReturned()
        {
            // Arrange
            var partsToSplitInto = 2;
            var enumerableToSplit = new int[] { };
    
            // Act
            var result = enumerableToSplit.Split(partsToSplitInto);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Split_PartsToSpltIsOne_ArrayWithFullItemReturned()
        {
            // Arrange
            var partsToSplitInto = 1;
            var enumerableToSplit = new int[] { 1, 2, 3, 4 };

            // Act
            var result = enumerableToSplit.Split(partsToSplitInto);

            // Assert
            result.Count().Should().Be(1);
            result[0].Should().BeEquivalentTo(enumerableToSplit, o => o.WithStrictOrdering());
        }

        [Fact]
        public void Split_ArrayIsDivisibleByPartsToSplitFully_AllArraysSizedAreEqual()
        {
            // Arrange
            var partsToSplitInto = 2;
            var enumerableToSplit = new int[] { 1, 2, 3, 4 };
    
            // Act
            var result = enumerableToSplit.Split(partsToSplitInto);

            // Assert
            result.Count().Should().Be(2);
            result[0].Should().BeEquivalentTo(new int[] { 1, 2 });
            result[1].Should().BeEquivalentTo(new int[] { 3, 4 });
        }

        [Fact]
        public void Split_ArrayIsNotEquallyDivisibleByPartsToSplit_LastArrayItemHoldsRemainder() {
            // Arrange
            var partsToSplitInto = 3;
            var enumerableToSplit = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            var result = enumerableToSplit.Split(partsToSplitInto);

            // Assert
            result.Count().Should().Be(3);
            result[0].Should().BeEquivalentTo(new int[] { 1, 2 }, o => o.WithStrictOrdering());
            result[1].Should().BeEquivalentTo(new int[] { 3, 4 }, o => o.WithStrictOrdering());
            result[2].Should().BeEquivalentTo(new int[] { 5 }, o => o.WithStrictOrdering());
        }
    }
}