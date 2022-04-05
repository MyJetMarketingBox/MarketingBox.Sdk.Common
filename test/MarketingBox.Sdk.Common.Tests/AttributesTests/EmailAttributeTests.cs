using MarketingBox.Sdk.Common.Attributes;
using NUnit.Framework;

namespace MarketingBox.Sdk.Common.Tests.AttributesTests;

[TestFixture]
public class EmailAttributeTests
{
    private static object[] _validEmails =
    {
        new object[] { "a@a.aa" },
        new object[] { "a@a.aaa" },
        new object[] { "a@a[a]a.aa" },
        new object[] { "a-a@a.aa" },
        new object[] { "a_a@a.aa" },
        new object[] { "a.a@a.aa" },
        new object[] { "a.a1234567890@a.aa" },
        new object[] { "a.a@1234567890a.aa" },
        new object[] { $"{new string('a',64)}@{new string('a',252)}.aa" },
    };

    private static object[] _inValidEmails =
    {
        new object[] { ".a@a.aa" },
        new object[] { "a.@a.aa" },
        new object[] { "a@.a.aa." },
        new object[] { "-a@a.aaa" },
        new object[] { "a-@a.aaa" },
        new object[] { "a@a.aaaa" },
        new object[] { "a@a.a" },
        new object[] { "a@[a]a.aa" },
        new object[] { "a@a[]a.aa" },
        new object[] { "a@a-]a.aa" },
        new object[] { "a@a.a-a" },
        new object[] { "a@a.a_a" },
        new object[] { "a@a.aa-" },
        new object[] { "a@a.aa_" },
        new object[] { "a@a!a.aa" },
        new object[] { "a@a@a.aa" },
        new object[] { "a@a#a.aa" },
        new object[] { "a@a$a.aa" },
        new object[] { "a@a%a.aa" },
        new object[] { "a@a^a.aa" },
        new object[] { "a@a&a.aa" },
        new object[] { "a@a*a.aa" },
        new object[] { "a@a(a.aa" },
        new object[] { "a@a)a.aa" },
        new object[] { "a@a.11" },
        new object[] { "йцук@a.aa" },
        new object[] { $"{new string('a',15)}.{new string('1',15)}.@a.aa" },
        new object[] { "@a" },
        new object[] { "a@" },
        new object[] { "a" },
        new object[] { $"a@{new string('a',253)}.aa" },
        new object[] { $"{new string('a',65)}@a.aa" },
    };
    
    [Test]
    [TestCaseSource(nameof(_validEmails))]
    public void EmailValidationHappyDayTest(string email)
    {
        var emailValidator = new IsValidEmailAttribute();
        Assert.True(emailValidator.IsValid(email));
    }
    
    [Test]
    [TestCaseSource(nameof(_inValidEmails))]
    public void EmailValidationRainyDayTest(string email)
    {
        var emailValidator = new IsValidEmailAttribute();
        Assert.False(emailValidator.IsValid(email));
    }
}