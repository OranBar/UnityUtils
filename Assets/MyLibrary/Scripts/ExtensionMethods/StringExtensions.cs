using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/* *****************************************************************************
 * File:    StringExtensions.cs
 * Author:  Philip Pierce - Tuesday, September 09, 2014
 * Description:
 *  Extensions for strings
 *  
 * History:
 *  Tuesday, September 09, 2014 - Created
 * ****************************************************************************/

/// <summary>
/// Extensions for strings
/// </summary>
public static class StringExtensions
{
    #region Reverse

    /// <summary>
    /// Reverses the string
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string Reverse(this string input)
    {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new String(chars);
    }

    // Reverse
    #endregion

    #region IsNullOrEmpty

    /// <summary>
    /// Null or empty check as extension
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    // IsNullOrEmpty
    #endregion

    #region RemoveSpaces

    /// <summary>
    /// Remove white space, not line end
    /// Useful when parsing user input such phone,
    /// price int.Parse("1 000 000".RemoveSpaces(),.....
    /// </summary>
    /// <param name="value"></param>
    public static string RemoveSpaces(this string value)
    {
        return value.Replace(" ", string.Empty);
    }

    // RemoveSpaces
    #endregion

    #region SubstringFromXToY

    /// <summary>
    /// Extracts the substring starting from 'start' position to 'end' position.
    /// </summary>
    /// <param name="s">The given string.</param>
    /// <param name="start">The start position.</param>
    /// <param name="end">The end position.</param>
    /// <returns>The substring.</returns>
    public static string SubstringFromXToY(this string s, int start, int end)
    {
        if (s.IsNullOrEmpty())
            return string.Empty;

        // if start is past the length of the string
        if (start >= s.Length)
            return string.Empty;

        // if end is beyond the length of the string, reset
        if (end >= s.Length)
            end = s.Length - 1;

        return s.Substring(start, end - start);
    }

    // SubstringFromXToY
    #endregion


    #region GetWordCount

    /// <summary>
    /// Returns the number of words in the given string.
    /// </summary>
    /// <param name="s">The given string.</param>
    /// <returns>The word count.</returns>
    public static int GetWordCount(this string s)
    {
        return (new Regex(@"\w+")).Matches(s).Count;
    }

    // GetWordCount
    #endregion

    #region IsValidEmail

    /// <summary>
    /// Returns true if the email address is valid
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsValidEmail(this string email)
    {
        // fail if null or too long
        if ((string.IsNullOrEmpty(email)) || (email.Length > 100))
            return false;

        //// set to ignore case
        //Regex regex = new Regex(STR_EmailPattern, RegexOptions.IgnoreCase);
        //// return if the regex matches
        //return regex.IsMatch(email);


        // use the MSDN email validator
        return new EmailValidator().IsValidEmail(email);
    }

    // IsValidEmail
    #endregion

    #region IsValidIPAddress

    /// <summary>
    /// Checks whether the given string is a valid IP address using regular expressions.
    /// </summary>
    /// <param name="s">The given string.</param>
    /// <returns>True if it is a valid IP address, false otherwise.</returns>
    public static bool IsValidIPAddress(this string s)
    {
        return Regex.IsMatch(s, @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
    }

    // IsValidIPAddress
    #endregion

}
