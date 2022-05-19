using Signaturit.Application.Enums;
using System.Collections.Generic;

namespace Signaturit.Application.Mappings
{
    public static class TrialsMappingExtensions
    {
        public static bool HasWildCard(string value, string wildCard)
        {
            return value.Contains(wildCard);
        }

        public static int GetPoints(string value)
        {
            int points = 0;

            char[] data = value.ToCharArray();

            bool hasK = false;
            foreach (var ch in data)
            {
                if (ch == 'k')
                {
                    points += (int)Signers.K;
                    hasK = true;
                }

                if (ch == 'n') points += (int)Signers.N;
                if (ch == 'v' && hasK == false) points += (int)Signers.V;
            }

            return points;
        }

        public static string GetPrevision(int mark, int clean, bool includeK)
        {
            Dictionary<string, int> values = new Dictionary<string, int>();
            if (!includeK) values.Add("V", (int)Signers.V);
            values.Add("N", (int)Signers.N);
            values.Add("K", (int)Signers.K);

            string res = string.Empty;

            int dif = mark - clean;
            if (dif > 0) return "W";

            foreach (var kpv in values)
            {
                if (dif + kpv.Value == 0) res = "Tie: Need " + kpv.Key;
                if (dif + kpv.Value > 0)
                {
                    res = "Win: Need " + kpv.Key;
                    break;
                }
            }

            return string.IsNullOrEmpty(res) ? "L" : res;
        }

        public static string GetResolution(string def, string pro)
        {
            if (string.IsNullOrEmpty(def) || string.IsNullOrEmpty(pro))
                return "";

            var defense = def.ToLower();
            var prosecutor = pro.ToLower();

            int pointDef = GetPoints(defense);
            int pointPro = GetPoints(prosecutor);

            if (HasWildCard(defense, "#"))
            {
                string prev = GetPrevision(pointDef, pointPro, HasWildCard(defense, "k"));
                return (prev == "W") ? "DEF" : (prev == "L") ? "PRO" : prev;
            }
            else if (HasWildCard(prosecutor, "#"))
            {
                string prev = GetPrevision(pointPro, pointDef, HasWildCard(prosecutor, "k"));
                return (prev == "W") ? "PRO" : (prev == "L") ? "DEF" : prev;
            }
            else
            {
                return pointDef > pointPro ? "DEF" : (pointDef < pointPro) ? "PRO" : "---";
            }
        }

        public static string GetStatus(string result)
        {
            if (string.IsNullOrEmpty(result)) return "UNSTARTED";
            if (result == "DEF" || result == "PRO") return "CLOSED";
            if (result == "---" || result == "PRO") return "TIED";
            return "PENDING";
        }

        public static int GetPointDif(string def, string pro)
        {
            string prev = GetResolution(def, pro);
            if (prev == "DEF") return 1;
            if (prev == "PRO") return 2;

            return 0;
        }
    }
}