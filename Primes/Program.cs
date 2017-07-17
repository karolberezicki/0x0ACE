using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace Primes
{
    public class Program
    {
        private const string AceKeyName = "X-0x0ACE-Key";
        private const string AceKeyValue = "";
        private const string UriString = "http://5.9.247.121/d34dc0d3";

        public static void Main()
        {
            string result = MainAsync().Result;
            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static async Task<string> MainAsync()
        {
            // http://stackoverflow.com/a/1072205/7454424
            //List<int> primes = PrimeGenerator.GeneratePrimesSieveOfEratosthenes(1000000);

            // http://www.geekality.net/2009/10/19/the-sieve-of-atkin-in-c/
            List<int> primes = PrimeGenerator.GeneratePrimesSieveOfAtkin(1000000);

            Dictionary<string, string> headers = new Dictionary<string, string> {{AceKeyName, AceKeyValue}};

            string htmlString = await HttpTools.HttpGetAsync(UriString, headers).ConfigureAwait(false);
            htmlString = Regex.Replace(htmlString, @"\s+", "");

            Regex regex = new Regex("<spanclass=\"challenge\">(.*?)</span>");
            string challenge = regex.Matches(htmlString)[0].Groups[1].Value;

            regex = new Regex("inputtype=\"hidden\"name=\"verification\"value=\"(.*?)\"");
            string verification = regex.Matches(htmlString)[0].Groups[1].Value;

            List<int> challengeNumbers =
                challenge.Replace("[", "").Replace(",...", "").Replace("]", "").Split(',').Select(int.Parse).ToList();

            List<int> solutionList = primes.Where(p => p > challengeNumbers[0] && p < challengeNumbers[1]).ToList();
            string solution = string.Join(",", solutionList);

            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"verification", verification},
                {"solution", solution}
            };

            return await HttpTools.HttpPostAsync(UriString, headers, data).ConfigureAwait(false);
        }
    }
}