using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;

namespace BMC.EnterpriseBusiness.Business
{
    public enum SearchPrefixChar
    {
        None = 0,
        Hyphen,
        Hash,
        ForwardSlash,
        Backslash,
        Plus,
        Minus,
    }

    public class SearchStringResult : DisposableObject
    {
        public string Result { get; set; }
        public SearchPrefixChar PrefixChar { get; set; }
    }

    public static class GlobalBusiness
    {
        public static SearchStringResult CreateSearchString(string search)
        {
            return CreateSearchString(search, null);
        }

        public static SearchStringResult CreateSearchString(string search, Func<SearchPrefixChar, string, bool> func)
        {
            search = search.Trim();
            if (search.IsEmpty()) return new SearchStringResult()
            {
                Result = search,
            };

            ModuleProc PROC = new ModuleProc("Globals", "CreateSearchString");
            SearchStringResult result = new SearchStringResult();
            SearchPrefixChar prefixChar = SearchPrefixChar.None;
            IDictionary<SearchPrefixChar, bool> prefixCharResults = new SortedDictionary<SearchPrefixChar, bool>() 
            {
                { SearchPrefixChar.Hyphen, false },
                { SearchPrefixChar.Hash, false },
                { SearchPrefixChar.ForwardSlash, false },
                { SearchPrefixChar.Backslash, false },
                { SearchPrefixChar.Plus, false },
                { SearchPrefixChar.Minus, false },
            };
            string sResult = string.Empty;

            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in search)
                {
                    switch (c)
                    {
                        case '\'':
                            sb.Append("['']");
                            break;

                        case '-':
                            sb.Append("[-]");
                            break;

                        default:
                            sb.Append(c);
                            break;
                    }
                }

                sResult = sb.ToString();
                if (sResult.Length > 0 &&
                    sResult[0] == '+')
                {
                    prefixChar = SearchPrefixChar.Plus;
                    sResult = sResult.Substring(1, sResult.Length - 1);
                    if (func != null) prefixCharResults[prefixChar] = func(prefixChar, sResult);
                }
                if (sResult.Length > 0 &&
                    sResult[0] == '-')
                {
                    prefixChar = SearchPrefixChar.Minus;
                    sResult = sResult.Substring(1, sResult.Length - 1);
                    if (func != null) prefixCharResults[prefixChar] = func(prefixChar, sResult);
                }
                if (sResult.Length > 0 &&
                    sResult[0] == '/')
                {
                    prefixChar = SearchPrefixChar.ForwardSlash;
                    sResult = sResult.Substring(1, sResult.Length - 1);
                    if (func != null) prefixCharResults[prefixChar] = func(prefixChar, sResult);
                }
                if (sResult.Length > 0 &&
                    sResult[0] == '\\')
                {
                    prefixChar = SearchPrefixChar.Backslash;
                    sResult = sResult.Substring(1, sResult.Length - 1);
                    if (func != null) prefixCharResults[prefixChar] = func(prefixChar, sResult);
                }
                if (sResult.Length > 0 &&
                    sResult[0] == '#')
                {
                    prefixChar = SearchPrefixChar.Hash;
                    sResult = sResult.Substring(1, sResult.Length - 1);
                    if (func != null) prefixCharResults[prefixChar] = func(prefixChar, sResult);
                }
                if (sResult.Length > 0 &&
                    sResult[0] == '_')
                {
                    prefixChar = SearchPrefixChar.Hyphen;
                    sResult = sResult.Substring(1, sResult.Length - 1);
                    if (func != null) prefixCharResults[prefixChar] = func(prefixChar, sResult);
                }

                if(!prefixCharResults[SearchPrefixChar.Hyphen])
                {
                    sResult = sb.ToString();
                    sResult = sResult.Replace('*', '%').Replace('?', '_');
                    if (sResult.IndexOf('%') >= 0 && sResult.IndexOf('_') >= 0)
                    {
                        sResult = "%" + sResult + "%";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            result.Result = sResult;
            result.PrefixChar = prefixChar;
            return result;
        }

    }
}
