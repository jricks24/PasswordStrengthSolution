using Xunit;

namespace PasswordStrengthLib.Tests
{
    public class PasswordStrengthCheckerTests
    {
        [Fact]
        public void Length_7_Fails_Length_Criterion()
        {
            var chk = new PasswordStrengthLib.PasswordStrengthChecker();
            var strength = chk.Evaluate("Aa1!aaa"); // len=7
            Assert.NotEqual(PasswordStrengthLib.PasswordStrengthChecker.Strength.STRONG, strength);
        }

        [Fact]
        public void Length_8_All_Five_Criteria_Should_Be_Strong()
        {
            var chk = new PasswordStrengthLib.PasswordStrengthChecker();
            var strength = chk.Evaluate("Aa1!aaaa"); // len=8, upper/lower/digit/symbol
            Assert.Equal(PasswordStrengthLib.PasswordStrengthChecker.Strength.STRONG, strength);
        }

        [Fact]
        public void Only_Length_Met_Should_Be_Weak()
        {
            var chk = new PasswordStrengthLib.PasswordStrengthChecker();
            var strength = chk.Evaluate("aaaaaaaa"); // only length
            Assert.Equal(PasswordStrengthLib.PasswordStrengthChecker.Strength.WEAK, strength);
        }

        [Fact]
        public void Two_Criteria_Should_Be_Medium()
        {
            var chk = new PasswordStrengthLib.PasswordStrengthChecker();
            var strength = chk.Evaluate("ABCDEFGH"); // length + upper = 2
            Assert.Equal(PasswordStrengthLib.PasswordStrengthChecker.Strength.MEDIUM, strength);
        }
    }
}
