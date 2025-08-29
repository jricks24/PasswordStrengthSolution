using Xunit;
using PasswordStrengthLib;  // <- if your library namespace is different, change this

public class PasswordEvaluatorTests
{
    [Fact]
    public void Length_7_Fails_Length_Criterion()
    {
        var chk = new PasswordEvaluator();
        var strength = chk.Evaluate("Aa1!aaa"); // len = 7
        Assert.NotEqual(PasswordEvaluator.Strength.STRONG, strength);
    }

    [Fact]
    public void Length_8_All_Five_Criteria_Should_Be_Strong()
    {
        var chk = new PasswordEvaluator();
        var strength = chk.Evaluate("Aa1!aaaa"); // len = 8 + upper/lower/digit/symbol
        Assert.Equal(PasswordEvaluator.Strength.STRONG, strength);
    }

    [Fact]
    public void Only_Length_Met_Should_Be_Weak()
    {
        var chk = new PasswordEvaluator();
        var strength = chk.Evaluate("aaaaaaaa"); // only length met
        Assert.Equal(PasswordEvaluator.Strength.WEAK, strength);
    }

    [Fact]
    public void Two_Criteria_Should_Be_Medium()
    {
        var chk = new PasswordEvaluator();
        var strength = chk.Evaluate("ABCDEFGH"); // length + upper = 2
        Assert.Equal(PasswordEvaluator.Strength.MEDIUM, strength);
    }
}
