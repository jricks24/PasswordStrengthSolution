
using System;
using System.Linq;

namespace PasswordStrengthLib;

public static class PasswordStrengthChecker
{
    /// <summary>
    /// Evaluates the strength of a password based on four criteria:
    /// 1) At least one uppercase letter, 2) at least one lowercase letter,
    /// 3) at least one digit, 4) at least one symbol (non-letter/digit).
    /// Returns one of: "INELIGABLE", "WEAK", "MEDIUM", "STRONG".
    /// </summary>
    public static string Evaluate(string? password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return "INELIGABLE";
        }

        bool hasUpper = password.Any(char.IsUpper);
        bool hasLower = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSymbol = password.Any(c => !char.IsLetterOrDigit(c));

        int met = (hasUpper ? 1 : 0)
                + (hasLower ? 1 : 0)
                + (hasDigit ? 1 : 0)
                + (hasSymbol ? 1 : 0);

        return met switch
        {
            0 => "INELIGABLE",
            1 => "WEAK",
            2 or 3 => "MEDIUM",
            4 => "STRONG",
            _ => "INELIGABLE"
        };
    }
}
