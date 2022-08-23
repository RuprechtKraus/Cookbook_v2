namespace Cookbook_v2.Toolkit.Validation
{
    public static class ValidationMessage
    {
        public static string MaxLength( string subject, int maxLen )
            => $"{subject} maximum length is {maxLen}";

        public static string MinLength( string subject, int minLen )
            => $"{subject} minimum length is {minLen}";

        public static string Required( string subject )
            => $"{subject} required";

        public static string Equal( string first, string second )
            => $"{first} must be equal to {second}";
    }
}
