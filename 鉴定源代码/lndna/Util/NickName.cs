using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Util
{
    public static class NickName
    {
        static IDictionary<string, string> nmDict = new Dictionary<string, string>();

        public static bool CanIns(string name)
        {
            if (nmDict.Count == 0)
            {
                nmDict.Add("`", string.Empty); nmDict.Add("~", string.Empty); nmDict.Add("!", string.Empty); nmDict.Add("@", string.Empty);
                nmDict.Add("#", string.Empty); nmDict.Add("$", string.Empty); nmDict.Add("%", string.Empty); nmDict.Add("^", string.Empty);
                nmDict.Add("&", string.Empty); nmDict.Add("*", string.Empty); nmDict.Add("(", string.Empty); nmDict.Add(")", string.Empty);
                nmDict.Add("-", string.Empty); nmDict.Add("=", string.Empty); nmDict.Add("_", string.Empty); nmDict.Add("+", string.Empty);
                nmDict.Add("{", string.Empty); nmDict.Add("}", string.Empty); nmDict.Add("[", string.Empty); nmDict.Add("]", string.Empty);
                nmDict.Add(";", string.Empty); nmDict.Add(":", string.Empty); nmDict.Add("'", string.Empty); nmDict.Add("\"", string.Empty);
                nmDict.Add("\\", string.Empty); nmDict.Add("|", string.Empty); nmDict.Add("<", string.Empty); nmDict.Add(",", string.Empty);
                nmDict.Add(">", string.Empty); nmDict.Add(".", string.Empty); nmDict.Add("/", string.Empty); nmDict.Add("?", string.Empty);
                nmDict.Add("and ", string.Empty);
                nmDict.Add("or ", string.Empty);
                nmDict.Add("exec ", string.Empty);
                nmDict.Add("execute ", string.Empty);
                nmDict.Add("insert ", string.Empty);
                nmDict.Add("select ", string.Empty);
                nmDict.Add("delete ", string.Empty);
                nmDict.Add("update ", string.Empty);
                nmDict.Add("alter ", string.Empty);
                nmDict.Add("create ", string.Empty);
                nmDict.Add("drop ", string.Empty);
                nmDict.Add("truncate ", string.Empty);
                nmDict.Add("declare ", string.Empty);
                nmDict.Add("xp_cmdshell", string.Empty);
                nmDict.Add("restore ", string.Empty);
                nmDict.Add("backup ", string.Empty);
                nmDict.Add("net ", string.Empty);
            }

            foreach (string vkey in nmDict.Keys)
            {
                if (name.ToLower().Contains(vkey)) return false;
            }
            return true;
        }

        public static string GenerateName()
        {
            string name = string.Empty;
            string[] currentConsonant;
            string[] vowels = "a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,i,i,i,o,o,o,u,y,ee,ee,ea,ea,ey,eau,eigh,oa,oo,ou,ough,ay".Split(',');
            string[] commonConsonants = "s,s,s,s,t,t,t,t,t,n,n,r,l,d,sm,sl,sh,sh,th,th,th".Split(',');
            string[] averageConsonants = "sh,sh,st,st,b,c,f,g,h,k,l,m,p,p,ph,wh".Split(',');
            string[] middleConsonants = "x,ss,ss,ch,ch,ck,ck,dd,kn,rt,gh,mm,nd,nd,nn,pp,ps,tt,ff,rr,rk,mp,ll".Split(','); //Can't start
            string[] rareConsonants = "j,j,j,v,v,w,w,w,z,qu,qu".Split(',');
            Random rng = new Random(Guid.NewGuid().GetHashCode()); //http://codebetter.com/blogs/59496.aspx
            int[] lengthArray = new int[] { 2, 2, 2, 2, 2, 2, 3, 3, 3, 4 }; //Favor shorter names but allow longer ones
            int length = lengthArray[rng.Next(lengthArray.Length)];
            for (int i = 0; i < length; i++)
            {
                int letterType = rng.Next(1000);
                if (letterType < 775) currentConsonant = commonConsonants;
                else if (letterType < 875 && i > 0) currentConsonant = middleConsonants;
                else if (letterType < 985) currentConsonant = averageConsonants;
                else currentConsonant = rareConsonants;
                name += currentConsonant[rng.Next(currentConsonant.Length)];
                name += vowels[rng.Next(vowels.Length)];
                if (name.Length > 4 && rng.Next(1000) < 800) break; //Getting long, must roll to save
                if (name.Length > 6 && rng.Next(1000) < 950) break; //Really long, roll again to save
                if (name.Length > 7) break; //Probably ridiculous, stop building and add ending
            }
            int endingType = rng.Next(1000);
            if (name.Length > 6)
                endingType -= (name.Length * 25); //Don't add long endings if already long
            else
                endingType += (name.Length * 10); //Favor long endings if short
            if (endingType < 400) { } // Ends with vowel
            else if (endingType < 775) name += commonConsonants[rng.Next(commonConsonants.Length)];
            else if (endingType < 825) name += averageConsonants[rng.Next(averageConsonants.Length)];
            else if (endingType < 840) name += "ski";
            else if (endingType < 860) name += "son";
            else if (Regex.IsMatch(name, "(.+)(ay|e|ee|ea|oo)$") || name.Length < 5)
            {
                name = "Mc" + name.Substring(0, 1).ToUpper() + name.Substring(1);
                return name;
            }
            else name += "ez";
            name = name.Substring(0, 1).ToUpper() + name.Substring(1); //Capitalize first letter
            return name;
        }
    }
}
