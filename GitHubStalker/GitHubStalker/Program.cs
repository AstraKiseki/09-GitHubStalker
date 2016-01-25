using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

/*
What I need to do is figure out where to separate classes, learn APIs, and how to seek out the information I need.  Info seeking class set is necessary, VERY necessary.
ANNND DISREGARD THAT, JSON IS INDEED AMAZING.  And github's a jerk.
*/
namespace GitHubStalker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter an username, please!");

            string username = Console.ReadLine();

            // downloading content
            WebClient wc = new WebClient();

            wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            string json = wc.DownloadString("https://api.github.com/users/" + username);

            //parse  json into something I can display
            var o = JObject.Parse(json);



            Console.WriteLine(o["login"].ToString());
            Console.WriteLine(o["login"].ToString() + " has " + o["public_repos"].ToString() + " public repositories and " + o["followers"].ToString() + " followers.  They are following " + o["following"].ToString() + " accounts." );

            Console.ReadLine();

        
            Console.WriteLine("Would you like to look at their repositories?  Y/N");

            string RepoAnswer = Console.ReadLine();

            if (RepoAnswer.Equals("Y"))
                {
                string ReposList = wc.DownloadString("http://api.github.com/users/" + username + "/repos");
                var r = JArray.Parse(ReposList);

                Console.WriteLine("Url: https:/github.com/{0}", username);
                Console.WriteLine("{0} has " + o["public_repos"].ToString() + " repositories, consisting of: ", username);
                foreach (var Repo in r)
                {
                    Console.WriteLine("====" + Repo["name"].ToString() + ", with " + Repo["watchers_count"] + "watchers, and " + Repo["stargazers_count"] + "stars.");
                }
                Console.ReadLine();
            }

            if (RepoAnswer.Equals("N"))
            {
                Console.WriteLine("Thank you for using this program!");
                Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Please answer Y or N.");
            }

            Console.ReadLine();
        }
    }
}
