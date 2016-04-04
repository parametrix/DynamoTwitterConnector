using System;
using System.Collections.Generic;
using Tweetinvi;

namespace DynamoTwitterConnector
{
    /// <summary>
    /// Initial Authentication Node
    /// Created as a Pre-requiste for all other Nodes
    /// To Get tokens Start here: https://dev.twitter.com/oauth/overview/application-owner-access-tokens
    /// </summary>
    public static class TwitterConnector
    {
        public static object AuthenticateUser(string ConsumerKey, string ConsumerSecret, string AccessToken, string AccessTokenSecret)
        {
            Auth.SetUserCredentials(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);

            TweetinviEvents.QueryBeforeExecute += (sender, args1) =>
            {
                Console.WriteLine(args1.QueryURL);
            };

            var authenticatedUser = User.GetAuthenticatedUser();

            return authenticatedUser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="NumberOfTweets"></param>
        /// <param name="AuthenticatedUser"></param>
        /// <returns></returns>
        public static List<string> GetUserTimeLine(string UserName, int NumberOfTweets, object AuthenticatedUser)
        {
            List<string> tweets = new List<string>();

            var user = User.GetUserFromScreenName(UserName);

            var timelineTweets = user.GetUserTimeline(NumberOfTweets);
            foreach (var tweet in timelineTweets)
            {
                tweets.Add(tweet.Text);
            }

            return tweets;
        }

        /// <summary>
        /// Tweeting Function
        /// Added Authentication Node to force precursor
        /// </summary>
        /// <param name="TweetText"></param>
        /// <param name="AuthenticatedUser"></param>
        /// <returns></returns>
        public static string PublishTweet(string TweetText, object AuthenticatedUser)
        {
            var tweet = Tweet.PublishTweet(TweetText);
            Console.WriteLine(tweet.IsTweetPublished);
            if (tweet.IsTweetPublished)
                return "Successfully Published :)";
            return "Tweeting Failed :(";
        }
    }
}
