namespace PasswordStrength
{
    public class PasswordEvaluator
    {
        public enum Strength
        {
            INELIGIBLE,
            WEAK,
            MEDIUM,
            STRONG
        }

        public Strength Evaluate(string password)
        {
            if (string.IsNullOrEmpty(password))
                return Strength.INELIGIBLE;

            int criteria = 0;

            // ✅ NEW: minimum length check
            if (password.Length >= 8)
                criteria++;

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSymbol = false;

            foreach (char ch in password)
            {
                if (char.IsUpper(ch)) hasUpper = true;
                else if (char.IsLower(ch)) hasLower = true;
                else if (char.IsDigit(ch)) hasDigit = true;
                else hasSymbol = true;
            }

            if (hasUpper) criteria++;
            if (hasLower) criteria++;
            if (hasDigit) criteria++;
            if (hasSymbol) criteria++;

            // Map criteria count → Strength
            return criteria switch
            {
                0 => Strength.INELIGIBLE,
                1 => Strength.WEAK,
                2 or 3 => Strength.MEDIUM,
                _ => Strength.STRONG
            };
        }
    }
}
