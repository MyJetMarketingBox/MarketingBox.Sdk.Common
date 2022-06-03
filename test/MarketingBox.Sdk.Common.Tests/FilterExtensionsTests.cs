using System.Linq;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.Sdk.Common.Extensions;
using NUnit.Framework;

namespace MarketingBox.Sdk.Common.Tests;

[TestFixture]
public class FilterExtensionsTests
{
    private enum TestEnum
    {
        Test1 = 1,
        Test2
    }

    private static object[] _validEnumData =
    {
        new object[] {"Test1,Test2,Test2,Test1"},
        new object[] {"1,2,1,2"},
    };

    private static object[] _wrongEnumData =
    {
        new object[] {"Test1,Test2,Test3"},
        new object[] {"Test1,,Test2"},
        new object[] {"1,2,a"},
        new object[] {"1,2,3"},
        new object[] {"1,,2"}
    };

    private static object[] _validLongData =
    {
        new object[] {"1,2,1,2"}
    };

    private static object[] _wrongLongData =
    {
        new object[] {"1,a,2","a"},
        new object[] {"1,,2",""},
    };

    private static object[] _emptyData =
    {
        new object[] {string.Empty},
        new object[] {null}
    };

    [TestCaseSource(nameof(_validLongData))]
    public void LongTest(string values)
    {
        var ids = values.Parse<long>();
        Assert.That(ids, Has.Exactly(2).Items);
        Assert.AreEqual(1, ids[0]);
        Assert.AreEqual(2, ids[1]);
    }

    [TestCaseSource(nameof(_wrongLongData))]
    public void WrongLongDataTest(string values, string val)
    {
        var exception = Assert.Throws<BadRequestException>(() => values.Parse<long>());
        Assert.NotNull(exception);
        Assert.AreEqual("Invalid format", exception.Error.ErrorMessage);
        Assert.AreEqual($"Can't parse value: {val}", exception.Error.ValidationErrors.FirstOrDefault()?.ErrorMessage);
        Assert.AreEqual(nameof(values), exception.Error.ValidationErrors.FirstOrDefault()?.ParameterName);
    }

    [TestCaseSource(nameof(_validEnumData))]
    public void EnumTest(string values)
    {
        var ids = values.Parse<TestEnum>();
        Assert.That(ids, Has.Exactly(2).Items);
        Assert.AreEqual(TestEnum.Test1, ids[0]);
        Assert.AreEqual(TestEnum.Test2, ids[1]);
    }

    [TestCaseSource(nameof(_wrongEnumData))]
    public void WrongEnumDataTest(string values)
    {
        var exception = Assert.Throws<BadRequestException>(() => values.Parse<TestEnum>());
        Assert.NotNull(exception);
        Assert.AreEqual("Invalid format", exception.Error.ErrorMessage);
        Assert.AreEqual("Can't parse values", exception.Error.ValidationErrors.FirstOrDefault()?.ErrorMessage);
        Assert.AreEqual(nameof(values), exception.Error.ValidationErrors.FirstOrDefault()?.ParameterName);
    }

    [TestCaseSource(nameof(_emptyData))]
    public void EmptyDataTest(string values)
    {
        var idsEnum = values.Parse<TestEnum>();
        Assert.IsEmpty(idsEnum);
        var idsLong = values.Parse<long>();
        Assert.IsEmpty(idsLong);
    }
}