using Shouldly;
using Xunit;

namespace Valit.Tests.Int32
{
    public class Int32_IsEqualTo_Tests
    {
        [Fact]
        public void Int32_IsEqualTo_For_Not_Nullable_Values_Throws_When_Null_Rule_Is_Given()
        {
            var exception = Record.Exception(() => {
                ((IValitRule<Model, int>)null)
                    .IsEqualTo(1);
            });

            exception.ShouldBeOfType(typeof(ValitException));
        }

        [Fact]
        public void Int32_IsEqualTo_For_Not_Nullable_Value_And_Nullable_Value_Throws_When_Null_Rule_Is_Given()
        {
            var exception = Record.Exception(() => {
                ((IValitRule<Model, int>)null)
                    .IsEqualTo((int?)1);
            });

            exception.ShouldBeOfType(typeof(ValitException));
        }

        [Fact]
        public void Int32_IsEqualTo_For_Nullable_Value_And_Not_Nullable_Value_Throws_When_Null_Rule_Is_Given()
        {
            var exception = Record.Exception(() => {
                ((IValitRule<Model, int?>)null)
                    .IsEqualTo(1);
            });

            exception.ShouldBeOfType(typeof(ValitException));
        }

        [Fact]
        public void Int32_IsEqualTo_For_Nullable_Values_Throws_When_Null_Rule_Is_Given()
        {
            var exception = Record.Exception(() => {
                ((IValitRule<Model, int?>)null)
                    .IsEqualTo((int?)1);
            });

            exception.ShouldBeOfType(typeof(ValitException));
        }


        [Theory]
        [InlineData(10, true)]
        [InlineData(9, false)]
        [InlineData(11, false)]
        public void Int32_IsEqualTo_Returns_Proper_Results_For_Not_Nullable_Values(int value,  bool expected)
        {
            IValitResult result = ValitRules<Model>
                .Create()
                .Ensure(m => m.Value, _=>_
                    .IsEqualTo(value))
                .For(_model)
                .Validate();

            result.Succeeded.ShouldBe(expected);
        }

        [Theory]
        [InlineData((int) 10, true)]
        [InlineData((int) 9, false)]
        [InlineData((int) 11, false)]
        [InlineData(null, false)]
        public void Int32_IsEqualTo_Returns_Proper_Results_For_Not_Nullable_Value_And_Nullable_Value(int? value,  bool expected)
        {
            IValitResult result = ValitRules<Model>
                .Create()
                .Ensure(m => m.Value, _=>_
                    .IsEqualTo(value))
                .For(_model)
                .Validate();

            result.Succeeded.ShouldBe(expected);
        }

        [Theory]
        [InlineData(false, 10, true)]
        [InlineData(false, 9, false)]
        [InlineData(false, 11, false)]
        [InlineData(true, 10, false)]
        public void Int32_IsEqualTo_Returns_Proper_Results_For_Nullable_Value_And_Not_Nullable_Value(bool useNullValue, int value,  bool expected)
        {
            IValitResult result = ValitRules<Model>
                .Create()
                .Ensure(m => useNullValue? m.NullValue : m.NullableValue, _=>_
                    .IsEqualTo(value))
                .For(_model)
                .Validate();

            result.Succeeded.ShouldBe(expected);
        }

        [Theory]
        [InlineData(false, (int) 10, true)]
        [InlineData(false, (int) 9, false)]
        [InlineData(false, (int) 11, false)]
        [InlineData(false, null, false)]
        [InlineData(true, (int) 10, false)]
        [InlineData(true, null, false)]
        public void Int32_IsEqualTo_Returns_Proper_Results_For_Nullable_Values(bool useNullValue, int? value,  bool expected)
        {
            IValitResult result = ValitRules<Model>
                .Create()
                .Ensure(m => useNullValue? m.NullValue : m.NullableValue, _=>_
                    .IsEqualTo(value))
                .For(_model)
                .Validate();

            result.Succeeded.ShouldBe(expected);
        }

#region ARRANGE
        public Int32_IsEqualTo_Tests()
        {
            _model = new Model();
        }

        private readonly Model _model;

        class Model
        {
            public int Value => 10;
            public int? NullableValue => 10;
            public int? NullValue => null;
        }
#endregion
    }
}
