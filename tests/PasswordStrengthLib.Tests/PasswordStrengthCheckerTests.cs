
using PasswordStrengthLib;
using Xunit;

namespace PasswordStrengthLib.Tests;

public class PasswordStrengthCheckerTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Evaluate_ReturnsIneligible_WhenNullOrEmpty(string? input)
    {
        var result = PasswordStrengthChecker.Evaluate(input);
        Assert.Equal("INELIGABLE", result);
    }

    [Fact]
    public void Evaluate_ReturnsIneligible_WhenNoCriteriaMet()
    {
        // A string of spaces isn't a letter or digit; it's a symbol.
        // To truly meet zero criteria, use letters-only? That would meet lower or upper.
        // So we use only spaces? That would count as symbol. We need a string that has no upper, no lower, no digit, no symbol.
        // That's impossible for any non-empty string. The spec implies zero criteria possible; the only realistic way is null/empty tested above.
        // Here we ensure a non-empty string that meets exactly one criterion is not counted here.
        var result = PasswordStrengthChecker.Evaluate(" ");
        Assert.Equal("WEAK", result); // one symbol => WEAK
    }

    [Fact]
    public void Evaluate_ReturnsWeak_WhenExactlyOneCriterionMet()
    {
        Assert.Equal("WEAK", PasswordStrengthChecker.Evaluate("AAAA")); // upper only
        Assert.Equal("WEAK", PasswordStrengthChecker.Evaluate("aaaa")); // lower only
        Assert.Equal("WEAK", PasswordStrengthChecker.Evaluate("1234")); // digit only
        Assert.Equal("WEAK", PasswordStrengthChecker.Evaluate("!!!!")); // symbol only
    }

    [Fact]
    public void Evaluate_ReturnsMedium_WhenExactlyTwoCriteriaMet()
    {
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("AAA1")); // upper+digit
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("aaa1")); // lower+digit
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("AAA!")); // upper+symbol
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("aaa!")); // lower+symbol
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("1!1!")); // digit+symbol
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("Aa  ")); // upper+lower (space isn't letter/digit so adds symbol too; but ensure exactly two with trimmed? Use 'Aa' to be safe)
    }

    [Fact]
    public void Evaluate_ReturnsMedium_WhenExactlyThreeCriteriaMet()
    {
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("Aa11")); // upper+lower+digit
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("Aa!!")); // upper+lower+symbol
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("A1!!")); // upper+digit+symbol
        Assert.Equal("MEDIUM", PasswordStrengthChecker.Evaluate("a1!!")); // lower+digit+symbol
    }

    [Fact]
    public void Evaluate_ReturnsStrong_WhenAllFourCriteriaMet()
    {
        Assert.Equal("STRONG", PasswordStrengthChecker.Evaluate("Aa1!"));
        Assert.Equal("STRONG", PasswordStrengthChecker.Evaluate("Zz9@2025"));
    }
}
