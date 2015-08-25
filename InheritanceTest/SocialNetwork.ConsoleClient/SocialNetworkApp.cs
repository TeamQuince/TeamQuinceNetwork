namespace SocialNetwork.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SocialNetwork.Data;
    using SocialNetwork.Models;

    public class SocialNetworkApp
    {
        public static void Main()
        {
            var context = new SocialNetworkContext();
            Console.WriteLine(context.Groups.Count());
        }
    }
}
